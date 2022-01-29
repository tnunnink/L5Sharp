using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Serialization.Tests
{
    [TestFixture]
    public class ModuleSerializerTests
    {
        private static readonly string L5X = Path.Combine(Environment.CurrentDirectory, @"TestFiles\Test.xml");
        private ModuleSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            var context = new LogixContext(L5X);
            _serializer = new ModuleSerializer(context);
        }
        
        [Test]
        public void Serialize_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _serializer.Serialize(null!)).Should().Throw<ArgumentNullException>();
        }

        /*[Test]
        public void Serialize_WhenCalled_ShouldNotBeNull()
        {
            var module = new Module("Test", "Catalog", 0, 1, 1);

            var xml = _serializer.Serialize(module);

            xml.Should().NotBeNull();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void Serialize_ValueTypeArray_ShouldBeApproved()
        {
            var module = new Module("Test", "Catalog", 0, 1, 1);

            var xml = _serializer.Serialize(module);

            Approvals.VerifyXml(xml.ToString());
        }*/

        [Test]
        public void Deserialize_ValidModule_ShouldNotBeNull()
        {
            var element = XElement.Parse(GetTestModule());

            var component = _serializer.Deserialize(element);

            component.Should().NotBeNull();
        }

        [Test]
        public void Deserialize_TestModule_ShouldHaveExpectedProperties()
        {
            var element = XElement.Parse(GetTestModule());

            var component = _serializer.Deserialize(element);

            component.Name.Should().Be("Local_Mod_4");
            component.Description.Should().BeEmpty();
            component.CatalogNumber.Should().Be("1756sc-CTR8/A");
            component.Vendor.Should().Be(58);
            component.ProductType.Should().Be(109);
            component.ProductCode.Should().Be(15);
            component.Revision.Should().Be(new Revision(1, 1));
            component.ParentModule.Should().Be("Local");
            component.ParentModPortId.Should().Be(1);
            component.Inhibited.Should().BeFalse();
            component.MajorFault.Should().BeFalse();
            component.SafetyEnabled.Should().BeFalse();
            component.State.Should().Be(KeyingState.CompatibleModule);
            component.Slot.Should().Be(4);
            component.IP.Should().BeNull();
            component.Bus.Should().BeNull();
            component.Connection.Should().BeNull();
            component.Config.Should().BeNull();
            component.Input.Should().BeNull();
            component.Output.Should().BeNull();
        }

        private static string GetTestModule()
        {
            return
                @"<Module Use=""Target"" Name=""Local_Mod_4"" CatalogNumber=""1756sc-CTR8/A"" Vendor=""58"" ProductType=""109"" ProductCode=""15"" Major=""1"" Minor=""1"" ParentModule=""Local"" ParentModPortId=""1"" Inhibited=""false""
                 MajorFault=""false"">
                <EKey State=""CompatibleModule""/>
                <Ports>
                <Port Id=""1"" Address=""4"" Type=""ICP"" Upstream=""true""/>
                </Ports>
                <Communications>
                <ConfigTag ConfigSize=""216"" ExternalAccess=""Read/Write"">
                <Data Format=""L5K"">
                <![CDATA[[220,225,1.67772150e+007,1.67772150e+007,1.67772150e+007,1.67772150e+007,1.67772150e+007
		                ,1.67772150e+007,1.67772150e+007,1.67772150e+007,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0,0,0,0,0,0
		                ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                ,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000,0.00000000e+000
		                ,0.00000000e+000,0.00000000e+000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]]]>
                </Data>
                <Data Format=""Decorated"">
                <Structure DataType=""SC:1756sc_CTR8:C:0"">
                <DataValueMember Name=""Ch0CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch1CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch2CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch3CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch4CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch5CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch6CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch7CountLimit"" DataType=""REAL"" Radix=""Float"" Value=""16777215.0""/>
                <DataValueMember Name=""Ch0Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7Preset"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch1RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch2RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch3RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch4RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch5RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch6RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch7RateSamplePeriod"" DataType=""INT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch0OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch1OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch2OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch3OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch4OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch5OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch6OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch7OperationMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch0StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch1StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch2StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch3StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch4StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch5StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch6StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch7StorageMode"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch0RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch1RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch2RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch3RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch4RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch5RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch6RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch7RangeType"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch0KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7KFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7RFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7MFactor"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch1Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch2Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch3Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch4Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch5Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch6Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""Ch7Filter"" DataType=""SINT"" Radix=""Decimal"" Value=""0""/>
                <DataValueMember Name=""GateFilter"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7GateFilter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""RollOverTo"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7RollOverTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""RollUnderTo"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7RollUnderTo"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""StopOnZero"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7StopOnZero"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""StopOnLimit"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7StopOnLimit"" DataType=""BOOL"" Value=""0""/>
                </Structure>
                </Data>
                </ConfigTag>
                <Connections>
                <Connection Name=""Output"" RPI=""10000"" Type=""Output"" EventID=""0"" ProgrammaticallySendEventTrigger=""false"" Unicast=""false"">
                <InputTag ExternalAccess=""Read/Write"">
                <Data Format=""Decorated"">
                <Structure DataType=""SC:1756sc_CTR8:I:0"">
                <DataValueMember Name=""CommStatus"" DataType=""DINT"" Radix=""Binary"" Value=""2#0000_0000_0000_0000_0000_0000_0000_0000""/>
                <DataValueMember Name=""Ch0CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7CountValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7StoredValue"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch0Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch1Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch2Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch3Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch4Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch5Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch6Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""Ch7Rate"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                <DataValueMember Name=""DisableEcho"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7DisableEcho"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""WasReset"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7WasReset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""WasPreset"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7WasPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""NewStoredData"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7NewStoredData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""NewRateData"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7NewRateData"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""GateActive"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7GateActive"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""CounterInputState"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7CounterInputState"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""DirectionInverted"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7DirectionInverted"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""CountLimitFlag"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7CountLimitFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""CountZeroFlag"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7CountZeroFlag"" DataType=""BOOL"" Value=""0""/>
                <ArrayMember Name=""CSTTimestamp"" DataType=""DINT"" Dimensions=""2"" Radix=""Hex"">
                <Element Index=""[0]"" Value=""16#0000_0000""/>
                <Element Index=""[1]"" Value=""16#0000_0000""/>
                </ArrayMember>
                </Structure>
                </Data>
                </InputTag>
                <OutputTag ExternalAccess=""Read/Write"">
                <Data Format=""L5K"">
                <![CDATA[[0,0,0,0,0,0,0,0]]]>
                </Data>
                <Data Format=""Decorated"">
                <Structure DataType=""SC:1756sc_CTR8:O:0"">
                <DataValueMember Name=""Disable"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7Disable"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""ResetCounter"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7ResetCounter"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""LoadPreset"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7LoadPreset"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""ResetFlags"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7ResetFlags"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""InvertDirection"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7InvertDirection"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""ResetNewRateFlag"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7ResetNewRateFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""ResetNewStoredFlag"" DataType=""SINT"" Radix=""Binary"" Value=""2#0000_0000""/>
                <DataValueMember Name=""Ch0ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch1ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch2ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch3ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch4ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch5ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch6ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                <DataValueMember Name=""Ch7ResetNewStoredFlag"" DataType=""BOOL"" Value=""0""/>
                </Structure>
                </Data>
                </OutputTag>
                </Connection>
                </Connections>
                </Communications>
                <ExtendedProperties>
                <public><ConfigID>400</ConfigID><Vendor>Spectrum Controls, Inc.</Vendor><CatNum>1756sc-CTR8</CatNum></public>
                </ExtendedProperties>
                </Module>";
        }
    }
}