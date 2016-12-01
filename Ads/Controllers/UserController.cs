namespace Ads.Controllers
{
    using System.Web.Mvc;
    using System;
    using System.Linq;
    using Models;
    using ViewModels;

    public class UserController : BaseController
    {
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(AdViewModel adViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }
    }
}