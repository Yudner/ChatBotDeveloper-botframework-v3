using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CustomerServiceProject.Dialogs
{
    [LuisModel(modelID: "Application Id", subscriptionKey: "Suscription key")]

    [Serializable]
    public class CustomerServiceDialog : LuisDialog<string>
    {
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Lo siento pero no estoy programadao para este tipo de preguntas");
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }

        [LuisIntent("ObtenerAgradecimientos")]
        public async Task ObtenerAgradecimientos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Siempre estaré para ayudarte");
            await Task.Delay(2000);
            await context.PostAsync("¿En qué más te puedo ayudar?");
        }

        [LuisIntent("HacerReclamo")]
        public async Task HacerReclamo(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Gracias por reportarlo");
            await Task.Delay(2000);
            await context.PostAsync("Por favor registra tu problema en el siguiente enlace:");
            await Task.Delay(2000);

            var reply = context.MakeMessage();
            reply.Attachments.Add(GetCard());
            await Task.Delay(2000);
            await context.PostAsync(reply);

        }

        private Attachment GetCard()
        {
            var card = new HeroCard
            {
                Title="Asistente Virtual",
                Buttons = new List<CardAction>
                {
                    new CardAction(type: ActionTypes.OpenUrl, title:"Ir a la web", value:"https://azure.microsoft.com/es-es/support/options/")
                }
            };
            return card.ToAttachment();
        }
    }
}