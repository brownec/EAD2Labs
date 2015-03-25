using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectYear4.Startup))]
namespace ProjectYear4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
