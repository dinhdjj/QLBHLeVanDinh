using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBHLeVanDinh.Startup))]
namespace QLBHLeVanDinh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
