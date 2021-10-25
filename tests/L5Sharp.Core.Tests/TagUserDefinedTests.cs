using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class TagUserDefinedTests
    {
        [Test]
        public void New_ValidTypeAndName_TagShouldNotBeNull()
        {
            var type = new DataType("Test", new Member("Member01", Predefined.Dint));
            var tag = new Tag("TestTag", type);

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void AddMember_ValidMember_ShouldUpdateTag()
        {
            var type = new DataType("Test", new Member("Member01", Predefined.Dint));
            var tag = new Tag("TestTag", type);

            type.AddMember("NewMember", Predefined.Bool);

            tag.Members.Should().HaveCount(2);
        }
        
        [Test]
        public void UpdatedNestedType_ValidMember_ShouldUpdateNestedTagMember()
        {
            var type1 = new DataType("BaseType", new Member("BaseMember", Predefined.Dint));
            var type2 = new DataType("SubType", new Member("Member01", Predefined.Bool));
            type1.AddMember("SubMember", type2);
            var tag = new Tag("TestTag", type1);

            type2.AddMember("NewMember", Predefined.Int);

            tag.Members.Should().HaveCount(2);
            var names = tag.GetMembersNames();
            names.Should().HaveCount(4);
        }
    }
}