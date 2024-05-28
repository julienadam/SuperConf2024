using QuestPDF.Infrastructure;
using SuperConf2024.Entities;
using SuperConf2024.Services;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

if (builder.Configuration["USE_FAKES"] == "1")
{
    builder.Services.AddSingleton<IInscriptionService, FakeInscriptionService>();
}
else
{
    builder.Services.AddDbContext<SuperconfdbContext>();
    builder.Services.AddScoped<IInscriptionService, DbInscriptionService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
