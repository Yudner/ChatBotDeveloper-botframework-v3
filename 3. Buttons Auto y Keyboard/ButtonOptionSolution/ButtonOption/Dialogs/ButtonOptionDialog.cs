using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ButtonOption.Dialogs
{
    [Serializable]
    public class ButtonOptionDialog : IDialog
    {
        const string SiOption = "SI";
        const string NoOption = "NO";
        int count = 0;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            if (count == 0)
            {
                PromptDialog.Choice(
                            context,
                            this.Selection,
                            new[] { SiOption, NoOption },
                                    "Quieres hacer una pregunta?",
                                    "Selecciona una Opción",
                                    promptStyle: PromptStyle.Keyboard);
                count++;
            }
            else
            {
                PromptDialog.Choice(
                            context,
                            this.Selection,
                            new[] { SiOption, NoOption },
                                    "Quieres hacer otra pregunta?",
                                    "Selecciona una Opción",
                                    promptStyle: PromptStyle.Auto);
            }
        }

        private async Task Selection(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var selection = await result;

                switch (selection)
                {
                    case SiOption:

                        await context.PostAsync("Ingresa tu Pregunta");
                        context.Wait(MessageReceived);
                        break;

                    case NoOption:
                        await context.PostAsync("Que tengas un buen día");
                        context.Wait(MessageReceived);
                        break;
                }
            }
            catch (Exception e)
            {
                await context.PostAsync(e.Message);
            }
        }

    }
}