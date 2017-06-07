using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RIBA_Task.Startup))]
namespace RIBA_Task
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
