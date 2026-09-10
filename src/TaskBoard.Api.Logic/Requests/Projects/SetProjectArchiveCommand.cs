using MediatR;
using TaskBoard.Api.Logic.Shared.Authorization;
using TaskBoard.Infrastructure.Repositories;

namespace TaskBoard.Api.Logic.Requests.Projects;

public class SetProjectArchiveCommand : IRequest<bool>, IRequirePermission
{
    public Guid ProjectId { get; init; }
    public bool IsArchived { get; init; }
    public string Permission => "ProjectManage";
}

public class SetProjectArchiveCommandHandler : IRequestHandler<SetProjectArchiveCommand, bool>
{
    private readonly IProjectRepository _projectRepository;

    public SetProjectArchiveCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<bool> Handle(
        SetProjectArchiveCommand request,
        CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);
        if (project == null)
            return false;

        await _projectRepository.SetArchivedAsync(request.ProjectId, request.IsArchived);
        return true;
    }
}
