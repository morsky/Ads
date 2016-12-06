namespace Ads.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Linq;
    using Models;
    using ViewModels;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using System.Web;
    using System.IO;

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
        [ValidateAntiForgeryToken]
        public ActionResult CreateAd(AdViewModel adViewModel, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ListAds", "User");
            }

            string fileName = UploadFile(file);            

            var user = this.Context.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            Ad newAd = new Ad()
            {
                Title = adViewModel.Title,
                Content = adViewModel.Content,
                CreatedOn = DateTime.Now,
                FileName = fileName,
                User = user
            };

            this.Context.Ads.Add(newAd);
            this.Context.SaveChanges();

            return RedirectToAction("ListAds", "User");
        }        

        [HttpGet]
        [Authorize]
        public ActionResult ListAds()
        {
            var user = this.Context.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);

            ICollection<AdViewModel> userAds = Context.Ads.Select(a => new AdViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedOn = a.CreatedOn,
                FileName = a.FileName,
                User = a.User
            })
            .Where(a => a.User.UserName == user.UserName)
            .ToList();
            
            return View(userAds);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditAd(int? id)
        {
            var ad = Context.Ads.Find(id);

            if (ad == null)
            {
                return HttpNotFound();
            }
            
            return View(ad);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditAd([Bind(Include = "Id, Title, Content, FileName")] Ad ad, HttpPostedFileBase file)
        {
            ad.User = Context.Users.Find(User.Identity.GetUserId());
            ad.CreatedOn = DateTime.Now;
            ad.FileName = UploadFile(file);


            if (ModelState.IsValid)
            {
                Context.Ads.Attach(ad);
                Context.Entry(ad).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return RedirectToAction("ListAds", "User");
        }

        [HttpDelete]
        [Authorize]
        public ActionResult DeleteAd(int? id)
        {
            var ad = Context.Ads.Find(id);

            Context.Ads.Remove(ad);
            Context.SaveChanges();

            return RedirectToAction("ListAds", "User");
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            string fileName = null;

            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/AdsImages"), fileName);
                file.SaveAs(path);
            }

            return fileName;
        }
    }
}