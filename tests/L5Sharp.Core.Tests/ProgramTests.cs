using FluentAssertions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");

            program.Should().NotBeNull();
        }
        
        [Test]
        public void AddTag_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");

            program.AddTag("Test_Bool", Predefined.Bool);

            program.Tags.Should().Contain(t => t.Name == "Test_Bool");
        }

        [Test]
        public void GetTagTyped_AsCorrectType_ShouldNotBeNull()
        {
            var program = new Program("Test");
            program.AddTag("Test", Predefined.Bool);

            var tag = program.GetTag("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
        }

        [Test]
        public void GetTagTyped_FromNonGenericTag_ShouldNotBeNull()
        {
            var program = new Program("Test");
            program.AddTag("Test", Predefined.Dint);

            var tag = program.GetTag("Test");

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
        }
    }
}