using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CmsTest.Startup))]
namespace CmsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
