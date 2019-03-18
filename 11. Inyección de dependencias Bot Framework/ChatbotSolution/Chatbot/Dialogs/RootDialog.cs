using System;
using System.Threading.Tasks;
using Chatbot.Constants;
using Chatbot.Services.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Chatbot.Dialogs
{
    [LuisModel(modelID: "6a8e8ca2-7ae4-4f0c-862a-00ccfa44147d", subscriptionKey: "fd08d1ddfc494ba8abea8761cc806593", domain: "centralus.api.cognitive.microsoft.com", threshold: 0.5)]
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        readonly IQnAMakerService _qnAMakerService;
        public RootDialog(IQnAMakerService qnAMakerService)
        {
            _qnAMakerService = qnAMakerService;
        }

        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await QnaMakerMessage(context, result.Query);
        }

        [LuisIntent("Saludos")]
        public async Task Saludos(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Hola {context.Activity.From.Name}, ¿En qué te puedo ayudar?");
        }

        [LuisIntent("CentroContacto")]
        public async Task CentroContacto(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("El número de centro de contacto es: 653499");
        }

        private async Task QnaMakerMessage(IDialogContext context, string query)
        {
            string respuesta = _qnAMakerService.GetAnswer(query);

            if (respuesta.Equals(QnaMakerConstants.AnswerNotFound))
            {
                await context.PostAsync("Lo siento pero no tengo una respuesta para esa pregunta.");
            }
            else
                await context.PostAsync(respuesta);
        }
    }
}