using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CardProject.Dialogs
{
    [Serializable]
    public class CardDialog : IDialog
    {
        const string HeroCard = "Hero Card";
        const string ThumbnailCard = "Thumbnail Card";
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            PromptDialog.Choice(context, Selection,new[] { HeroCard, ThumbnailCard}, "Seleciona una opción", promptStyle: PromptStyle.Auto);

        }

        private async Task Selection(IDialogContext context, IAwaitable<string> result)
        {
            var resultSelect = await result;
            var reply = context.MakeMessage();

            var attachment = GetSelectionCards(resultSelect);
            reply.Attachments.Add(attachment);

            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }

        private static Attachment GetSelectionCards(string selectCard)
        {
            switch (selectCard)
            {
                case HeroCard:
                    return GetHeroCard();

                case ThumbnailCard:
                    return GetThumbnailCard();
                default:
                    return GetHeroCard();
            }
        }

        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Tarjeta URL",
                Subtitle = "Mi tarjeta",
                Text = "mi texto",
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Ir a la web", value: "https://docs.microsoft.com/bot-framework") }
            };
            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard()
        {
            var heroCard = new ThumbnailCard
            {
                Title = "Tarjeta URL",
                Subtitle = "Mi tarjeta",
                Text = "mi texto",
                Images = new List<CardImage> { new CardImage("https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.PostBack, "opción", value: "opción") }
            };
            return heroCard.ToAttachment();
        }

    }
}