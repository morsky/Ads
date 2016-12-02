namespace Ads.Data
{
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class AdsDbContext : IdentityDbContext<ApplicationUser>
    {
        public AdsDbContext()
            : base("AdsConnection")
        {
            Database.Log += Logger;
        }

        public IDbSet<Ad> Ads { get; set; }

        public static AdsDbContext Create()
        {
            return new AdsDbContext();
        }

        public static void Logger(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }
    }
}