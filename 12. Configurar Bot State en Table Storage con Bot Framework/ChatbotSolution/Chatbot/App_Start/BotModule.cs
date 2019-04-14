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

            string storageConnectionString = CloudConfigurationManager.GetSetting("Storage.ConnectionString");
            string storageStateTable = CloudConfigurationManager.GetSetting("Storage.StateTable");

            var store = new TableBotDataStore(storageConnectionString, storageStateTable);

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