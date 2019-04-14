using Autofac;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace BotFacebookAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                GlobalConfiguration.Configure(WebApiConfig.Register);

                var storage = new TableBotDataStore(ConfigurationManager.AppSettings["Storage.ConnectionString"]);

                Conversation.UpdateContainer(
                           builder =>
                           {
                               builder.Register(c => storage)
                                         .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                                         .AsSelf()
                                         .SingleInstance();

                               builder.Register(c => new CachingBotDataStore(storage,
                                          CachingBotDataStoreConsistencyPolicy
                                          .ETagBasedConsistency))
                                          .As<IBotDataStore<BotData>>()
                                          .AsSelf()
                                          .InstancePerLifetimeScope();
                           });
            }
            catch (Exception e)
            {

            }
        }
    }
}
