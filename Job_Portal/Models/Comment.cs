using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal.Models
{
    public class Comment
    {
        [Key]
        [Column("Id")] // Maps to the actual DB column name "Id"
        public int CommentId { get; set; }

        public int PostId { get; set; }

        [Required]
        [StringLength(100)]
        public string AuthorName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostedAt { get; set; }

        // Optional navigation property
        public Post Post { get; set; }
    }
}
