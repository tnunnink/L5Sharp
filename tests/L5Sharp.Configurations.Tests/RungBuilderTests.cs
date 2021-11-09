using FluentAssertions;
using L5Sharp.Builders;
using L5Sharp.Core;
using L5Sharp.Instructions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Configurations.Tests
{
    [TestFixture]
    public class RungBuilderTests
    {
        [Test]
        public void Do_Valid_ShouldBeExpect()
        {
            //MOV.Of(1000, new Tag("Member", Logix.DataType.Dint))
            var b1 = RungBuilder.New(1).Do("Step1").Compile().Build();

            b1.Should().NotBeNull();
        }
        
        [Test]
        public void MoreComplex_Valid_ShouldBeExpect()
        {
            var b2 = RungBuilder.New(2).When("Input").Then("Output").Compile().Build();

            b2.Should().NotBeNull();
        }
        
        [Test]
        public void Branch_Valid_ShouldBeExpect()
        {
            var b2 = RungBuilder.New(2)
                .When("XIC(bit1)")
                .Or(b => b.When("XIO(bit1)").And("XIO(bit2)"))
                .Or(b => b.When("XIO(bit1)").And("XIC(bit2)"))
                .Then("OTE(out1)")
                .And("OTU(out2)")
                .Compile()
                .Build();

            b2.Should().NotBeNull();
        }
        
        [Test]
        public void Branch2_Valid_ShouldBeExpect()
        {
            var b2 = RungBuilder.New(2, "This is a test rung comment")
                .When("XIC(bit1)").And("XI0(bit2)").Then("OTL(bit3)").And("OTU(bit1)")
                .Compile().Build();
            
            b2.Should().NotBeNull();
        }

        [Test]
        public void AnotherPlease()
        {
            var b = RungBuilder.New(0, "This is a test")
                .When(new XIO().Of(Tag.OfType<Bool>("Test")).Signature)
                .Then("OTL(b2)")
                .And(b => b.When("XIO(b3)").Then("OTU(b1)"))
                .Compile().Build();

            var result = b.Text;
            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void METHOD()
        {
            /*var rung = RungBuilder.New(1, "This is a test rung")
               .When(XIC.Of(new Tag<Bool>("Test1")))
               .And(XIC.Of(new Tag<Bool>("Test2")))
               .Then(MOV.Of(10, new Tag("Test", Logix.DataType.Bool)))
               .Compile()
               .Build();*/

            var tag = Tag.OfType<Int>("Test");

            var b1 = new XIO().Of(Tag.OfType<Bool>("Test"));
            var text = new MOV().Of(new Dint(1000), Tag.Atomic<Dint>("Test"));


            var b3 = RungBuilder.New(2)
                .When("XIC(bit)")
                .And("XIC(bit)")
                .Or(b => b.When("XIC(bit)")
                    .And("XIC(bit)")
                    .Then("OTE(bit)"))
                .Or(b => b.Do("OTU(bit)")
                    .And("XIC(bit)"))
                .Or(b => b.When("XIC(bit)")
                    .Or(inner => inner.When("XIC(bit)").Then("OTL(bit)"))
                    .Then("OTL(bit)"))
                .Then("OTU(bit)")
                .Compile()
                .Build();
            
            b3.Should().NotBeNull();
            
            
        }
    }
}