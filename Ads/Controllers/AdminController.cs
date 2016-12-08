namespace Ads.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;
    public class AdminController : CRUDController
    {
        [HttpGet]
        public ActionResult ListUsers()
        {
            var admin = this.Context.Users.FirstOrDefault(a => a.Email == "admin@admin.com");

            ICollection<ApplicationUserViewModel> users = Context.Users.Select(u => new ApplicationUserViewModel()
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            })
            .Where(u => u.Email != admin.Email)
            .ToList();

            return View(users);
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditUserRole(string id)
        {
            var selectedUser = Context.Users.Find(id);

            ViewBag.User = selectedUser.UserName.ToString();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(Context));
            var userRole = UserManager.GetRoles(selectedUser.Id);
            
            ViewBag.UserRoles = userRole;

            var roles = Context.Roles.OrderBy(x => x.Name);
            List<string> userRoles = new List<string>();

            foreach (var role in roles)
            {
                userRoles.Add(role.Name);
            }            

            return View(userRoles);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditUserRole(List<string> role)
        {

            return View();
        }

        [Authorize]
        public ActionResult DeleteUser(string id)
        {
            var selectedUser = Context.Users.Find(id);
            int[] adIds = new int[selectedUser.Ads.Count];
            int count = 0;

            foreach (var item in selectedUser.Ads)
            {
                var addNumber = item.Id;
                adIds[count] = addNumber;

                count++;
            }

            for (int i = 0; i < adIds.Length; i++)
            {
                var ad = Context.Ads.Find(adIds[i]);
                var fullPath = Server.MapPath("~/Images/AdsImages/" + ad.FileName);

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Context.Ads.Remove(ad);
            }

            Context.Users.Remove(selectedUser);
            Context.SaveChanges();

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}