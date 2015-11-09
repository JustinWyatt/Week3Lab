using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;


namespace ConsoleApplication12
{
    public class MessageResponse
    {
        public List<Message> messages { get; set; }
    }

    public class Message
    {

        public string ActualMessage { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public string Subject { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"\nActual Message: {ActualMessage},\nAuthor: {Author},\nDatePosted: {DatePosted},\nSubject: {Subject},\nCount: {Count}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Message userMessage = new Message();

            while (true)
            {
                Console.WriteLine("What is your name?");
                string userMessageName = Console.ReadLine();
                userMessage.Author = userMessageName;
                if (userMessageName == string.Empty)
                {
                    userMessage.Author = "Not provided";
                }

                Console.WriteLine("Type your subject");
                string userSubject = Console.ReadLine();
                userMessage.Subject = userSubject;
                if (userSubject == string.Empty)
                {
                    userMessage.Subject = "Not provided";
                }

                Console.WriteLine("Type your message");
                string userActualMessage = Console.ReadLine();
                userMessage.ActualMessage = userActualMessage;
                if (userActualMessage == string.Empty)
                {
                    userMessage.ActualMessage = "Not provided";
                }

                userMessage.DatePosted = DateTime.Now;
                break;
            }
            var client = new RestClient("http://localhost:58798");

            var finishedMessage = new RestRequest("api/message/add", Method.POST);
            //MessageResponse messageComplete = client.Execute<MessageResponse>(finishedMessage).Data;
            finishedMessage.RequestFormat = DataFormat.Json;
            finishedMessage.AddBody(userMessage);
            client.Execute(finishedMessage);
            Console.WriteLine($"Status Description: {client.Execute(finishedMessage).StatusDescription}");
            Console.WriteLine($"\n--User Message--\n {userMessage}");
            //var messageRequest = new RestRequest($"api/message", Method.GET);
            //MessageResponse message = client.Execute<MessageResponse>(messageRequest).Data;

            Console.ReadLine();
        }
    }
}

