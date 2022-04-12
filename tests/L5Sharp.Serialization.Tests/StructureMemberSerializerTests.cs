using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Types;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class StructureMemberSerializerTests
    {
        private StructureMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new StructureMemberSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var component = new Member<IDataType>("Test", new StructureType("Test"));

            var xml = _serializer.Serialize(component);

            xml.Should().NotBeNull();
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_DataValueValueMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<IMember<IDataType>>
            {
                Member.Create<BOOL>("BoolMember"),
                Member.Create<SINT>("SintMember"),
                Member.Create<INT>("IntMember"),
                Member.Create<DINT>("DintMember"),
                Member.Create<LINT>("LintMember"),
                Member.Create<REAL>("RealMember")
            });

            var component = new Member<IDataType>("Test", type);

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SerializeArrayMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<IMember<IDataType>>
            {
                Member.Create<DINT>("SimpleMember", new Dimensions(10)),
                Member.Create<TIMER>("ComplexMember", 5),
                Member.Create<STRING>("StringMember", 2)
            });
            
            var component = new Member<IDataType>("Test", type);

            var xml = _serializer.Serialize(component);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_StructureMembers_ShouldBeApproved()
        {
            var type = new StructureType("Test", new List<IMember<IDataType>>
            {
                Member.Create<STRING>("StringMember"),
                Member.Create<TIMER>("SintMember"),
                Member.Create<COUNTER>("IntMember"),
            });
            
            var component = new Member<IDataType>("Test", type);

            var xml = _serializer.Serialize(component);

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

            FluentActions.Invoking(() => _serializer.Deserialize(element)).Should().Throw<ArgumentException>()
                .WithMessage($"Element 'Invalid' not valid for the serializer {_serializer.GetType()}.");
        }

        [Test]
        public void Deserialize_SimpleStructureMember_ShouldNotBeNull()
        {
            var xml = XElement.Parse(GetSimpleStructureMember());

            var component = _serializer.Deserialize(xml);

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_SimpleStructureMember_ShouldHaveExpectedProperties()
        {
            var xml = XElement.Parse(GetSimpleStructureMember());

            var component = _serializer.Deserialize(xml);

            component.Name.Should().Be("SimpleMember");
            component.DataType.Name.Should().Be("SimpleType");
            component.DataType.Should().BeOfType<StructureType>();
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Radix.Should().Be(Radix.Null);
            component.Description.Should().BeEmpty();
            component.MemberType.Should().Be(MemberType.StructureMember);
            component.DataType.As<IComplexType>().Members.Should().HaveCount(5);
        }

        private static string GetSimpleStructureMember()
        {
            return  @"<StructureMember Name=""SimpleMember"" DataType=""SimpleType"">
                <DataValueMember Name=""BoolMember"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""SintMember"" DataType=""SINT"" Radix=""Hex"" Value=""16#00""/>
                <DataValueMember Name=""IntMember"" DataType=""INT"" Radix=""Octal"" Value=""8#000_000""/>
                <DataValueMember Name=""DintMember"" DataType=""DINT"" Radix=""ASCII"" Value=""'$00$00$00$00'""/>
                <DataValueMember Name=""LintMember"" DataType=""LINT"" Radix=""Decimal"" Value=""0""/>
                </StructureMember>";
        }
    }
}