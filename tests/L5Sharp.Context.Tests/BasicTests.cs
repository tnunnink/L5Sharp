using FluentAssertions;
using L5Sharp.Core;
using NUnit.Framework;

namespace L5Sharp.Context.Tests
{
    [TestFixture]
    public class BasicTests
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
        
        [Test]
        public void KnownTemplateFileShouldExists()
        {
            FileAssert.Exists(Known.Template);
        }

        [Test]
        public void Content_ValidFile_ShouldHaveExpectedProperties()
        {
            var context = L5XContext.Load(Known.L5X);

            context.L5X.SchemaRevision.Should().Be(new Revision());
            context.L5X.SoftwareRevision.Should().Be(new Revision(32, 2));
            context.L5X.TargetName.Should().Be(new ComponentName("TestController"));
            context.L5X.TargetType.Should().Be("Controller");
            context.L5X.ContainsContext.Should().BeFalse();
            context.L5X.Owner.Should().Be("tnunnink, EN Engineering");
            context.L5X.ExportDate.Year.Should().BeGreaterOrEqualTo(2021);
        }

        [Test]
        public void Controller_WhenCalled_ShouldNotBeNull()
        {
            var context = L5XContext.Load(Known.L5X);

            var controller = context.Controller;

            controller.Should().NotBeNull();
        }
        
        [Test]
        public void Controller_WhenCalled_ShouldBeExpected()
        {
            var context = L5XContext.Load(Known.L5X);

            var controller = context.Controller;

            controller.Name.Should().Be("TestController");
            controller.ProcessorType.Should().Be(new CatalogNumber("1756-L83E"));
            controller.Revision.Should().Be(new Revision(32, 11));
            controller.Description.Should().Be("This is a test project");
        }
    }
}