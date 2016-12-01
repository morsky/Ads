namespace Ads.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Linq;
    using Models;
    using ViewModels;
    using System.Collections.Generic;

    public class UserController : BaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult CreateAd()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateAd(AdViewModel adViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListAds", "User");
            }

            var user = this.Context.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            Ad newAdad = new Ad()
            {
                Title = adViewModel.Title,
                Content = adViewModel.Content,
                CreatedOn = DateTime.Now,
                User = user
            };

            this.Context.Ads.Add(newAdad);
            this.Context.SaveChanges();

            return RedirectToAction("ListAds", "User");
        }

        public ActionResult ListAds()
        {
            var user = this.Context.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            ICollection<AdViewModel> userAds = Context.Ads.Select(a => new AdViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedOn = a.CreatedOn,
                User = a.User
            })
            .Where(a => a.User.UserName == user.UserName)
            .ToList();
            
            return View(userAds);
        }

        public ActionResult DeleteAd(int? id)
        {
            var ad = Context.Ads.Find(id);

            Context.Ads.Remove(ad);
            Context.SaveChanges();

            return RedirectToAction("ListAds", "User");
        }
    }
}