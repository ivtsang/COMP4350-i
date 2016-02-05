using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConnectR.Startup))]
namespace ConnectR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
