using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Neptun.Startup))]
namespace Neptun
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
