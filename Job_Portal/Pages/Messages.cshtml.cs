using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace Job_Portal.Pages
{
    public class MessagesModel : PageModel
    {
        public List<Conversation> Conversations { get; set; } = new List<Conversation>();
        public Conversation? SelectedConversation { get; set; }

        [BindProperty]
        public string NewMessageContent { get; set; } = string.Empty;

        public void OnGet(int? conversationId)
        {
            LoadConversations();

            if (conversationId.HasValue)
            {
                SelectedConversation = Conversations.FirstOrDefault(c => c.Id == conversationId.Value);
            }
        }

        public IActionResult OnPostSendMessage(int conversationId)
        {
            LoadConversations();

            var conversation = Conversations.FirstOrDefault(c => c.Id == conversationId);
            if (conversation != null && !string.IsNullOrWhiteSpace(NewMessageContent))
            {
                conversation.Messages.Add(new Message
                {
                    Content = NewMessageContent,
                    IsFromCurrentUser = true,
                    SenderName = "You" // Ideally, fetch from the logged-in user
                });
            }

            return RedirectToPage("Messages", new { conversationId });
        }

        private void LoadConversations()
        {
            Conversations = new List<Conversation>
            {
                new Conversation { Id = 1, RecipientName = "John Doe", RecipientTitle = "Recruiter", LastMessage = "Hello, how are you?", Messages = new List<Message>
                    {
                        new Message { Content = "Hello, how are you?", IsFromCurrentUser = false, SenderName = "John Doe" },
                        new Message { Content = "I'm good, thanks!", IsFromCurrentUser = true, SenderName = "You" }
                    }
                },
                new Conversation { Id = 2, RecipientName = "Jane Smith", RecipientTitle = "Hiring Manager", LastMessage = "Are you available for an interview?", Messages = new List<Message>
                    {
                        new Message { Content = "Are you available for an interview?", IsFromCurrentUser = false, SenderName = "Jane Smith" }
                    }
                }
            };
        }
    }

    public class Conversation
    {
        public int Id { get; set; }
        public string RecipientName { get; set; } = string.Empty;
        public string RecipientTitle { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new List<Message>();
        public bool IsSelected { get; set; } = false;
    }

    public class Message
    {
        public string Content { get; set; } = string.Empty;
        public bool IsFromCurrentUser { get; set; }
        public string SenderName { get; set; } = string.Empty;
    }
}
