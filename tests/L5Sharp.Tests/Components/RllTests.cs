using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class RllTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var routine = new Rll();

            routine.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldBeEmpty()
        {
            var routine = new Rll();

            routine.Should().BeEmpty();
        }

        [Test]
        public void New_WithRungs_ShouldHaveExpectedCount()
        {
            var routine = new Rll("Test", Scope.Program)
            {
                { "NOP", "This is a test rung" },
                "[OTE(SomeTagName)];",
                { "Not Real", "This is a fake", RungType.InsertReplace }
            };

            routine.Name.Should().Be("Test");
            routine.Should().HaveCount(3);
        }
    }
}