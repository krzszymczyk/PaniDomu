using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaniDomu.Startup))]
namespace PaniDomu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
