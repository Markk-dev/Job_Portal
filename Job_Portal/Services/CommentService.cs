using Job_Portal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Job_Portal.Data;
public class CommentService
{
    private readonly ApplicationDbContext _context;

    public CommentService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Comment>> GetCommentsByPostId(int postId)
    {
        return await _context.Comments
                             .Where(c => c.PostId == postId)
                             .OrderBy(c => c.PostedAt)
                             .ToListAsync();
    }

    public async Task AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateComment(int commentId, string newContent)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            comment.Content = newContent;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteComment(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}


