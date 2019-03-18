using Autofac;
using Chatbot.Dialogs;
using Chatbot.Services.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
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

            builder.Update(container: Conversation.Container);
        }
    }
}