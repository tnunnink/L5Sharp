using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class BasicFileTests
    {
        [Test]
        public void KnownTestFileShouldExists()
        {
            FileAssert.Exists(Known.L5X);
        }

        [Test]
        public void Load_ValidFile_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            context.Should().NotBeNull();
        }

        /*[Test]
        public void Content_ValidFile_ShouldHaveExpectedProperties()
        {
            var context = L5XContext.Load(Known.L5X);

            context.Info.SchemaRevision.Should().Be(new Revision());
            context.Info.SoftwareRevision.Should().Be(new Revision(32, 2));
            context.Info.TargetName.Should().Be(new ComponentName("TestController"));
            context.Info.TargetType.Should().Be("Controller");
            context.Info.ContainsContext.Should().BeFalse();
            context.Info.Owner.Should().Be("tnunnink, EN Engineering");
            context.Info.ExportDate.Year.Should().BeGreaterOrEqualTo(2021);
        }*/

        [Test]
        public void Controller_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var controller = context.Controller();

            controller.Should().NotBeNull();
        }
        
        [Test]
        public void Controller_WhenCalled_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.L5X);

            var controller = context.Controller()!;

            controller.Name.Should().Be("TestController");
            controller.ProcessorType.Should().Be(new CatalogNumber("1756-L83E"));
            controller.Revision.Should().Be(new Revision(32, 11));
            controller.Description.Should().Be("This is a test project");
        }
    }
}