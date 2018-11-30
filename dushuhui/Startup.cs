using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dushuhui.Startup))]
namespace dushuhui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
