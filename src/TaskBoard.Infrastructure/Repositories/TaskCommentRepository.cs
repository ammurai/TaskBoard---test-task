using Dapper;
using Microsoft.Data.SqlClient;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Repositories;

public interface ITaskCommentRepository
{
    Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId);
    Task InsertAsync(TaskComment comment, SqlTransaction? transaction = null);
}

public class TaskCommentRepository : SqlRepositoryBase, ITaskCommentRepository
{
    public TaskCommentRepository(string connectionString) : base(connectionString) { }

    public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId)
    {
        var sql = LoadSql("TaskComments_GetByTaskId.sql");
        using var conn = CreateConnection();
        return await conn.QueryAsync<TaskComment>(sql, new { TaskItemId = taskId });
    }

    public async Task InsertAsync(TaskComment comment, SqlTransaction? transaction = null)
    {
        var sql = LoadSql("TaskComments_Insert.sql");
        var parameters = new
        {
            Id = comment.Id,
            TaskItemId = comment.TaskItemId,
            UserId = comment.UserId,
            Content = comment.Content
        };

        if (transaction != null)
        {
            await transaction.Connection!.ExecuteAsync(sql, parameters, transaction);
            return;
        }

        using var conn = CreateConnection();
        await conn.ExecuteAsync(sql, parameters);
    }
}
