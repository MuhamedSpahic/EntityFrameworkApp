using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventorA.Startup))]
namespace EventorA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
