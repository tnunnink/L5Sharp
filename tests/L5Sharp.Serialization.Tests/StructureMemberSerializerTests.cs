using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Atomics;
using L5Sharp.Core;
using L5Sharp.Creators;
using L5Sharp.Enums;
using L5Sharp.Predefined;
using L5Sharp.Serialization.Data;
using NUnit.Framework;
using String = L5Sharp.Predefined.String;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class StructureMemberSerializerTests
    {
        private StructureMemberSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new StructureMemberSerializer(new StructureSerializer());
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
                Member.Create<Bool>("BoolMember"),
                Member.Create<Sint>("SintMember"),
                Member.Create<Int>("IntMember"),
                Member.Create<Dint>("DintMember"),
                Member.Create<Lint>("LintMember"),
                Member.Create<Real>("RealMember")
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
                Member.Create<Dint>("SimpleMember", new Dimensions(10)),
                Member.Create<Timer>("ComplexMember", 5),
                Member.Create<String>("StringMember", 2)
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
                Member.Create<String>("StringMember"),
                Member.Create<Timer>("SintMember"),
                Member.Create<Counter>("IntMember"),
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
            component.IsArrayMember.Should().BeFalse();
            component.IsValueMember.Should().BeFalse();
            component.IsStructureMember.Should().BeTrue();
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