using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using QnAMakerProject.Constants;
using QnAMakerProject.Services.QnAMaker;

namespace QnAMakerProject.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            var serviceQnaMaker = new QnAMakerService();
            var respuesta = serviceQnaMaker.GetAnswer(message.Text);
            if (respuesta.Equals(QnaMakerConstants.AnswerNotFound))
            {
                await context.PostAsync("Lo siento, pero no estoy preparado para este tipo de preguntas.");
            }
            else
                await context.PostAsync(respuesta);
        }
    }
}