// Models/Message.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }

        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsFromCurrentUser { get; set; }
    }
}
