using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(klukk_social.Startup))]
namespace klukk_social
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
