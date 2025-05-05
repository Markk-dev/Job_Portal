using Job_Portal.Models;

public class PostLike
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Username { get; set; }
    public DateTime LikedAt { get; set; }

    public Post Post { get; set; }  
}
