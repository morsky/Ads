namespace Ads.Controllers
{
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using ViewModels;
    using PagedList;
    public class HomeController : BaseController
    {
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Context.Ads.Count();

            ICollection<AdViewModel> ads = Context.Ads.Select(a => new AdViewModel()
            {
                Title = a.Title,
                Content = a.Content,
                CreatedOn = a.CreatedOn,
                FileName = a.FileName,
                User = a.User
            })
            .ToList();

            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 2;
            int pageNumber = (page ?? 1);

            return View(ads.ToPagedList(pageNumber, pageSize));
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