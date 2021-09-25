using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Tests
{
    [TestFixture]
    public class BuilderApiTests
    {
        [Test]
        public void ControllerBuilder()
        {
            var controller = new Controller();

            controller.Create()
                .DataType("SomeType", "Description")
                .WithMember("Member", "Bool");
        }
    }
}