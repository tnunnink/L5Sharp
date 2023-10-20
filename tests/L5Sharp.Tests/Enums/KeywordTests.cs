using FluentAssertions;
using L5Sharp.Enums;

namespace L5Sharp.Tests.Enums;

[TestFixture]
public class KeywordTests
{
    [Test]
    public void By_WhenCalled_ShouldNotBeNull()
    {
        Keyword.By.Should().NotBeNull();
    }

    [Test]
    public void Case_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Case.Should().NotBeNull();
    }

    [Test]
    public void Do_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Do.Should().NotBeNull();
    }

    [Test]
    public void Else_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Else.Should().NotBeNull();
    }

    [Test]
    public void ElsIf_WhenCalled_ShouldNotBeNull()
    {
        Keyword.ElsIf.Should().NotBeNull();
    }

    [Test]
    public void EndCase_WhenCalled_ShouldNotBeNull()
    {
        Keyword.EndCase.Should().NotBeNull();
    }

    [Test]
    public void EndFor_WhenCalled_ShouldNotBeNull()
    {
        Keyword.EndFor.Should().NotBeNull();
    }

    [Test]
    public void EndIf_WhenCalled_ShouldNotBeNull()
    {
        Keyword.EndIf.Should().NotBeNull();
    }

    [Test]
    public void EndRepeat_WhenCalled_ShouldNotBeNull()
    {
        Keyword.EndRepeat.Should().NotBeNull();
    }

    [Test]
    public void EndWhile_WhenCalled_ShouldNotBeNull()
    {
        Keyword.EndWhile.Should().NotBeNull();
    }

    [Test]
    public void Exit_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Exit.Should().NotBeNull();
    }

    [Test]
    public void For_WhenCalled_ShouldNotBeNull()
    {
        Keyword.For.Should().NotBeNull();
    }

    [Test]
    public void Goto_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Goto.Should().NotBeNull();
    }

    [Test]
    public void If_WhenCalled_ShouldNotBeNull()
    {
        Keyword.If.Should().NotBeNull();
    }

    [Test]
    public void Of_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Of.Should().NotBeNull();
    }

    [Test]
    public void Repeat_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Repeat.Should().NotBeNull();
    }

    [Test]
    public void Return_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Return.Should().NotBeNull();
    }

    [Test]
    public void Then_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Then.Should().NotBeNull();
    }

    [Test]
    public void This_WhenCalled_ShouldNotBeNull()
    {
        Keyword.This.Should().NotBeNull();
    }

    [Test]
    public void To_WhenCalled_ShouldNotBeNull()
    {
        Keyword.To.Should().NotBeNull();
    }

    [Test]
    public void Until_WhenCalled_ShouldNotBeNull()
    {
        Keyword.Until.Should().NotBeNull();
    }

    [Test]
    public void While_WhenCalled_ShouldNotBeNull()
    {
        Keyword.While.Should().NotBeNull();
    }
}