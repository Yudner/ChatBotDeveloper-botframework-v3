using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace ChatBotProject.Dialogs
{
    [Serializable]
    public class TextFlowDialog : IDialog
    {
        string optionSaludar = "1. Saludar";
        string optionDesapedir = "2. Despedir";
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Hola soy tu amigo Bot y tengo estas opciones para ti:");
            await context.PostAsync(optionSaludar+ " \n" +optionDesapedir);
            context.Wait(OptioResult);
        }

        private async Task OptioResult(IDialogContext context, IAwaitable<object> result)
        {
            var message= await result as Activity;


            if ((message.Text.Equals("Saludar")) || (message.Text.Equals("Despedir")))
            {
                if (message.Text.Equals("Saludar"))
                {
                    await context.PostAsync("Hola");
                }
                else
                {
                    await context.PostAsync("Hata pronto");
                }                
            }
            else
            {
                await context.PostAsync("Ópción no válida");
                await context.PostAsync(optionSaludar + " \n" + optionDesapedir);
            }

        }
    }
}