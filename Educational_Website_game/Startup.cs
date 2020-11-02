using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibraryDeweyApp.Startup))]
namespace LibraryDeweyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
