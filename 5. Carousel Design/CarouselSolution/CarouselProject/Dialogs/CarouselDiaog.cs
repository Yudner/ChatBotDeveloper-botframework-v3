using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace CarouselProject.Dialogs
{
    [Serializable]
    public class CarouselDiaog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceived);
        }

        private async Task MessageReceived(IDialogContext context, IAwaitable<object> result)
        {
            var reply = context.MakeMessage();
            reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;

            reply.Attachments = GetCards();

            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }


        private static IList<Attachment> GetCards()
        {
            return new List<Attachment>()
            {
                GetHeroCard(),
                GetThumbnailCard()
            };
        }
        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Tarjeta URL",
                Subtitle = "Mi tarjeta",
                Text = "mi texto",
                Images = new List<CardImage> { new CardImage("https://static1.squarespace.com/static/587d02a986e6c0d1e3188b28/5883d6a946c3c44ae80fb517/589cb78b197aea0ba4b343fc/1486665612739/home-home-icon-80046+copy+copy.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Ir a la web", value: "https://docs.microsoft.com/bot-framework") }
            };
            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Tarjeta URL",
                Subtitle = "Mi tarjeta",
                Text = "mi texto",
                Images = new List<CardImage> { new CardImage("http://www.asesoria-madrid.com/imagenes/iconos_web/centro-de-negocios-madrid.png") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Ir a la web", value: "https://docs.microsoft.com/bot-framework")  }
            };
            return heroCard.ToAttachment();
        }


    }
}