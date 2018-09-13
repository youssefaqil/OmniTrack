using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OmniTrackTma_TDB_.Startup))]
namespace OmniTrackTma_TDB_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
