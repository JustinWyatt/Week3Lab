using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Week3Lab.Models
{
    public class Message
    {
        public string ActualMessage { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public string Subject { get; set; }

    }

    public class JustinController : ApiController
    {
    }
}
