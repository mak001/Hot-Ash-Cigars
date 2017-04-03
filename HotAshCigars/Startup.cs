using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotAshCigars.Startup))]
namespace HotAshCigars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
