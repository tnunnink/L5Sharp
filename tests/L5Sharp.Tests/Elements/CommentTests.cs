using FluentAssertions;

namespace L5Sharp.Tests.Elements;

[TestFixture]
public class CommentTests
{
    [Test]
    public void New_NullName_ShouldThrowException()
    {
        FluentActions.Invoking(() => new Comment(null!)).Should().Throw<ArgumentNullException>();
    }
    
    [Test]
    public void New_EmptyName_ShouldNotBeNull()
    {
        var comment = new Comment(string.Empty);

        comment.Should().NotBeNull();
    }
    
    [Test]
    public void New_Name_ShouldNotBeNull()
    {
        var comment = new Comment(".Member");

        comment.Should().NotBeNull();
        comment.Operand.Should().Be(".Member");
    }

    [Test]
    public void New_Default_ShouldBeExpected()
    {
        var comment = new Comment("Test", "This is the comment value.");

        comment.Operand.Should().Be("Test");
        comment.Value.Should().Be("This is the comment value.");
    }
}