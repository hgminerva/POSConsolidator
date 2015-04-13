using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POSConsolidator.Startup))]
namespace POSConsolidator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
