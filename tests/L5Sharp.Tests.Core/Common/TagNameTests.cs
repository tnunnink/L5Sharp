using System.Diagnostics;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class TagNameTests
    {
        private const string TestTagName = "MyTag_01.SomeMember[1].Some_Element_Property";
        private const string Base = "MyTag_01";
        private const string Path = "SomeMember[1].Some_Element_Property";

        private static readonly IEnumerable<string> Members = new List<string>
        {
            "MyTag_01",
            "SomeMember",
            "[1]",
            "Some_Element_Property"
        };


        [Test]
        public void New_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new TagName(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_Empty_ShouldNotBeNullAndIsEmptyTrue()
        {
            var tagName = new TagName(string.Empty);

            tagName.Should().NotBeNull();
            tagName.IsEmpty.Should().BeTrue();
        }

        [Test]
        public void New_SimpleTagName_ShouldNotBeNull()
        {
            var tagName = new TagName("Test");

            tagName.Should().NotBeNull();
        }

        [Test]
        public void New_BaseTagOnly_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName("MyTag");

            tagName.Path.Should().Be("MyTag");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().BeEmpty();
            tagName.Member.Should().BeEmpty();
            tagName.Element.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
            tagName.Members.Should().HaveCount(1);
        }

        [Test]
        public void New_TagNameTwoMembers_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag.MemberName");

            tagName.Path.Should().Be("MyTag.MemberName");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".MemberName");
            tagName.Member.Should().Be("MemberName");
            tagName.Element.Should().Be("MemberName");
            tagName.Depth.Should().Be(1);
            tagName.Members.Should().HaveCount(2);
        }

        [Test]
        public void New_TagNameThreeMembers_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag.MemberName.SubMember");

            tagName.Path.Should().Be("MyTag.MemberName.SubMember");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".MemberName.SubMember");
            tagName.Member.Should().Be("MemberName.SubMember");
            tagName.Element.Should().Be("SubMember");
            tagName.Depth.Should().Be(2);
            tagName.Members.Should().HaveCount(3);
        }

        [Test]
        public void New_TagNameWithArrayIndex_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag[13].MemberName");

            tagName.Path.Should().Be("MyTag[13].MemberName");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be("[13].MemberName");
            tagName.Member.Should().Be("[13].MemberName");
            tagName.Element.Should().Be("MemberName");
            tagName.Depth.Should().Be(2);
            tagName.Members.Should().HaveCount(3);
        }

        [Test]
        public void New_TagNameWithBitReference_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag.MemberName.1");

            tagName.Path.Should().Be("MyTag.MemberName.1");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".MemberName.1");
            tagName.Member.Should().Be("MemberName.1");
            tagName.Element.Should().Be("1");
            tagName.Depth.Should().Be(2);
            tagName.Members.Should().HaveCount(3);
        }

        [Test]
        public void New_ComplexTagName_ShouldBeExpected()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            tagName.Path.Should().Be("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");
            tagName.Base.Should().Be("Module:1:I");
            tagName.Operand.Should().Be(".TagName.Member[1].SubTag.Another[12,13,14].Value.12");
            tagName.Member.Should().Be("TagName.Member[1].SubTag.Another[12,13,14].Value.12");
            tagName.Element.Should().Be("12");
            tagName.Depth.Should().Be(8);
            tagName.Members.Should().HaveCount(9);
            tagName.IsEmpty.Should().BeFalse();
            tagName.IsQualified.Should().BeTrue();
        }

        [Test]
        public void Scope_NoProgramPrefix_ShouldBeControllerAndEmptyContainer()
        {
            var tagName = new TagName("MyTag");

            var scope = tagName.Scope;

            scope.Level.Should().Be(ScopeLevel.Controller);
            scope.Container.Should().BeEmpty();
        }

        [Test]
        public void Scope_WithProgramPrefix_ShouldBeControllerAndEmptyContainer()
        {
            var tagName = new TagName("Program:SomeProgram.MyTag");

            var scope = tagName.Scope;

            scope.Level.Should().Be(ScopeLevel.Program);
            scope.Container.Should().Be("SomeProgram");
        }

        [Test]
        public void Scope_WithProgramPrefix_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName("Program:SomeProgram.MyTag");
            
            tagName.Path.Should().Be("Program:SomeProgram.MyTag");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().BeEmpty();
            tagName.Member.Should().BeEmpty();
            tagName.Element.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
            tagName.Members.Should().HaveCount(1);
        }

        [Test]
        public void IsQualified_QualifiedTagNameWithAllPossibleMemberTypes_ShouldBeTrue()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            var result = tagName.IsQualified;

            result.Should().BeTrue();
        }

        [Test]
        public void IsQualified_ArraySegmentOnly_ShouldBeFalse()
        {
            var tagName = new TagName("[1]");

            var result = tagName.IsQualified;

            result.Should().BeFalse();
        }

        [Test]
        [Description("GitHub Issue #52: A tag with a bit index reference tag should be qualified")]
        public void IsQualified_BitIndexAddressing_ShouldBeFalse()
        {
            var tagName = new TagName("DintTest.[Offset]");

            var result = tagName.IsQualified;

            result.Should().BeTrue();
        }

        [Test]
        public void Root_WhenCalledManyTimes_ShouldBeEfficient()
        {
            var stopwatch = new Stopwatch();
            var tags = new List<TagName>();

            for (var i = 0; i < 1000000; i++)
            {
                var tag = TagName.Concat("MyTag_01.SomeMember[1].Some_Element_Property", i.ToString());
                tags.Add(tag);
            }

            stopwatch.Start();

            foreach (var tag in tags)
            {
                var member = tag.Base;
                member.Should().NotBeNull();
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test]
        public void Operand_MemberSeparator_ShouldBeExpected()
        {
            var tagName = new TagName("MyTag.SomeMember.1");

            var operand = tagName.Operand;

            operand.Should().Be(".SomeMember.1");
        }

        [Test]
        public void Operand_ArrayBracket_ShouldBeExpected()
        {
            var tagName = new TagName("MyTag[1].SomeMember.1");

            var operand = tagName.Operand;

            operand.Should().Be("[1].SomeMember.1");
        }

        [Test]
        [TestCase("MyTag.SomeMember.1", "SomeMember.1")]
        [TestCase("MyTag[1].SomeMember.1", "[1].SomeMember.1")]
        public void Path_MemberSeparator_ShouldBeExpected(string text, string expected)
        {
            var tagName = new TagName(text);

            var path = tagName.Member;

            path.Should().Be(expected);
        }

        [Test]
        public void Path_ArrayBracket_ShouldBeExpected()
        {
            var tagName = new TagName("MyTag[1].SomeMember.1");

            var path = tagName.Member;

            path.Should().Be("[1].SomeMember.1");
        }

        [Test]
        public void Member_ValidTag_ShouldBeExpected()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            var member = tagName.Element;

            member.Should().Be("12");
        }

        [Test]
        public void Member_Empty_ShouldBeExpected()
        {
            var tagName = new TagName("");

            var member = tagName.Element;

            member.Should().BeEmpty();
        }

        [Test]
        public void Member_ArrayPart_ShouldBeExpected()
        {
            var tagName = new TagName("MyTag.Member[1]");

            var member = tagName.Element;

            member.Should().Be("[1]");
        }

        [Test]
        public void Member_WhenRetrievedLargeNumberOfTimes_ShouldBeFast()
        {
            var stopwatch = new Stopwatch();
            var tags = new List<TagName>();

            for (var i = 0; i < 1000000; i++)
            {
                var tag = TagName.Concat("MyTagName.Member[1].Some_Other_Longer_Name", i.ToString());
                tags.Add(tag);
            }

            stopwatch.Start();

            foreach (var tag in tags)
            {
                var member = tag.Element;
                member.Should().NotBeNull();
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test]
        public void Members_WhenIterated_ShouldNotBeEmptyAndOfTypeString()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            foreach (var member in tagName.Members)
            {
                member.Should().BeOfType<string>();
                member.Should().NotBeEmpty();
            }
        }

        [Test]
        public void Members_WhenCalled_ShouldContainExpectedValues()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            var members = tagName.Members.ToList();

            members[0].Should().Be("Module:1:I");
            members[1].Should().Be("TagName");
            members[2].Should().Be("Member");
            members[3].Should().Be("[1]");
            members[4].Should().Be("SubTag");
            members[5].Should().Be("Another");
            members[6].Should().Be("[12,13,14]");
            members[7].Should().Be("Value");
            members[8].Should().Be("12");
        }

        [Test]
        public void Members_WhenCalledManyTimes_ShouldBeEfficient()
        {
            var stopwatch = new Stopwatch();
            var tags = new List<TagName>();

            for (var i = 0; i < 1000000; i++)
            {
                var tag = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");
                tags.Add(tag);
            }

            stopwatch.Start();

            foreach (var tag in tags)
            {
                var member = tag.Members.ToList();
                member.Should().HaveCount(9);
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test]
        public void Empty_WhenCalled_ShouldBeEmptyValue()
        {
            var tagName = TagName.Empty;

            tagName.Should().Be(string.Empty);
        }

        [Test]
        public void Empty_WhenCalled_ShouldHaveExpectedValues()
        {
            var tagName = TagName.Empty;

            tagName.Base.Should().BeEmpty();
            tagName.Operand.Should().BeEmpty();
            tagName.Member.Should().BeEmpty();
            tagName.Element.Should().BeEmpty();
            tagName.Members.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
            tagName.IsEmpty.Should().BeTrue();
            tagName.IsQualified.Should().BeFalse();
        }

        [Test]
        public void Combine_TwoMemberNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine(Base, Path);

            tagName.Should().Be(TestTagName);
        }

        [Test]
        public void Combine_ManyNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine("MyTag", "SomeArray", "[1]", "ArrayElement", "SomeMember", ".1");

            tagName.Should().Be("MyTag.SomeArray[1].ArrayElement.SomeMember.1");
        }

        [Test]
        public void Combine_CollectionOfValidMembers_ShouldBeExpectedValue()
        {
            var tagName = TagName.Combine(Members);

            tagName.Should().Be(TestTagName);
        }

        [Test]
        public void Contains_NullName_ShouldThrowArgumentNullException()
        {
            var tagName = new TagName(TestTagName);

            FluentActions.Invoking(() => tagName.Contains(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Contains_NameNotInTagName_ShouldBeFalse()
        {
            var tagName = new TagName(TestTagName);

            var result = tagName.Contains("SomeName");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_NameContainsTagName_ShouldBeTrue()
        {
            var tagName = new TagName(TestTagName);

            var result = tagName.Contains(Base);

            result.Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_Byte_ShouldBeExpected()
        {
            TagName tagName = TestTagName;

            tagName.Equals(TestTagName).Should().BeTrue();
        }

        [Test]
        public void ImplicitOperator_ScanRate_ShouldBeExpected()
        {
            string tagName = new TagName(TestTagName);

            tagName.Equals(TestTagName).Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void TypedEquals_AreNotEqual_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(Path);

            var result = first.Equals(second);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_Null_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);

            var result = first.Equals(null!);

            result.Should().BeFalse();
        }

        [Test]
        public void TypedEquals_AreSame_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);

            // ReSharper disable once EqualExpressionComparison
            var result = first.Equals(first);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void ObjectEquals_AreSame_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);

            var result = first.Equals((object)first);

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorEquals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first == second;

            result.Should().BeTrue();
        }

        [Test]
        public void OperatorNotEquals_AreEqual_ShouldBeFalse()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first != second;

            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_WhenCalled_ShouldNotBeZero()
        {
            var first = new TagName(TestTagName);

            var hash = first.GetHashCode();

            hash.Should().NotBe(0);
        }

        [Test]
        public void ToString_WhenCalled_ShouldReturnExpectedValue()
        {
            var first = new TagName(TestTagName);

            var value = first.ToString();

            value.Should().Be(TestTagName);
        }

        [Test]
        public void CompareTo_Equal_ShouldBeZero()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.CompareTo(second);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_Different_ShouldNotBeZero()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(Path);

            var result = first.CompareTo(second);

            result.Should().NotBe(0);
        }

        [Test]
        public void CompareTo_Same_ShouldBeZero()
        {
            var first = new TagName(TestTagName);

            var result = first.CompareTo(first);

            result.Should().Be(0);
        }

        [Test]
        public void CompareTo_Null_ShouldBeOne()
        {
            var first = new TagName(TestTagName);

            var result = first.CompareTo(null!);

            result.Should().Be(1);
        }
    }
}