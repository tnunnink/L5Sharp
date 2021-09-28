using L5Sharp.Builders;
using L5Sharp.Builders.Abstractions;
using L5Sharp.Primitives;
using NUnit.Framework;

namespace L5Sharp.Builder.Tests
{
    [TestFixture]
    public class ControllerBuilder
    {
        [Test]
        public void Create()
        {
            var controller = new Controller("Test");
            
            controller.Create().DataType("Test", b => b.HasDescription("Tests"));
            
            controller.Create<DataType, IDataTypeBuilder>(() => new DataTypeBuilder("DataType"), 
                b => b.HasDescription("TEst"));
        }
    }
}