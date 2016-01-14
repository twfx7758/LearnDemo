using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Learn.MVCTest.Startup))]
namespace Learn.MVCTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
