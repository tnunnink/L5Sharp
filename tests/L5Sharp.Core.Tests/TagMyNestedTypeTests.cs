using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagMyNestedTypeTests
    {
        [Test]
        public void Create_DefaultConstructor_shouldNotBeNull()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Should().NotBeNull();
        }

        [Test]
        public void Create_WithRadix_ShouldNotSetRadixSinceItsAComplexType()
        {
            var tag = Tag.Create<MyNestedType>("Test", Radix.Binary);

            tag.Should().NotBeNull();
            tag.Radix.Should().Be(Radix.Null);
        }

        [Test]
        public void Create_WithExternalAccessReadOnly_ShouldHaveExpectedAccess()
        {
            var tag = Tag.Create<MyNestedType>("Test", externalAccess: ExternalAccess.ReadOnly);

            tag.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
        }
        
        [Test]
        public void Create_WithExternalAccessReadOnly_AllMembersShouldHaveReadOnlyAccess()
        {
            var tag = Tag.Create<MyNestedType>("Test", externalAccess: ExternalAccess.ReadOnly);

            var members = tag.GetMembers();

            members.Select(m => m.ExternalAccess).Should().AllBeEquivalentTo(ExternalAccess.ReadOnly);
        }

        [Test]
        public void Create_Default_ShouldHaveUserDefinedDescription()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Description.Should().Be("This is the user defined description");
        }

        [Test]
        public void Create_Default_MembersShouldHaveExpectedDescription()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var indy = tag.GetMember(m => m.Indy);
            indy.Description.Should().Be("This is the user defined description Test Bool Member");
            
            var str = tag.GetMember(m => m.Str);
            str.Description.Should().Be("This is the user defined description Test String Member");
            
            var tmr = tag.GetMember(m => m.Tmr);
            tmr.Description.Should().Be("This is the user defined description Test Timer Member");
        }
    }
}