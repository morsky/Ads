using System.Data.Entity;
using Microsoft.AspNet.Identity;

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

            Ad newAd = new Ad()
            {
                Title = adViewModel.Title,
                Content = adViewModel.Content,
                CreatedOn = DateTime.Now,
                User = user
            };

            this.Context.Ads.Add(newAd);
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

        [HttpGet]
        public ActionResult EditAd(int? id)
        {
            var ad = Context.Ads.Find(id);

            if (ad == null)
            {
                return HttpNotFound();
            }

            TempData["AdModel"] = ad;

            return View(ad);
        }

        [HttpPost]
        public ActionResult EditAd([Bind(Include = "Title, Content")] Ad ad)
        {
            //ad.User = Context.Users.Find(User.Identity.GetUserId());
            //ad.CreatedOn = DateTime.Now;
            var currentAd = TempData["AdModel"] as Ad;

            if (currentAd != null)
            {
                ad.Id = currentAd.Id;
                ad.CreatedOn = currentAd.CreatedOn;
            }

            if (ModelState.IsValid)
            {
                Context.Ads.Attach(ad);
                Context.Entry(ad).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return RedirectToAction("ListAds", "User");
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