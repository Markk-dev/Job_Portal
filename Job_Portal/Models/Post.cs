using System;

namespace Job_Portal.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime PostedAt { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string GetTimeAgo()
        {
            var span = DateTime.Now - PostedAt;

            if (span.TotalDays > 1)
                return $"{(int)span.TotalDays}d ago";
            else if (span.TotalHours > 1)
                return $"{(int)span.TotalHours}h ago";
            else if (span.TotalMinutes > 1)
                return $"{(int)span.TotalMinutes}m ago";
            else
                return "Just now";


        }


        public bool CanBeDeletedBy(string username)
        {
            return !string.IsNullOrEmpty(username) && AuthorName == username;
        }
    }
}
