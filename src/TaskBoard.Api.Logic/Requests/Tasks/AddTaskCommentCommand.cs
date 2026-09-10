using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using TaskBoard.Api.Logic.Models;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Repositories;

namespace TaskBoard.Api.Logic.Requests.Tasks;

public class AddTaskCommentCommand : IRequest<TaskCommentDto?>
{
    public Guid ProjectId { get; init; }
    public Guid TaskId { get; init; }
    public string Content { get; init; } = string.Empty;
}

public class AddTaskCommentCommandHandler : IRequestHandler<AddTaskCommentCommand, TaskCommentDto?>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskCommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public AddTaskCommentCommandHandler(
        ITaskRepository taskRepository,
        ITaskCommentRepository commentRepository,
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<TaskCommentDto?> Handle(
        AddTaskCommentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null || task.ProjectId != request.ProjectId)
            return null;

        var userIdValue = _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value
            ?? _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdValue, out var userId))
            throw new UnauthorizedAccessException("User not authenticated.");

        var comment = new TaskComment
        {
            Id = Guid.NewGuid(),
            TaskItemId = request.TaskId,
            UserId = userId,
            Content = request.Content.Trim(),
            DateCreated = DateTime.UtcNow
        };

        await _commentRepository.InsertAsync(comment);
        var user = await _userRepository.GetByIdAsync(userId);
        comment.UserName = user == null ? string.Empty : $"{user.FirstName} {user.LastName}";
        return _mapper.Map<TaskCommentDto>(comment);
    }
}
