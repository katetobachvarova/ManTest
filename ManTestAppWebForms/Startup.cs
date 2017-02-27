using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManTestAppWebForms.Startup))]
namespace ManTestAppWebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
