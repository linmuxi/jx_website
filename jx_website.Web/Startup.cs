using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jx_website.Web.Startup))]
namespace jx_website.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
