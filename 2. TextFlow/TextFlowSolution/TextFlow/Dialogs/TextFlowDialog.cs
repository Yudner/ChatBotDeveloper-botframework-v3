using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TextFlow.Dialogs
{
    [Serializable]
    public class TextFlowDialog : IDialog
    {
        string optionSaludar = "1. Saludar";
        string optionDespedir = "2. Despedir";
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("Hola soy Tico, tu amigo Bot y Tengo estas opciones para ti");
            await context.PostAsync(optionSaludar + " \n" + optionDespedir);

            context.Wait(OptionsResult);
        }

        private async Task OptionsResult(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;

            if ((message.Text.Equals("Saludar")) || (message.Text.Equals("Despedir")))
            {
                if (message.Text.Equals("Saludar"))
                {
                    await context.PostAsync("Hola");                    
                    await context.PostAsync(optionSaludar + " \n" + optionDespedir);
                }
                if (message.Text.Equals("Despedir"))
                {
                    await context.PostAsync("Hasta pronto");
                }
            }
            else
            {
                await context.PostAsync("Opción no válida");
                await context.PostAsync(optionSaludar + " \n" + optionDespedir);
            }
        }

    }
}