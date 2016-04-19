using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Learn.MvcWeb.Startup))]
namespace Learn.MvcWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
