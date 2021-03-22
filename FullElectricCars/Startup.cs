using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullElectricCars.Startup))]
namespace FullElectricCars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
