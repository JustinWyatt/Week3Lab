using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web.Http;
using Week3Lab.Models;

namespace Week3Lab.Controllers
{
    public class Message
    {
        public string ActualMessage { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public string Subject { get; set; }
        public int ID { get; set; }
    }

    public class MessageController : ApiController
    {
           
        private List<Message> GetMessages()
        {
            MemoryCache memoryCache = MemoryCache.Default;
            var messages = (List<Message>)memoryCache.Get("messages");
            if (messages == null)
            {
                messages = new List<Message>()
                {
                    new Message { ID = 1, ActualMessage = "Hello", Author = "Justin", DatePosted = DateTime.Now, Subject = "What's Up" },
                    new Message { ID = 2, ActualMessage = "News", Author = "Mike", DatePosted = DateTime.Parse("1/2/2013"), Subject = "Good News" },
                    new Message { ID = 3, ActualMessage = "Elections", Author = "Tom", DatePosted = DateTime.Parse("3/14/2012"), Subject = "Next Prez" }
                };
                memoryCache.Set("messages", messages, DateTimeOffset.Now.AddHours(10));
            }

            return messages;

        }

        private void SaveMessages(List<Message> messages)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set("messages", messages, DateTimeOffset.Now.AddHours(10));
        }


        [HttpPost]
        public IHttpActionResult CreateMessage([FromBody]Message m)
        {
            var messages = GetMessages();
            m.ID = messages.Max(x => x.ID) +1;
            messages.Add(m);
            return Ok(m);

        }

        [HttpGet]
        public IHttpActionResult GetTheMessage()
        {
            var messages = GetMessages();
            return Ok(messages);
        }

    }
}
