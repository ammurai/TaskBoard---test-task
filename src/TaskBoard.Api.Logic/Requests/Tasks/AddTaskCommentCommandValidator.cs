using FluentValidation;

namespace TaskBoard.Api.Logic.Requests.Tasks;

public class AddTaskCommentCommandValidator : AbstractValidator<AddTaskCommentCommand>
{
    public AddTaskCommentCommandValidator()
    {
        RuleFor(x => x.Content)
            .Must(content => !string.IsNullOrWhiteSpace(content))
            .WithMessage("Comment cannot be empty.")
            .MaximumLength(4000)
            .WithMessage("Comment cannot exceed 4000 characters.");
    }
}
