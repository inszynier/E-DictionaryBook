using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_DictionaryBook.Startup))]
namespace E_DictionaryBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
