using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DestinaMarket.Web.Startup))]
namespace DestinaMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
