using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Constants
{
    public class QnaMakerConstants
    {
        public const string Host = "https://chatbot-qnamaker.azurewebsites.net/qnamaker";
        public const string KnowledgeBaseId = "44056855-251c-48d2-9679-7bb9cde7e696";
        public const string EndPointKey = "85d43489-f579-467f-984c-2cdbaf4be202";

        public const string FormatJson = "application/json";
        public const string AnswerNotFound = "no good match found in kb.";
    }
}