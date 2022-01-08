using System.Linq;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Types;
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
        
        [Test]
        public void Comment_BaseTag_ShouldUpdateMemberDescriptions()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Comment("Override Description");
            
            tag.GetMember(m => m.Indy).Description.Should().Be("Override Description Test Bool Member");
            tag.GetMember(m => m.Str).Description.Should().Be("Override Description Test String Member");
            tag.GetMember(m => m.Tmr).Description.Should().Be("Override Description Test Timer Member");
        }
        
        [Test]
        public void Comment_ResetCommentOnBaseTag_ShouldRevertDescriptions()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Comment("Override Description");
            
            tag.GetMember(m => m.Indy).Description.Should().Be("Override Description Test Bool Member");
            tag.GetMember(m => m.Str).Description.Should().Be("Override Description Test String Member");
            tag.GetMember(m => m.Tmr).Description.Should().Be("Override Description Test Timer Member");
            
            tag.Comment(string.Empty);
            
            tag.GetMember(m => m.Indy).Description.Should().Be("This is the user defined description Test Bool Member");
            tag.GetMember(m => m.Str).Description.Should().Be("This is the user defined description Test String Member");
            tag.GetMember(m => m.Tmr).Description.Should().Be("This is the user defined description Test Timer Member");
        }
        
        [Test]
        public void Comment_MemberThenTag_MemberShouldRetainOverride()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.GetMember(m => m.Indy).Comment("This is a member comment");
            tag.GetMember(m => m.Indy).Description.Should().Be("This is a member comment");
            
            tag.Comment("Override Description");

            tag.GetMember(m => m.Indy).Description.Should().Be("This is a member comment");
        }
        
        [Test]
        public void Comment_BaseTag_NestedMembersShouldGetComment()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            
            tag.Comment("Test Description");

            var nested = tag.GetMember(m => m.Simple).GetMembers();

            nested.Select(m => m.Description).Should().AllBeEquivalentTo("Test Description");
        }

        [Test]
        public void NameIndex_ValidMemberName_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag["Test.Simple.M3"];

            member.Should().NotBeNull();
            member.Name.Should().Be("M3");
            member.TagName.Should().Be("Test.Simple.M3");
            member.DataType.Should().BeOfType<Int>();
        }
        
        [Test]
        public void GetMember_ValidMemberName_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.GetMember(m => m.Simple).GetMember(m => m.M3);

            member.Should().NotBeNull();
            member.Name.Should().Be("M3");
            member.TagName.Should().Be("Test.Simple.M3");
            member.DataType.Should().BeOfType<Int>();
        }
    }
}