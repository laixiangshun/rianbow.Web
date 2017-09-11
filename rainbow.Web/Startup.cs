using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rainbow.Web.Startup))]
namespace rainbow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
