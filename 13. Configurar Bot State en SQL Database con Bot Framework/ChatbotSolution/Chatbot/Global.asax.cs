using Chatbot.App_Start;
using System.Web.Http;

namespace Chatbot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            BotModule.Configure();
        }
    }
}
