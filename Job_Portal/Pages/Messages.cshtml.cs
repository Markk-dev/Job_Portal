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
                    SenderName = "You" 
                });
            }

            return RedirectToPage("Messages", new { conversationId });
        }

        private void LoadConversations()
        {
            Conversations = new List<Conversation>
            //For now we'll be using this as placeholder data
            {
                new Conversation { Id = 1, RecipientName = "Mark Louie Alvarez", RecipientTitle = "Team Developer", LastMessage = "Hey, I just pushed my PR for....", Messages = new List<Message>
                    {
                        new Message { Content = "Hey, I just pushed my PR for review. When you get a chance, can you take a look?", IsFromCurrentUser = false, SenderName = "Mark Louie Alvarez" },
                        new Message { Content = "Yep, I see it. I’ll go through it shortly and leave some comments if needed.", IsFromCurrentUser = true, SenderName = "You" }
                    }
                },
                new Conversation { Id = 2, RecipientName = "Carl Kenzo Benavente", RecipientTitle = "Front-end Developer", LastMessage = "Hey, I ran into a weird....", Messages = new List<Message>
                    {
                        new Message { Content = "Hey, I ran into a weird bug in the payment flow. The total amount sometimes doubles on checkout. Could you check this thing out, Thanks!", IsFromCurrentUser = false, SenderName = "Carl Kenzo Benavente" }
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
