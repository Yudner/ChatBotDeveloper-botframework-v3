using Autofac;
using Chatbot.Dialogs;
using Chatbot.Services.QnAMaker;
using Microsoft.Azure;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Chatbot.App_Start
{
    public class BotModule
    {
        public static void Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<RootDialog>().InstancePerDependency();
            builder.RegisterType<QnAMakerService>().Keyed<IQnAMakerService>(FiberModule.Key_DoNotSerialize).AsImplementedInterfaces();

            #region BotState

            var uri = new Uri(ConfigurationManager.AppSettings["CosmosDB.Uri"]);
            var key = ConfigurationManager.AppSettings["CosmosDB.Key"];
            var database = ConfigurationManager.AppSettings["CosmosDB.Database"];
            var botStateCollection = ConfigurationManager.AppSettings["CosmosDB.BotStateCollection"];

            var store = new DocumentDbBotDataStore(uri, key, database, botStateCollection);

            builder.Register(c => store)
                         .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                         .AsSelf()
                         .SingleInstance();

            builder.Register(c => new CachingBotDataStore(store,
                       CachingBotDataStoreConsistencyPolicy
                       .ETagBasedConsistency))
                       .As<IBotDataStore<BotData>>()
                       .AsSelf()
                       .InstancePerLifetimeScope();

            #endregion
            
            builder.Update(container: Conversation.Container);
        }
    }
}