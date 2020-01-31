using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CampaniasLito.Startup))]
namespace CampaniasLito
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
