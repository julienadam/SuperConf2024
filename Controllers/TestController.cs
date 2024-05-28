using Microsoft.AspNetCore.Mvc;
using SuperConf2024.Entities;

namespace SuperConf2024.Controllers
{
    public class TestController : Controller
    {
        private readonly SuperconfdbContext context;

        public TestController(SuperconfdbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return Content(context.Inscriptions.Count().ToString());
        }
    }
}
