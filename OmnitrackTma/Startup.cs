using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OmnitrackTma.Startup))]
namespace OmnitrackTma
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
