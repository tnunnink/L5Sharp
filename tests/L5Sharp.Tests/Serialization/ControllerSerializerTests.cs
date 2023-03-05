using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Serialization;
using Task = System.Threading.Tasks.Task;

namespace L5Sharp.Tests.Serialization
{
    [TestFixture]
    public class ControllerSerializerTests
    {
        private ControllerSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new ControllerSerializer();
        }

        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var controller = new Controller { Name = "Test", ProcessorType = "1756-L74", Revision = new Revision() };

            var serialized = _serializer.Serialize(controller);

            serialized.Should().NotBeNull();
        }

        [Test]
        
        public Task Serialize_WhenCalled_ShouldNotBeApproved()
        {
            var controller = new Controller
            {
                Name = "Test",
                ProcessorType = "1756-L74E", 
                Revision = new Revision(),
                ProjectCreationDate = new DateTime(2023, 1, 1, 12, 0, 0),
                LastModifiedDate = new DateTime(2023, 2, 1, 12, 0, 0)
            };

            var serialized = _serializer.Serialize(controller);

            return Verify(serialized.ToString());
        }
    }
}