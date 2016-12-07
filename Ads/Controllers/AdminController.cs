namespace Ads.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;
    public class AdminController : CRUDController
    {
        [HttpGet]
        public ActionResult ListUsers()
        {
            //var admin = this.Context.Users.Where(u => u.Email != "admin@admin.com").ToList();
            var admin = this.Context.Users.FirstOrDefault(a => a.Email == "admin@admin.com");

            ICollection<ApplicationUserViewModel> users = Context.Users.Select(u => new ApplicationUserViewModel()
            {
                UserName = u.UserName,
                Email = u.Email,
            })
            .Where(u => u.Email != admin.Email)
            .ToList();

            return View(users);
        }
    }
}