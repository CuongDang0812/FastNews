using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FastNews.Startup))]
namespace FastNews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
