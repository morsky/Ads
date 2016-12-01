namespace Ads.Controllers
{
    using System.Web.Mvc;
    using Data;

    public class BaseController : Controller
    {
        public BaseController()
            : this (new AdsDbContext())
        {
        }

        public BaseController(AdsDbContext context)
        {
            this.Context = context;
        }

        public AdsDbContext Context { get; set; }
    }
}