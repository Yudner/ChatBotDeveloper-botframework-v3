using System;
using System.Threading.Tasks;
using BotFacebookAPI.Services.FacebookUserProfile;
using Microsoft.Bot.Builder.Dialogs;

namespace BotFacebookAPI.Dialogs
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
            var serviceFacebook = new GetUserProfile();
            var userData = await serviceFacebook.Execute(context.Activity.From.Id);
            if (userData != null)
            {
                await context.PostAsync("Id: " + userData.Id);
                await context.PostAsync("Name: "+ userData.Name);
                await context.PostAsync("First_Name: " + userData.First_name);
                await context.PostAsync("Last_Name: " + userData.Last_name);
                await context.PostAsync("Profile_Pic: " + userData.Profile_pic);
                await context.PostAsync("Locale: " + userData.Locale);
                await context.PostAsync("Timezone: " + userData.Timezone);
                await context.PostAsync("Gender: " + userData.Gender);
            }
        }
    }
}
