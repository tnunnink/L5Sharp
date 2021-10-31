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
            var type = new DataType("Test", new DataTypeMember("Member01", Predefined.Dint));
            var tag = new Tag("TestTag", type);

            tag.Should().NotBeNull();
        }
        
        [Test]
        public void AddMember_ValidMember_ShouldUpdateTag()
        {
            var type = new DataType("Test", new DataTypeMember("Member01", Predefined.Dint));
            var tag = new Tag("TestTag", type);

            type.Members.Add(new DataTypeMember("NewMember", Predefined.Bool));

            tag.Members.Should().HaveCount(2);
        }
        
        [Test]
        public void UpdatedNestedType_ValidMember_ShouldUpdateNestedTagMember()
        {
            var type1 = new DataType("BaseType", new DataTypeMember("BaseMember", Predefined.Dint));
            var type2 = new DataType("SubType", new DataTypeMember("Member01", Predefined.Bool));
            type1.Members.Add(new DataTypeMember("SubMember", type2));
            var tag = new Tag("TestTag", type1);

            type2.Members.Add(new DataTypeMember("NewMember", Predefined.Int));

            tag.Members.Should().HaveCount(2);
            var names = tag.GetMembersNames();
            names.Should().HaveCount(4);
        }
    }
}