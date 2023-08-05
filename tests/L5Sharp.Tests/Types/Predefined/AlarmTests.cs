using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class AlarmTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new ALARM();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new ALARM();

            type.Name.Should().Be(nameof(ALARM));
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Members.Should().HaveCount(24);
            type.EnableIn.Should().Be(0);
            type.In.Should().Be(0);
            type.HHLimit.Should().Be(0);
            type.HLimit.Should().Be(0);
            type.LLimit.Should().Be(0);
            type.LLLimit.Should().Be(0);
            type.Deadband.Should().Be(0);
            type.ROCPosLimit.Should().Be(0);
            type.ROCNegLimit.Should().Be(0);
            type.ROCPeriod.Should().Be(0);
            type.EnableOut.Should().Be(0);
            type.HHAlarm.Should().Be(0);
            type.HAlarm.Should().Be(0);
            type.LAlarm.Should().Be(0);
            type.LLAlarm.Should().Be(0);
            type.ROCPosAlarm.Should().Be(0);
            type.ROCNegAlarm.Should().Be(0);
            type.ROC.Should().Be(0);
            type.Status.Should().Be(0);
            type.InstructFault.Should().Be(0);
            type.DeadbandInv.Should().Be(0);
            type.ROCPosLimitInv.Should().Be(0);
            type.ROCNegLimitInv.Should().Be(0);
            type.ROCPeriodInv.Should().Be(0);
        }

        [Test]
        public void New_Element_ShouldHaveExpectedValues()
        {
            const string xml = @"<StructureMember Name=""AlarmMember"" DataType=""ALARM"">
                                    <DataValueMember Name=""EnableIn"" DataType=""BOOL"" Value=""1""/>
                                    <DataValueMember Name=""In"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""HHLimit"" DataType=""REAL"" Radix=""Float""
                                                     Value=""3.40282347e+038""/>
                                    <DataValueMember Name=""HLimit"" DataType=""REAL"" Radix=""Float""
                                                     Value=""3.40282347e+038""/>
                                    <DataValueMember Name=""LLimit"" DataType=""REAL"" Radix=""Float""
                                                     Value=""-3.40282347e+038""/>
                                    <DataValueMember Name=""LLLimit"" DataType=""REAL"" Radix=""Float""
                                                     Value=""-3.40282347e+038""/>
                                    <DataValueMember Name=""Deadband"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""ROCPosLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""ROCNegLimit"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""ROCPeriod"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""EnableOut"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""HHAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""HAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""LAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""LLAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROCPosAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROCNegAlarm"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROC"" DataType=""REAL"" Radix=""Float"" Value=""0.0""/>
                                    <DataValueMember Name=""Status"" DataType=""DINT"" Radix=""Hex"" Value=""16#0000_0000""/>
                                    <DataValueMember Name=""InstructFault"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""DeadbandInv"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROCPosLimitInv"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROCNegLimitInv"" DataType=""BOOL"" Value=""0""/>
                                    <DataValueMember Name=""ROCPeriodInv"" DataType=""BOOL"" Value=""0""/>
                                </StructureMember>";
            var element = XElement.Parse(xml);

            var type = new ALARM(element);

            type.Should().NotBeNull();
            type.EnableIn.Should().Be(1);
            type.In.Should().Be(0);
            type.HHLimit.Should().Be(3.40282347e+038);
            type.HLimit.Should().Be(3.40282347e+038);
            type.LLimit.Should().Be(-3.40282347e+038);
            type.LLLimit.Should().Be(-3.40282347e+038);
            type.Deadband.Should().Be(0);
            type.ROCPosLimit.Should().Be(0);
            type.ROCNegLimit.Should().Be(0);
            type.ROCPeriod.Should().Be(0);
            type.EnableOut.Should().Be(0);
            type.HHAlarm.Should().Be(0);
            type.HAlarm.Should().Be(0);
            type.LAlarm.Should().Be(0);
            type.LLAlarm.Should().Be(0);
            type.ROCPosAlarm.Should().Be(0);
            type.ROCNegAlarm.Should().Be(0);
            type.ROC.Should().Be(0);
            type.Status.Should().Be(0);
            type.InstructFault.Should().Be(0);
            type.DeadbandInv.Should().Be(0);
            type.ROCPosLimitInv.Should().Be(0);
            type.ROCNegLimitInv.Should().Be(0);
            type.ROCPeriodInv.Should().Be(0);
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new ALARM();

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}