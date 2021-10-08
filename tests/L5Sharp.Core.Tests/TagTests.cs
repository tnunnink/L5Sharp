using System.Linq;
using AutoFixture;
using FluentAssertions;
using L5Sharp.Abstractions;
using L5Sharp.Enumerations;
using L5Sharp.Exceptions;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void New_ValidTagName_ShouldNotBeNull()
        {
            var tag = new Tag("Test", Predefined.Bool);

            tag.Should().NotBeNull();
        }

        [Test]
        public void New_InvalidTagName_ShouldThrowInvalidNameException()
        {
            var fixture = new Fixture();

            FluentActions.Invoking(() => new Tag(fixture.Create<string>(), Predefined.Bool)).Should()
                .Throw<InvalidNameException>();
        }
        
        [Test]
        public void New_NoOverloads_ShouldHaveExpectedDefaults()
        {
            var tag = new Tag("Test", Predefined.Bool);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(Predefined.Bool.Name);
            tag.Dimension.Should().Be(Dimensions.Empty);
            tag.Radix.Should().Be(Radix.Decimal);
            tag.ExternalAccess.Should().Be(ExternalAccess.None);
            tag.Value.Should().Be(false);
            tag.Description.Should().BeEmpty();
            tag.TagType.Should().Be(TagType.Base);
            tag.Usage.Should().Be(TagUsage.Null);
            tag.Scope.Should().Be(Scope.Null);
            tag.AliasFor.Should().BeEmpty();
            tag.Constant.Should().BeFalse();
        }
        
        [Test]
        public void New_AllDataType_ShouldHaveExpectedDefaults()
        {
            var tag = new Tag("Test", Predefined.Dint, new Dimensions(5), Radix.Ascii, ExternalAccess.ReadOnly, 
                TagType.Alias, TagUsage.Local, "This is a test tag", Scope.Program, "Alias.Name", true);

            tag.Should().NotBeNull();
            tag.Name.Should().Be("Test");
            tag.DataType.Should().Be(Predefined.Dint.Name);
            tag.Dimension.Should().Be(new Dimensions(5));
            tag.Radix.Should().Be(Radix.Ascii);
            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            tag.Value.Should().Be(0);
            tag.Description.Should().Be("This is a test tag");
            tag.TagType.Should().Be(TagType.Alias);
            tag.Usage.Should().Be(TagUsage.Local);
            tag.Scope.Should().Be(Scope.Program);
            tag.AliasFor.Should().Be("Alias.Name");
            tag.Constant.Should().BeTrue();
        }

        [Test]
        public void New_Counter_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Predefined.Counter);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(7);
            tag.Members.Any(t => t.Name == "PRE").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ACC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CU").Should().BeTrue();
            tag.Members.Any(t => t.Name == "CD").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DN").Should().BeTrue();
            tag.Members.Any(t => t.Name == "OV").Should().BeTrue();
            tag.Members.Any(t => t.Name == "UN").Should().BeTrue();
        }
        
        [Test]
        public void New_Alarm_ShouldHaveValidMembers()
        {
            var tag = new Tag("Test", Predefined.Alarm);

            tag.Should().NotBeNull();
            tag.Members.Should().HaveCount(24);
            tag.Members.Any(t => t.Name == "EnableIn").Should().BeTrue();
            tag.Members.Any(t => t.Name == "In").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HHLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "Deadband").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegLimit").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPeriod").Should().BeTrue();
            tag.Members.Any(t => t.Name == "EnableOut").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HHAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "HAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "LLAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegAlarm").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROC").Should().BeTrue();
            tag.Members.Any(t => t.Name == "Status").Should().BeTrue();
            tag.Members.Any(t => t.Name == "InstructFault").Should().BeTrue();
            tag.Members.Any(t => t.Name == "DeadbandInv").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCPosLimitInv").Should().BeTrue();
            tag.Members.Any(t => t.Name == "ROCNegLimitInv").Should().BeTrue();
        }

        [Test]
        public void New_TwoDimensionalArray_ShouldHaveExpectedLength()
        {
            var fixture = new Fixture();
            var first = fixture.Create<ushort>();
            var second = fixture.Create<ushort>();
            var length = first * second;
            
            var tag = new Tag("Test", Predefined.Dint, new Dimensions(first, second));

            tag.Dimension.Length.Should().Be(length);
        }

        [Test]
        public void SetRadix_AtomicValidRadix_ShouldSetMembersRadix()
        {
            var tag = new Tag("Test", Predefined.Dint, new Dimensions(3, 4));

            tag.Radix = Radix.Ascii;

            tag.Radix.Should().Be(Radix.Ascii);
            tag.Members.All(t => t.Radix == Radix.Ascii).Should().BeTrue();
        }

        [Test]
        public void New_GenericTag_ShouldNotBeNull()
        {
            var tag = new Tag<Bool>("Test");
        }
    }
}