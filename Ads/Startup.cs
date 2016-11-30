using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ads.Startup))]
namespace Ads
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
