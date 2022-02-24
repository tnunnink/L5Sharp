using System;
using System.Collections.Generic;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Factories;
using L5Sharp.Serialization.Components;
using L5Sharp.Types;
using L5Sharp.Types.Atomics;
using L5Sharp.Types.Predefined;
using NUnit.Framework;
using String = L5Sharp.Types.Predefined.String;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class TagSerializerTests
    {
        private TagSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _serializer = new TagSerializer();
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void SerializeSimpleBool_ShouldNotBeNull()
        {
            var tag = Tag.Create<Bool>("Test");

            var xml = _serializer.Serialize(tag);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleBool_ShouldBeApproved()
        {
            var tag = Tag.Create<Bool>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleSint_ShouldBeApproved()
        {
            var tag = Tag.Create<Sint>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleInt_ShouldBeApproved()
        {
            var tag = Tag.Create<Int>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleDint_ShouldBeApproved()
        {
            var tag = Tag.Create<Dint>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleLint_ShouldBeApproved()
        {
            var tag = Tag.Create<Lint>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleReal_ShouldBeApproved()
        {
            var tag = Tag.Create<Real>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleBoolArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Bool>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleSintArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Sint>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleIntArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Int>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleDintArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Dint>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleLintArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Lint>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_SimpleRealArray_ShouldBeApproved()
        {
            var tag = Tag.Create<Real>("Test", new Dimensions(5));

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_String_ShouldBeApproved()
        {
            var tag = Tag.Create<String>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_Timer_ShouldBeApproved()
        {
            var tag = Tag.Create<Timer>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }
        
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_AlarmDigital_ShouldBeApproved()
        {
            var tag = Tag.Create<AlarmDigital>("Test");

            var xml = _serializer.Serialize(tag);

            Approvals.VerifyXml(xml.ToString());
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_UserDefined_ShouldBeApproved()
        {
            var userType = new UserDefined("Test", "This is a test type", new List<IMember<IDataType>>
            {
                Member.Create<Bool>("BoolMember"),
                Member.Create<Dint>("DintMember"),
                Member.Create<Real>("RealMember"),
                Member.Create<Timer>("TimerMember"),
                Member.Create<String>("StringMember")
            });

            var tag = Tag.Create("Test", userType);

            var xml = _serializer.Serialize(tag);

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
        public void Deserialize_SimpleTagData_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetSimpleTagData());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }
        
        [Test]
        public void Deserialize_SimpleTagData_ShouldBeExpected()
        {
            var element = XElement.Parse(GetSimpleTagData());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("SimpleDint");
            component.DataType.Should().BeOfType<Dint>();
            component.Description.Should().BeEmpty();
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Decimal);
            component.ExternalAccess.Should().Be(ExternalAccess.None);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Null);
            component.Value.Should().Be(123456);
        }
        
        [Test]
        public void Deserialize_ComplexPredefined_ShouldBeExpected()
        {
            var element = XElement.Parse(GetPredefinedTagData());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("TestTimer");
            component.DataType.Should().BeOfType<Timer>();
            component.Description.Should().Be("Test Timer");
            component.TagType.Should().Be(TagType.Base);
            component.Constant.Should().BeFalse();
            component.Radix.Should().Be(Radix.Null);
            component.ExternalAccess.Should().Be(ExternalAccess.ReadOnly);
            component.Dimensions.Should().Be(Dimensions.Empty);
            component.Usage.Should().Be(TagUsage.Null);
            component.Value.Should().BeNull();
            component.Members().Should().HaveCount(5);
            component.Member("PRE").Value.Should().Be(1000);
            component.Member("ACC").Value.Should().Be(0);
            component.Member("EN").Value.Should().Be(false);
            component.Member("TT").Value.Should().Be(false);
            component.Member("DN").Value.Should().Be(false);
        }

        private static string GetSimpleTagData()
        {
            return
                @"<Tag Name=""SimpleDint"" TagType=""Base"" DataType=""DINT"" Radix=""Decimal"" Constant=""false"" ExternalAccess=""None"">
                <Data Format=""L5K"">
                <![CDATA[123456]]>
                </Data>
                <Data Format=""Decorated"">
                <DataValue DataType=""DINT"" Radix=""Decimal"" Value=""123456""/>
                </Data>
                </Tag>";
        }

        private static string GetPredefinedTagData()
        {
            return
                @"<Tag Name=""TestTimer"" TagType=""Base"" DataType=""TIMER"" Constant=""false"" ExternalAccess=""Read Only"">
                <Description>
                <![CDATA[Test Timer]]>
                </Description>
                <Data Format=""L5K"">
                <![CDATA[[0,1000,0]]]>
                </Data>
                <Data Format=""Decorated"">
                <Structure DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""1000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""0""/>
                </Structure>
                </Data>
                </Tag>";
        }
    }
}