using Job_Portal.Data;
using Job_Portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal.Pages;

public class MessagesModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public MessagesModel(ApplicationDbContext db) => _db = db;

    public List<ConversationViewModel> Conversations { get; set; } = new();
    public ConversationViewModel? SelectedConversation { get; set; }

    [BindProperty]
    public string NewMessageContent { get; set; } = string.Empty;

    [BindProperty]
    public string ReceiverUsername { get; set; } = string.Empty;

    public IActionResult OnGet(int? conversationId)
    {
        var currentUser = HttpContext.Session.GetString("username");
        if (string.IsNullOrEmpty(currentUser))
            return RedirectToPage("/Auth/Login");

        var conversations = _db.Conversations
            .Include(c => c.Messages)
            .Where(c => c.User1 == currentUser || c.User2 == currentUser)
            .ToList();

        Conversations = conversations.Select(c => new ConversationViewModel
        {
            Id = c.Id,
            RecipientName = c.User1 == currentUser ? c.User2 : c.User1,
            LastMessage = c.Messages.OrderByDescending(m => m.SentAt).FirstOrDefault()?.Content ?? "No messages yet",
            Messages = c.Messages.Select(m => new MessageViewModel
            {
                Content = m.Content,
                SenderName = m.SenderName,
                IsFromCurrentUser = m.SenderName == currentUser
            }).ToList(),
            IsSelected = (conversationId.HasValue && c.Id == conversationId.Value)
        }).ToList();

        SelectedConversation = Conversations.FirstOrDefault(c => c.IsSelected);
        return Page();
    }

    public IActionResult OnPostSendMessage(int conversationId)
    {
        var currentUser = HttpContext.Session.GetString("username");
        if (string.IsNullOrEmpty(currentUser))
            return RedirectToPage("/Auth/Login");

        if (string.IsNullOrWhiteSpace(NewMessageContent))
            return RedirectToPage("/Messages", new { conversationId });

        var conversation = _db.Conversations
            .Include(c => c.Messages)
            .FirstOrDefault(c => c.Id == conversationId);

        if (conversation != null)
        {
            var message = new Message
            {
                ConversationId = conversationId,
                Content = NewMessageContent,
                SenderName = currentUser,
                IsFromCurrentUser = true,
                SentAt = DateTime.Now
            };

            _db.Messages.Add(message);
            _db.SaveChanges();
        }

        return RedirectToPage("/Messages", new { conversationId });
    }

    public IActionResult OnPostStartConversation()
    {
        var currentUser = HttpContext.Session.GetString("username");
        if (string.IsNullOrEmpty(currentUser) || string.IsNullOrWhiteSpace(ReceiverUsername))
            return RedirectToPage("/Messages");

        if (ReceiverUsername == currentUser)
            return RedirectToPage("/Messages");

        var existing = _db.Conversations.FirstOrDefault(c =>
            (c.User1 == currentUser && c.User2 == ReceiverUsername) ||
            (c.User1 == ReceiverUsername && c.User2 == currentUser));

        if (existing != null)
            return RedirectToPage("/Messages", new { conversationId = existing.Id });

        var newConversation = new Conversation
        {
            User1 = currentUser,
            User2 = ReceiverUsername
        };

        _db.Conversations.Add(newConversation);
        _db.SaveChanges();

        return RedirectToPage("/Messages", new { conversationId = newConversation.Id });
    }

    public class ConversationViewModel
    {
        public int Id { get; set; }
        public string RecipientName { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public List<MessageViewModel> Messages { get; set; } = new();
        public bool IsSelected { get; set; }
        public string RecipientTitle => "User"; // Replace with real title from user profile if available
    }

    public class MessageViewModel
    {
        public string Content { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public bool IsFromCurrentUser { get; set; }
    }
}
