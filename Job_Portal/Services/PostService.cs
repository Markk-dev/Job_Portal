using MySqlConnector;

public class PostService
{
    private readonly string _connectionString;

    public PostService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> HasUserLikedPost(int postId, string username)
    {
        using var con = new MySqlConnection(_connectionString);
        await con.OpenAsync();
        var cmd = new MySqlCommand("SELECT 1 FROM PostLikes WHERE PostId = @postId AND Username = @username", con);
        cmd.Parameters.AddWithValue("@postId", postId);
        cmd.Parameters.AddWithValue("@username", username);
        var result = await cmd.ExecuteScalarAsync();
        return result != null;
    }

    public async Task ToggleLikeAsync(int postId, string username)
    {
        using var con = new MySqlConnection(_connectionString);
        await con.OpenAsync();

        using var transaction = await con.BeginTransactionAsync();

        try
        {
            var checkCmd = new MySqlCommand("SELECT Id FROM PostLikes WHERE PostId = @postId AND Username = @username", con);
            checkCmd.Transaction = (MySqlTransaction)transaction;
            checkCmd.Parameters.AddWithValue("@postId", postId);
            checkCmd.Parameters.AddWithValue("@username", username);

            var result = await checkCmd.ExecuteScalarAsync();

            if (result != null)
            {
              
                var delCmd = new MySqlCommand("DELETE FROM PostLikes WHERE PostId = @postId AND Username = @username", con);
                delCmd.Transaction = (MySqlTransaction)transaction;
                delCmd.Parameters.AddWithValue("@postId", postId);
                delCmd.Parameters.AddWithValue("@username", username);
                await delCmd.ExecuteNonQueryAsync();

                var decCmd = new MySqlCommand("UPDATE Posts SET LikeCount = LikeCount - 1 WHERE Id = @postId", con);
                decCmd.Transaction = (MySqlTransaction)transaction;
                decCmd.Parameters.AddWithValue("@postId", postId);
                await decCmd.ExecuteNonQueryAsync();
            }
            else
            {
               
                var insertCmd = new MySqlCommand("INSERT INTO PostLikes (PostId, Username) VALUES (@postId, @username)", con);
                insertCmd.Transaction = (MySqlTransaction)transaction;
                insertCmd.Parameters.AddWithValue("@postId", postId);
                insertCmd.Parameters.AddWithValue("@username", username);
                await insertCmd.ExecuteNonQueryAsync();

                var incCmd = new MySqlCommand("UPDATE Posts SET LikeCount = LikeCount + 1 WHERE Id = @postId", con);
                incCmd.Transaction = (MySqlTransaction)transaction;
                incCmd.Parameters.AddWithValue("@postId", postId);
                await incCmd.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
