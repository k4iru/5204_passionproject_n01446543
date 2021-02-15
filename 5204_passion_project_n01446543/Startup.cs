using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_5204_passion_project_n01446543.Startup))]
namespace _5204_passion_project_n01446543
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
