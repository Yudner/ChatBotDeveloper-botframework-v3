using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Services.QnAMaker
{
    public interface IQnAMakerService
    {
        string GetAnswer(string query);
    }
}