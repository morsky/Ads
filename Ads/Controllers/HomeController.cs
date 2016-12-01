using System;
using System.Linq;

namespace Ads.Controllers
{
    using System.Web.Mvc;
    using Data;
    using System.Collections.Generic;
    using ViewModels;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ICollection<AdViewModel> ads = Context.Ads.Select(a => new AdViewModel()
            {
                Title = a.Title,
                Content = a.Content,
                CreatedOn = a.CreatedOn
            })
            .ToList();

            return View(ads);
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