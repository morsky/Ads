namespace Ads.Controllers
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ViewModels;
    public class CRUDController : BaseController
    {
        [HttpGet]
        [Authorize]
        public virtual ActionResult CreateAd()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateAd(AdViewModel adViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var user = this.Context.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
                string fileName = UploadFile(file);

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
            }

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ListAds", "Admin");
            }

            return RedirectToAction("ListAds", "User");
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult ListAds()
        {            
            ICollection<AdViewModel> userAds = Context.Ads.Select(a => new AdViewModel()
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                CreatedOn = a.CreatedOn,
                FileName = a.FileName,
                User = a.User
            })            
            .ToList();

            return View(userAds);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult EditAd(int? id)
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
        public virtual ActionResult EditAd([Bind(Include = "Id, Title, Content, FileName")] Ad ad, HttpPostedFileBase file)
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

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ListAds", "Admin");
            }

            return RedirectToAction("ListAds", "User");
        }

        [Authorize]
        public virtual ActionResult DeleteAd(int? id)
        {
            var ad = Context.Ads.Find(id);

            Context.Ads.Remove(ad);
            Context.SaveChanges();

            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ListAds", "Admin");
            }

            return RedirectToAction("ListAds", "User");
        }

        private string UploadFile(HttpPostedFileBase file)
        {
            string fileName = null;

            Directory.CreateDirectory(Server.MapPath("~\\Images\\AdsImages"));

            if (file != null && file.ContentLength > 0)
            {
                string id = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);                
                fileName = id + extension;
                var path = Path.Combine(Server.MapPath("~/Images/AdsImages"), fileName);
                file.SaveAs(path);
            }

            return fileName;
        }
    }
}