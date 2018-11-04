using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using QnAMakerLUISProject.Constants;
using QnAMakerLUISProject.Services.QnAMaker;

namespace QnAMakerLUISProject.Dialogs
{
    [LuisModel(modelID: "", subscriptionKey: "", domain: "", threshold: 0.5)
    ]

    [Serializable]
    public class RootDialog : LuisDialog<string>
    {
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
            var qnaService = new QnAMakerService();
            string respuesta = qnaService.GetAnswer(query);

            if (respuesta.Equals(QnaMakerConstants.AnswerNotFound))
            {
                await context.PostAsync("Lo siento pero no tengo una respuesta para esa pregunta.");
            }
            else
                await context.PostAsync(respuesta);
        }
    }
}