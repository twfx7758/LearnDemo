using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Learn.WebForm.Startup))]
namespace Learn.WebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
