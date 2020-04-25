using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSchoolSystem.Startup))]
namespace SmartSchoolSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
