using LogixHelper.Primitives;
using NUnit.Framework;

namespace LogixHelper.Tests
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