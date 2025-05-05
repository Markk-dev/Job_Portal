using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        [Required]
        public string User1 { get; set; } = string.Empty; // current user
        [Required]
        public string User2 { get; set; } = string.Empty; // recipient

        public virtual List<Message> Messages { get; set; } = new();
    }
}
