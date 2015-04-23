using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessageBoard.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string PostTime { get; set; }
        public string TextMessage { get; set; }
    }
}