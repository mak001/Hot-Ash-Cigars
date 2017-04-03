using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hot_Ash_Cigars.Startup))]
namespace Hot_Ash_Cigars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
