using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SampleBotQnAMakerSolution.Dialogs
{
    [Serializable]
    [QnAMaker(authKey: "You authKey", knowledgebaseId: "you knowledgebaseId", defaultMessage: "Sin respuesta", scoreThreshold: 0.5, top: 1, endpointHostName: "you HostName")]
    public class RootDialog : QnAMakerDialog
    {

    }
}