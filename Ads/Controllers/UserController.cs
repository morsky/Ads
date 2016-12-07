namespace Ads.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;
    public class UserController : CRUDController
    {
        [HttpGet]
        [Authorize]
        public override ActionResult ListAds()
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
    }
}