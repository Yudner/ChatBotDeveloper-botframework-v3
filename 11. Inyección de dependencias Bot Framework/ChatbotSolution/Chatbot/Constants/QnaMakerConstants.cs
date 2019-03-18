using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Constants
{
    public class QnaMakerConstants
    {
        public const string Host = "https://chatbot-qnamaker.azurewebsites.net/qnamaker";
        public const string KnowledgeBaseId = "b4b607fd-55cc-4092-8dc3-d030e5480c54";
        public const string EndPointKey = "2ffcf71b-4072-4647-90c3-a878e6d2d29f";

        public const string FormatJson = "application/json";
        public const string AnswerNotFound = "no good match found in kb.";
    }
}