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

        [Authorize]
        public ActionResult DeleteUser(string id)
        {            
            var currentUser = Context.Users.Find(id);
            int[] adIds = new int[currentUser.Ads.Count];            
            int count = 0;

            foreach (var item in currentUser.Ads)
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

            Context.Users.Remove(currentUser);
            Context.SaveChanges();

            return RedirectToAction("ListUsers", "Admin");
        }
    }
}