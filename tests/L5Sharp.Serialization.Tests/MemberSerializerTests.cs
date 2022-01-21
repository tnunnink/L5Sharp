using System;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class MemberSerializerTests
    {
        private MemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            var context = new LogixContext(""); //todo need sample file I guess
            _serializer = new MemberSerializer(context);
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var member = Member.Create<Bool>("Test");

            var xml = _serializer.Serialize(member);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Basic_ShouldBeApproved()
        {
            var member = Member.Create<Bool>("Test");

            var xml = _serializer.Serialize(member);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_OverLoaded_ShouldBeApproved()
        {
            var member = Member.Create<Bool>("Test", Radix.Binary, ExternalAccess.ReadOnly,
                "This is a test member");

            var xml = _serializer.Serialize(member);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Complex_ShouldBeApproved()
        {
            var member = Member.Create<Timer>("Test");

            var xml = _serializer.Serialize(member);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_AtomicArray_ShouldBeApproved()
        {
            var member = Member.Create<Dint>("Test", new Dimensions(10));

            var xml = _serializer.Serialize(member);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        public void Deserialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Deserialize(null!)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_InvalidElementName_ShouldThrowArgumentException()
        {
            const string xml = @"<Invalid></Invalid>";
            var element = XElement.Parse(xml);

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void Deserialize_SimpleMember_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleMemberXml());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_AtomicMember_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetAtomicMemberXml());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Test");
            component.DataType.Should().BeOfType<Bool>();
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadWrite);
            component.Description.Should().Be("Simple Member");
        }
        
        private static string GetAtomicMemberXml()
        {
            return @"<Member Name=""Test"" DataType=""BOOL"" Dimension=""0"" Radix=""Decimal"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Simple Member]]>
                </Description>
                </Member>";
        }

        private static string GetArrayMemberXml()
        {
            return @" <Member Name=""IntArray"" DataType=""INT"" Dimension=""5"" Radix=""Octal"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[Test Int Array]]>
                </Description>
                </Member>";
        }

        private static string GetSimpleMemberXml()
        {
            return @"<Member Name=""SimpleMember"" DataType=""SimpleType"" Dimension=""0"" Radix=""NullType"" Hidden=""false"" ExternalAccess=""Read/Write"">
                <Description>
                <![CDATA[User defined simple member type]]>
                </Description>
                </Member>";
        }
    }
}