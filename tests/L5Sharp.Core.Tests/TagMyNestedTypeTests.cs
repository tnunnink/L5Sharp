using System.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Factories;
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

            var members = tag.Members();

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

            var indy = tag.Member(m => m.Indy);
            indy.Description.Should().Be("This is the user defined description Test Bool Member");
            
            var str = tag.Member(m => m.Str);
            str.Description.Should().Be("This is the user defined description Test String Member");
            
            var tmr = tag.Member(m => m.Tmr);
            tmr.Description.Should().Be("This is the user defined description Test Timer Member");
        }
        
        [Test]
        public void Comment_BaseTag_ShouldUpdateMemberDescriptions()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Comment("Override Description");
            
            tag.Member(m => m.Indy).Description.Should().Be("Override Description Test Bool Member");
            tag.Member(m => m.Str).Description.Should().Be("Override Description Test String Member");
            tag.Member(m => m.Tmr).Description.Should().Be("Override Description Test Timer Member");
        }
        
        [Test]
        public void Comment_ResetCommentOnBaseTag_ShouldRevertDescriptions()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Comment("Override Description");
            
            tag.Member(m => m.Indy).Description.Should().Be("Override Description Test Bool Member");
            tag.Member(m => m.Str).Description.Should().Be("Override Description Test String Member");
            tag.Member(m => m.Tmr).Description.Should().Be("Override Description Test Timer Member");
            
            tag.Comment(string.Empty);
            
            tag.Member(m => m.Indy).Description.Should().Be("This is the user defined description Test Bool Member");
            tag.Member(m => m.Str).Description.Should().Be("This is the user defined description Test String Member");
            tag.Member(m => m.Tmr).Description.Should().Be("This is the user defined description Test Timer Member");
        }
        
        [Test]
        public void Comment_MemberThenTag_MemberShouldRetainOverride()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            tag.Member(m => m.Indy).Comment("This is a member comment");
            tag.Member(m => m.Indy).Description.Should().Be("This is a member comment");
            
            tag.Comment("Override Description");

            tag.Member(m => m.Indy).Description.Should().Be("This is a member comment");
        }
        
        [Test]
        public void Comment_BaseTag_NestedMembersShouldGetComment()
        {
            var tag = Tag.Create<MyNestedType>("Test");
            
            tag.Comment("Test Description");

            var nested = tag.Member(m => m.Simple).Members();

            nested.Select(m => m.Description).Should().AllBeEquivalentTo("Test Description");
        }

        [Test]
        public void NameIndex_ValidMemberName_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.Member("Test.Simple.M3");

            member.Should().NotBeNull();
            member.Name.Should().Be("M3");
            member.TagName.Should().Be("Test.Simple.M3");
            member.DataType.Should().BeOfType<Int>();
        }
        
        [Test]
        public void GetMember_ValidMemberName_ShouldBeExpected()
        {
            var tag = Tag.Create<MyNestedType>("Test");

            var member = tag.Member(m => m.Simple).Member(m => m.M3);

            member.Should().NotBeNull();
            member.Name.Should().Be("M3");
            member.TagName.Should().Be("Test.Simple.M3");
            member.DataType.Should().BeOfType<Int>();
        }
    }
}