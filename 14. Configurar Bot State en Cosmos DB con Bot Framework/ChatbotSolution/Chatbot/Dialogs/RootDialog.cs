using System;
using System.Threading.Tasks;
using Chatbot.Constants;
using Chatbot.Services.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace Chatbot.Dialogs
{
    [LuisModel(modelID: "cec61c99-9ef9-465d-a0b5-d807f0bdcd75", subscriptionKey: "22a9e6a0b7314b2d8540a041659825a0", domain: "centralus.api.cognitive.microsoft.com", threshold: 0.5)]

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