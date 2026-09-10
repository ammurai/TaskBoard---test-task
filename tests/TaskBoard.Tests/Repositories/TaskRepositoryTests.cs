using FluentAssertions;
using Microsoft.Data.SqlClient;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Repositories;
using Xunit;

namespace TaskBoard.Tests.Repositories;

public class TaskRepositoryTests
{
    private const string ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TaskBoard;Trusted_Connection=True;MultipleActiveResultSets=true";

    [Fact]
    public async Task InsertAsync_ShouldInsertTaskSuccessfully()
    {
        var repo = new TaskRepository(ConnectionString);
        using var conn = new SqlConnection(ConnectionString);
        await conn.OpenAsync();
        using var tx = conn.BeginTransaction();

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            ProjectId = Guid.Parse("C1000000-0000-0000-0000-000000000001"),
            Title = "Test Task",
            Description = "Test Description",
            Status = "Todo",
            Priority = "Normal",
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedByUserId = Guid.Parse("B1000000-0000-0000-0000-000000000001")
        };

        var insertedId = await repo.InsertAsync(task, tx);
        tx.Rollback();

        insertedId.Should().Be(task.Id);
    }
}
