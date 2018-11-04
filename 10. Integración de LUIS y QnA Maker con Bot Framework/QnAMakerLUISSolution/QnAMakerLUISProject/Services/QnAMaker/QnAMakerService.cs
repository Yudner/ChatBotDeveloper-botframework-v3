using Newtonsoft.Json;
using QnAMakerLUISProject.Constants;
using RestSharp;

namespace QnAMakerLUISProject.Services.QnAMaker
{
    public class QnAMakerService
    {
        public string GetAnswer(string query)
        {
            var client = new RestClient(QnaMakerConstants.Host + "/knowledgebases/" + QnaMakerConstants.KnowledgeBaseId + "/generateAnswer");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "EndpointKey " + QnaMakerConstants.EndPointKey);
            request.AddParameter(QnaMakerConstants.FormatJson, "{\"question\": \"" + query + "\"}", ParameterType.RequestBody);
            var response = client.Execute(request);

            var result = JsonConvert.DeserializeObject<QnAMakerModel>(response.Content);

            if (result.Answers.Count > 0)
            {
                var respuesta = result.Answers[0].Answer;
                var score = result.Answers[0].Score;
                if (!respuesta.ToLower().Equals(QnaMakerConstants.AnswerNotFound) && score > 40)
                    return respuesta;
            }
            return QnaMakerConstants.AnswerNotFound;
        }
    }
}