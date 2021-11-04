using L5Sharp.Builders;
using L5Sharp.Core;
using L5Sharp.Instructions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Configurations.Tests
{
    [TestFixture]
    public class RunBuilderTests
    {
        [Test]
        public void Scratch_Testing()
        {
            var rung = RungBuilder
                .New(1, "This is a test rung")
                .If(XIC.Of(new Tag<Bool>("Test1")))
                .And(XIC.Of(new Tag<Bool>("Test2")))
                .AreEnergized()
                .Then(MOV.Of(10, new Tag("Test", Logix.DataType.Bool)))
                .Return()
                .Build();
        }
    }
}