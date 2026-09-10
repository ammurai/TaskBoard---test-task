using FluentAssertions;
using TaskBoard.Api.Logic.Requests.Tasks;
using Xunit;

namespace TaskBoard.Tests.Validators;

public class AddTaskCommentCommandValidatorTests
{
    private readonly AddTaskCommentCommandValidator _validator = new();

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\r\n")]
    public void Validate_EmptyOrWhitespaceComment_ShouldFail(string content)
    {
        var result = _validator.Validate(new AddTaskCommentCommand { Content = content });

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == nameof(AddTaskCommentCommand.Content));
    }

    [Fact]
    public void Validate_CommentExceedingMaxLength_ShouldFail()
    {
        var result = _validator.Validate(new AddTaskCommentCommand
        {
            Content = new string('A', 4001)
        });

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.ErrorMessage.Contains("4000"));
    }
}
