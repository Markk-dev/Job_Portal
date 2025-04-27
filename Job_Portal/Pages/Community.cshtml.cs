using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Job_Portal.Models;

namespace Job_Portal.Pages
{
    public class CommunityModel : PageModel
    {
        [BindProperty]
        public string NewPostContent { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>
        {
            new Post
            {
                Id = 1,
                AuthorName = "Mark Louie Alvarez",
                AuthorColor = "#FFC107", 
                Content = "Deep in the code, fueled by coffee and logic. Debugging, building, and making things work—one keystroke at a time.",
                LikeCount = 5,
                CommentCount = 2,
                PostedAt = DateTime.Now.AddHours(-2)
            },
            new Post
            {
                Id = 2,
                AuthorName = "Mark Vincent Madrid",
                AuthorColor = "#FFC107",
                Content = "Alright guys, let's be real. How many times have you used just one more handle and ended up rewriting half the project? 😅 Currently deep in a coding session refactoring things I wrote just a couple hours ago.",
                LikeCount = 3,
                CommentCount = 1,
                PostedAt = DateTime.Now.AddHours(-4)
            },
            new Post
            {
                Id = 3,
                AuthorName = "Carl Kenzo Benavente",
                AuthorColor = "#FFC107",
                Content = "Spent the last 3 hours debugging a problem, only to realize the issue was a single missing bracket. At this point, I think my code is just messing with me for fun.",
                LikeCount = 7,
                CommentCount = 3,
                PostedAt = DateTime.Now.AddHours(-6)
            },
            new Post
            {
                Id = 4,
                AuthorName = "Justin Jude Almaden",
                AuthorColor = "#FFC107",
                Content = "When your code finally compiles without errors, but now it does the exact opposite of what you wanted!",
                LikeCount = 10,
                CommentCount = 5,
                PostedAt = DateTime.Now.AddHours(-8)
            }
        };

        public List<Category> Categories { get; set; } = new List<Category>
        {
            new Category
            {
                Id = 1,
                Name = "Consulting",
                Description = "Providing expert insights and strategies to help businesses improve efficiency," +
                "solve problems, and achieve growth."
            },
            new Category
            {
                Id = 2,
                Name = "Advertising",
                Description = " Crafting creative campaigns and marketing strategies to boost brand" +
                " awareness and attract customers."
            },
            new Category
            {
                Id = 3,
                Name = "Finance",
                Description = "Managing financial resources, investments, and planning to ensure" +
                " stability and long-term growth."
            },
            new Category
            {
                Id = 4,
                Name = "Accounting",
                Description = "Recording, analyzing, and maintaining financial transactions to ensure" +
                " accuracy and regulatory compliance."
            }
        };

        public void OnGet()
        {
          
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewPostContent))
            {
                var newPost = new Post
                {
                    Id = this.Posts.Count + 1,
                    AuthorName = "Current User", 
                    AuthorColor = "#4CAF50",
                    Content = NewPostContent,
                    LikeCount = 0,
                    CommentCount = 0,
                    PostedAt = DateTime.Now
                };

                this.Posts.Insert(0, newPost); 
                NewPostContent = string.Empty; 
            }

            return RedirectToPage();
        }

        public IActionResult OnPostLikePost(int postId)
        {
            var post = this.Posts.Find(p => p.Id == postId);
            if (post != null)
            {
                post.LikeCount++;
            }
            return RedirectToPage();
        }
    }
}

