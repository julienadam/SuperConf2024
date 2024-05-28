using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SuperConf2024.Entities;

namespace SuperConf2024.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration config;
        
        public TestController(IConfiguration config)
        {
            this.config = config;
        }

        public IActionResult Blob()
        {
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = config["ManagedIdentityClientId"] });
            var account = config["StorageAccount"];
            var client = new BlobServiceClient(
                new Uri($"https://{account}.blob.core.windows.net/"),
                credential);

            var containerName = "tickets";
            var containerClient = client.GetBlobContainerClient(containerName);

            var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString());
            blobClient.Upload(new BinaryData(DateTime.Now.ToString()));

            return Content("Upload OK");
        }

        public IActionResult QrCode()
        {
            Inscription inscription = new Inscription
            {
                Email = "foo@bar.com",
                Nom = "Martin",
                Prenom = "Robert",
                DateNaissance = new DateTime(1987, 8, 5)
            };

            var json = System.Text.Json.JsonSerializer.Serialize(inscription);
            
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                json, 
                QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                return base.File(qrCodeImage, "image/png");
            }
        }

        public IActionResult Pdf()
        {
            Inscription inscription = new Inscription();
            inscription.Email = "foo@bar.com";
            inscription.Nom = "Martin";
            inscription.Prenom = "Robert";
            inscription.DateNaissance = new DateTime(1987, 8, 5);

            var json = System.Text.Json.JsonSerializer.Serialize(inscription);


            using QRCodeGenerator qrGenerator = new QRCodeGenerator();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            // code in your main method
            byte[] pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("FOO")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(Placeholders.LoremIpsum());
                            x.Item().Image(qrCodeImage);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            })
            //.ShowInPreviewer();
            .GeneratePdf();

            return base.File(pdf, "application/pdf");
        }

    }
}
