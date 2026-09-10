using AutoMapper;
using MediatR;
using TaskBoard.Api.Logic.Models;
using TaskBoard.Infrastructure.Repositories;

namespace TaskBoard.Api.Logic.Requests.Tasks;

public class GetTaskCommentsQuery : IRequest<IEnumerable<TaskCommentDto>?>
{
    public Guid ProjectId { get; init; }
    public Guid TaskId { get; init; }
}

public class GetTaskCommentsQueryHandler : IRequestHandler<GetTaskCommentsQuery, IEnumerable<TaskCommentDto>?>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskCommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public GetTaskCommentsQueryHandler(
        ITaskRepository taskRepository,
        ITaskCommentRepository commentRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskCommentDto>?> Handle(
        GetTaskCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.TaskId);
        if (task == null || task.ProjectId != request.ProjectId)
            return null;

        var comments = await _commentRepository.GetByTaskIdAsync(request.TaskId);
        return _mapper.Map<IEnumerable<TaskCommentDto>>(comments);
    }
}
