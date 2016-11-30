namespace Ads.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Data;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AdsDbContext context = new AdsDbContext();

            var count = context.Ads.Count();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}