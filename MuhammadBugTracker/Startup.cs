using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MuhammadBugTracker.Startup))]
namespace MuhammadBugTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
