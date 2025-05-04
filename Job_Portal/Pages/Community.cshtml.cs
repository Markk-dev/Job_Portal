using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Job_Portal.Data;
using Job_Portal.Models;

namespace Job_Portal.Pages
{
    public class CommunityModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly CommentService _commentService;

        public CommunityModel(ApplicationDbContext context, CommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        [BindProperty]
        public string NewPostContent { get; set; }

        [BindProperty]
        public string NewCommentContent { get; set; } // For adding new comment

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Category> Categories { get; set; } = new List<Category>
        {
            new Category { Id = 1, Name = "Consulting", Description = "Providing expert insights and strategies to help businesses improve efficiency, solve problems, and achieve growth." },
            new Category { Id = 2, Name = "Advertising", Description = "Crafting creative campaigns and marketing strategies to boost brand awareness and attract customers." },
            new Category { Id = 3, Name = "Finance", Description = "Managing financial resources, investments, and planning to ensure stability and long-term growth." },
            new Category { Id = 4, Name = "Accounting", Description = "Recording, analyzing, and maintaining financial transactions to ensure accuracy and regulatory compliance." }
        };

        public async Task OnGetAsync()
        {
            // Fetch posts from the database
            Posts = _context.Posts.OrderByDescending(p => p.PostedAt).ToList();

            // Get comments for each post
            foreach (var post in Posts)
            {
                post.Comments = await _commentService.GetCommentsByPostId(post.Id);
            }
        }

        public IActionResult OnPost()
        {
            string currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrWhiteSpace(currentUsername))
                return RedirectToPage("/Auth/Login");

            if (!string.IsNullOrWhiteSpace(NewPostContent))
            {
                var newPost = new Post
                {
                    AuthorName = currentUsername,
                    Content = NewPostContent,
                    LikeCount = 0,
                    CommentCount = 0,
                    PostedAt = DateTime.Now
                };
                _context.Posts.Add(newPost);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostLikePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                post.LikeCount++;
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDeletePost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                string sessionUsername = HttpContext.Session.GetString("username");
                if (!post.CanBeDeletedBy(sessionUsername))
                    return Forbid();

                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostEditPost(int postId)
        {
            var post = _context.Posts.Find(postId);
            if (post != null)
            {
                post.Content = "[Edited] " + post.Content;
                _context.SaveChanges();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCommentAsync(int postId)
        {
            string currentUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrWhiteSpace(currentUsername))
                return RedirectToPage("/Auth/Login");

            if (!string.IsNullOrWhiteSpace(NewCommentContent))
            {
                var newComment = new Comment
                {
                    PostId = postId,
                    AuthorName = currentUsername,
                    Content = NewCommentContent,
                    PostedAt = DateTime.Now
                };

                await _commentService.AddComment(newComment);
            }

            return RedirectToPage();
        }
    }
}
