namespace Ads.Data
{
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;

    public class AdsDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdsDbContext()
            : base("AdsConnection")
        {
        }

        public IDbSet<Ad> Ads { get; set; }

        public static AdsDbContext Create()
        {
            return new AdsDbContext();
        }
    }
}