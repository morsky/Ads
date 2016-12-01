using System;

namespace Ads.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using ViewModels;
    using Models;

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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdViewModel ad)
        {
            Ad newAdad = new Ad()
            {
                Title = ad.Title,
                Content = ad.Content,
                CreatedOn = DateTime.Now
            };

            this.Context.Ads.Add(newAdad);
            this.Context.SaveChanges();

            return RedirectToAction("Index");
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