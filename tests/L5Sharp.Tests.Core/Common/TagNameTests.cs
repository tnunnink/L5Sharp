using System.Diagnostics;
using FluentAssertions;

namespace L5Sharp.Tests.Core.Common
{
    [TestFixture]
    public class TagNameTests
    {
        private const string TestTagName = "MyTag.SomeMember.Array[1, 2].SubTag.12";

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
        public void Empty_WhenCalled_ShouldHaveExpectedValues()
        {
            var tagName = TagName.Empty;

            tagName.Base.Should().BeEmpty();
            tagName.Operand.Should().BeEmpty();
            tagName.Member.Should().BeEmpty();
            tagName.Element.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
            tagName.IsEmpty.Should().BeTrue();
            tagName.IsQualified.Should().BeFalse();
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
        }

        [Test]
        public void New_SingleMemberTagName_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag.MemberName");

            tagName.Path.Should().Be("MyTag.MemberName");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".MemberName");
            tagName.Member.Should().Be("MemberName");
            tagName.Element.Should().Be("MemberName");
            tagName.Depth.Should().Be(1);
        }

        [Test]
        public void New_MultipleMemberTagName_ShouldHaveExpectedValues()
        {
            var tagName = new TagName("MyTag.MemberName.SubMember");

            tagName.Path.Should().Be("MyTag.MemberName.SubMember");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".MemberName.SubMember");
            tagName.Member.Should().Be("MemberName.SubMember");
            tagName.Element.Should().Be("SubMember");
            tagName.Depth.Should().Be(2);
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
        }

        [Test]
        public void Scope_NoProgramPrefix_ShouldBeControllerAndEmptyContainer()
        {
            var tagName = new TagName("MyTag");

            tagName.Scope.Level.Should().Be(ScopeLevel.Controller);
            tagName.Scope.Container.Should().BeEmpty();
        }

        [Test]
        public void Scope_WithProgramPrefix_ShouldBeControllerAndEmptyContainer()
        {
            var tagName = new TagName("Program:SomeProgram.MyTag");

            tagName.Scope.Level.Should().Be(ScopeLevel.Program);
            tagName.Scope.Container.Should().Be("SomeProgram");
        }

        [Test]
        public void Scope_WithProgramPrefix_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName("Program:SomeProgram.MyTag");

            tagName.Path.Should().Be("Program:SomeProgram.MyTag");
            tagName.LocalPath.Should().Be("MyTag");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().BeEmpty();
            tagName.Member.Should().BeEmpty();
            tagName.Element.Should().BeEmpty();
            tagName.Depth.Should().Be(0);
        }

        [Test]
        public void Scope_WithProgramPrefixAndMemberTag_ShouldHaveExpectedProperties()
        {
            var tagName = new TagName("Program:SomeProgram.MyTag.Member[0].Value.12");

            tagName.Path.Should().Be("Program:SomeProgram.MyTag.Member[0].Value.12");
            tagName.LocalPath.Should().Be("MyTag.Member[0].Value.12");
            tagName.Base.Should().Be("MyTag");
            tagName.Operand.Should().Be(".Member[0].Value.12");
            tagName.Member.Should().Be("Member[0].Value.12");
            tagName.Element.Should().Be("12");
            tagName.Depth.Should().Be(4);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("MyTag.SomeMember.1", "MyTag")]
        [TestCase("MyTag[1].SomeMember.1", "MyTag")]
        [TestCase("Module:1:I.Data[1].SubTag.Value.12", "Module:1:I")]
        [TestCase(".Member[32].Value", "")]
        [TestCase("Member[32].Value", "Member")]
        [TestCase("[32].Value", "[32]")]
        public void Base_WhenCalled_ShouldBeExpected(string value, string expected)
        {
            var tagName = new TagName(value);

            var path = tagName.Base;

            path.Should().Be(expected);
        }

        [Test]
        public void Base_WhenCalledManyTimes_ShouldBeEfficient()
        {
            var tags = Enumerable.Range(0, 1000000)
                .Select(i => $"MyTagName_{i}.SomeMember.21")
                .Select(s => s.ToTagName())
                .ToList();

            var stopwatch = Stopwatch.StartNew();
            var elements = tags.Select(t => t.Base).ToList();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
            elements.Should().HaveCount(1000000);
            elements.Should().AllSatisfy(s => s.Should().StartWith("MyTagName"));
            stopwatch.Elapsed.Seconds.Should().BeLessThan(1);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("MyTag.SomeMember.1", "SomeMember.1")]
        [TestCase("MyTag[1].SomeMember.1", "[1].SomeMember.1")]
        [TestCase("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12",
            "TagName.Member[1].SubTag.Another[12,13,14].Value.12")]
        public void Member_WhenCalled_ShouldBeExpected(string value, string expected)
        {
            var tagName = new TagName(value);

            var path = tagName.Member;

            path.Should().Be(expected);
        }

        [Test]
        public void Member_CalledOnLargeList_ShouldExecuteInExpectedTime()
        {
            var tags = Enumerable.Range(0, 1000000)
                .Select(i => $"MyTagName_{i}.SomeMember.Target")
                .Select(s => s.ToTagName())
                .ToList();

            var stopwatch = Stopwatch.StartNew();
            var elements = tags.Select(t => t.Member).ToList();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
            elements.Should().HaveCount(1000000);
            elements.Should().AllSatisfy(s => s.Should().Be("SomeMember.Target"));
            stopwatch.Elapsed.Seconds.Should().BeLessThan(1);
        }

        [Test]
        [TestCase("", "")]
        [TestCase("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12", "12")]
        [TestCase("MyTag.Member[1]", "[1]")]
        public void Element_WhenCalled_ShouldBeExpected(string value, string expected)
        {
            var tagName = new TagName(value);

            var element = tagName.Element;

            element.Should().Be(expected);
        }

        [Test]
        public void Element_CalledOnLargeList_ShouldExecuteInExpectedTime()
        {
            var tags = Enumerable.Range(0, 1000000)
                .Select(i => $"MyTagName_{i}.SomeMember.Target")
                .Select(s => s.ToTagName())
                .ToList();

            var stopwatch = Stopwatch.StartNew();
            var elements = tags.Select(t => t.Element).ToList();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
            elements.Should().HaveCount(1000000);
            elements.Should().AllSatisfy(s => s.Should().Be("Target"));
            stopwatch.Elapsed.Seconds.Should().BeLessThan(1);
        }
        
        [Test]
        [TestCase("", 0)]
        [TestCase("MyTag.SomeMember.1", 2)]
        [TestCase("MyTag[1].SomeMember.1", 3)]
        [TestCase("Module:1:I.Data[1].SubTag.Value.12", 5)]
        [TestCase(".Member[32].Value", 2)]
        [TestCase("Member[32].Value", 2)]
        [TestCase("[32].Value", 1)]
        public void Depth_ValidTagName_ShouldBeExpected(string value, int expected)
        {
            var tagName = new TagName(value);

            var path = tagName.Depth;

            path.Should().Be(expected);
        }

        [Test]
        public void Members_ComplexTag_ShouldContainExpectedValues()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");

            var members = tagName.Members().ToList();

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
        public void Members_ToSpecifiedDepth_ShouldBeExpected()
        {
            var tagName = new TagName("Module:1:I.TagName.Member[1].SubTag.Another[12,13,14].Value.12");
            
            var members = tagName.Members(3).ToList();

            members.Count.Should().Be(3);
            members[0].Should().Be("Module:1:I");
            members[1].Should().Be("TagName");
            members[2].Should().Be("Member");
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("MyTag.SomeMember.1", 3)]
        [TestCase("MyTag[1].SomeMember.1", 4)]
        [TestCase("Module:1:I.Data[1].SubTag.Value.12", 6)]
        [TestCase(".Member[32].Value", 3)]
        [TestCase("Member[32].Value", 3)]
        [TestCase("[32].Value", 2)]
        public void Members_ProvidedTagName_ShouldHaveExpectedCount(string value, int expected)
        {
            var tagName = new TagName(value);

            var members = tagName.Members().ToList();

            members.Should().HaveCount(expected);
        }

        [Test]
        public void Members_WhenCalledManyTimes_ShouldBeEfficient()
        {
            var tags = Enumerable.Range(0, 1000000)
                .Select(i => $"Module:1:I.TagName.Member[{i}].SubTag.Another[12,13,14].Value.12")
                .Select(s => s.ToTagName())
                .ToList();

            var stopwatch = Stopwatch.StartNew();
            var members = tags.SelectMany(t => t.Members()).ToList();
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);
            members.Should().HaveCount(1000000 * 9);
            stopwatch.Elapsed.Seconds.Should().BeLessThan(3);
        }

        [Test]
        [TestCase("MyTagName", true)]
        [TestCase("MyTagName.Member", true)]
        [TestCase("MyTagName.Member[1]", true)]
        [TestCase("MyTagName.Member[1].Value", true)]
        [TestCase("MyTagName.Member[1].Value.21", false)]
        [TestCase("MyTagName.Member[2].Value", false)]
        [TestCase("MyTagName.Something[1].Value", false)]
        [TestCase("MyTag.Member[1].Value", false)]
        public void IsMemberOf_ParentTwoLevelsUp_ShouldBeTrue(string parent, bool expected)
        {
            var tagName = new TagName("MyTagName.Member[1].Value.21");

            var result = tagName.IsMemberOf(parent);

            result.Should().Be(expected);
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
        public void Contains_NameNotInTagName_ShouldBeFalse()
        {
            var tagName = new TagName("MyTagName.SomeMember.12");

            var result = tagName.Contains("FakeName");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_NameContainsTagName_ShouldBeTrue()
        {
            var tagName = new TagName("MyTagName.SomeMember.12");

            var result = tagName.Contains("SomeMember");

            result.Should().BeTrue();
        }

        [Test]
        public void Contains_NullName_ShouldThrowArgumentNullException()
        {
            var tagName = new TagName(TestTagName);

            FluentActions.Invoking(() => tagName.Contains(null!)).Should().Throw<ArgumentNullException>();
        }


        [Test]
        public void Combine_TwoMemberNames_ShouldBeExpected()
        {
            var tagName = TagName.Combine("MyTag", "SomeMember");

            tagName.Should().Be("MyTag.SomeMember");
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
            var tagName = TagName.Combine(["MyTag", "SomeArray", "[1]", "ArrayElement", "SomeMember", ".1"]);

            tagName.Should().Be("MyTag.SomeArray[1].ArrayElement.SomeMember.1");
        }

        [Test]
        public void ImplicitOperator_ValidName_ShouldBeExpected()
        {
            TagName tagName = "MyTagName";

            tagName.Should().Be("MyTagName");
        }

        [Test]
        public void ImplicitOperator_ScanRate_ShouldBeExpected()
        {
            string tagName = new TagName("MyTagName");

            tagName.Should().Be("MyTagName");
        }

        [Test]
        public void Equals_AreEqual_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);
            var second = new TagName(TestTagName);

            var result = first.Equals(second);

            result.Should().BeTrue();
        }

        [Test]
        public void Equals_AreSame_ShouldBeTrue()
        {
            var first = new TagName(TestTagName);

            // ReSharper disable once EqualExpressionComparison we need to test overriden logic
            var result = first.Equals(first);

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
            var second = new TagName("OtherTag.WithMember");

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