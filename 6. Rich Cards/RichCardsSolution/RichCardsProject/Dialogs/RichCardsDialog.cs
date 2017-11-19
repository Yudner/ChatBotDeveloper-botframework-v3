using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace RichCardsProject.Dialogs
{
    [Serializable]
    public class RichCardsDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            //Llamar a las tarjetas
            var reply = context.MakeMessage();
                        
            reply.Attachments.Add(GetAnimationCard()); // Animación
            reply.Attachments.Add(GetImageCard()); // Imagen
            reply.Attachments.Add(GetVideoCard());// Video
            reply.Attachments.Add(GetAudioCard()); // Audio
            
            await context.PostAsync(reply);
        }
               
        private Attachment GetAnimationCard()
        {
            var animationCard = new AnimationCard
            {
                Title = "Animation",
                Subtitle = "Animation Development",
                Media = new List<MediaUrl>
                {
                    new MediaUrl(){ Url = "http://i.giphy.com/Ki55RUbOV5njy.gif"}
                }                
            };
            return animationCard.ToAttachment();
        }
        private Attachment GetImageCard()
        {
            var ImageCard = new HeroCard
            {
                Title="Image",
                Subtitle="Image Development",
                Images = new List<CardImage> { new CardImage("https://cdn-images-1.medium.com/max/1600/1*iRSQFBghm3mET4Pdcw9BiQ.jpeg") }                
            };
            return ImageCard.ToAttachment();
        }

        private Attachment GetVideoCard()
        {
            var videoCard = new VideoCard
            {
                Title = "Video",
                Subtitle = "Video Development",
                Media = new List<MediaUrl>
                {
                    new MediaUrl(){ Url = "http://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_320x180.mp4" }
                }
            };

            return videoCard.ToAttachment();
        }

        private Attachment GetAudioCard()
        {
            var AudioCard = new AudioCard
            {
                Title = "Audio",
                Subtitle = "Audio Development",
                Media = new List<MediaUrl>
                {
                    new MediaUrl(){ Url = "http://www.wavlist.com/movies/004/father.wav" }
                },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, title:"Ver más", value: "https://dev.botframework.com/")
                }
            };

            return AudioCard.ToAttachment();
        }
    }
}