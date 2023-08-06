using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Core.Tests.Types.Predefined
{
    [TestFixture]
    public class AlarmDigitalTests
    {
        [Test]
        public void New__WhenCalled_ShouldNotBeNull()
        {
            var type = new ALARM_DIGITAL();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new ALARM_DIGITAL();

            type.Name.Should().Be(nameof(ALARM_DIGITAL));
            type.Family.Should().Be(DataTypeFamily.None);
            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Members.Should().HaveCount(28);
            type.Severity.Should().Be(0);
            type.MinDurationPRE.Should().Be(0);
            type.ShelveDuration.Should().Be(0);
            type.MaxShelveDuration.Should().Be(0);
            type.ProgTime.Should().Be(0);
            type.EnableIn.Should().Be(0);
            type.In.Should().Be(0);
            type.InFault.Should().Be(0);
            type.Condition.Should().Be(0);
            type.AckRequired.Should().Be(0);
            type.Latched.Should().Be(0);
            type.ProgAck.Should().Be(0);
            type.OperAck.Should().Be(0);
            type.ProgReset.Should().Be(0);
            type.OperReset.Should().Be(0);
            type.ProgSuppress.Should().Be(0);
            type.OperSuppress.Should().Be(0);
            type.ProgUnsuppress.Should().Be(0);
            type.OperUnsuppress.Should().Be(0);
            type.OperShelve.Should().Be(0);
            type.ProgUnshelve.Should().Be(0);
            type.OperUnshelve.Should().Be(0);
            type.ProgDisable.Should().Be(0);
            type.OperDisable.Should().Be(0);
            type.ProgEnable.Should().Be(0);
            type.OperEnable.Should().Be(0);
            type.AlarmCountReset.Should().Be(0);
            type.UseProgTime.Should().Be(0);
        }

        [Test]
        public void New_NullElement_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new ALARM_DIGITAL(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_InvalidElement_ShouldNotBeNull()
        {
            const string xml = @"<AlarmDigitalParameters />";
            var element = XElement.Parse(xml);

            var type = new ALARM_DIGITAL(element);

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_InvalidElement_ShouldThrowInvalidOperationExceptionWhenGettingProperty()
        {
            const string xml = @"<AlarmDigitalParameters />";
            var element = XElement.Parse(xml);

            var type = new ALARM_DIGITAL(element);

            FluentActions.Invoking(() => type.EnableIn).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void New_RealElement_ShouldHaveExpectedValues()
        {
            const string xml =
                @"<AlarmDigitalParameters Severity=""500"" MinDurationPRE=""0"" ShelveDuration=""0"" MaxShelveDuration=""0""
            ProgTime=""DT#1970-01-01-00:00:00.000_000Z"" EnableIn=""false"" In=""false""
            InFault=""false"" Condition=""true"" AckRequired=""true"" Latched=""false""
            ProgAck=""false"" OperAck=""false"" ProgReset=""false"" OperReset=""false""
            ProgSuppress=""false"" OperSuppress=""false"" ProgUnsuppress=""false""
            OperUnsuppress=""false"" OperShelve=""false"" ProgUnshelve=""false""
            OperUnshelve=""false""
            ProgDisable=""false"" OperDisable=""false"" ProgEnable=""false""
            OperEnable=""false"" AlarmCountReset=""false"" UseProgTime=""false""/>";
            var element = XElement.Parse(xml);

            var type = new ALARM_DIGITAL(element);

            type.Should().NotBeNull();
            type.Severity.Should().Be(500);
            type.MinDurationPRE.Should().Be(0);
            type.ShelveDuration.Should().Be(0);
            type.MaxShelveDuration.Should().Be(0);
            type.ProgTime.Should().Be(0);
            type.EnableIn.Should().Be(0);
            type.In.Should().Be(0);
            type.InFault.Should().Be(0);
            type.Condition.Should().Be(1);
            type.AckRequired.Should().Be(1);
            type.Latched.Should().Be(0);
            type.ProgAck.Should().Be(0);
            type.OperAck.Should().Be(0);
            type.ProgReset.Should().Be(0);
            type.OperReset.Should().Be(0);
            type.ProgSuppress.Should().Be(0);
            type.OperSuppress.Should().Be(0);
            type.ProgUnsuppress.Should().Be(0);
            type.OperUnsuppress.Should().Be(0);
            type.OperShelve.Should().Be(0);
            type.ProgUnshelve.Should().Be(0);
            type.OperUnshelve.Should().Be(0);
            type.ProgDisable.Should().Be(0);
            type.OperDisable.Should().Be(0);
            type.ProgEnable.Should().Be(0);
            type.OperEnable.Should().Be(0);
            type.AlarmCountReset.Should().Be(0);
            type.UseProgTime.Should().Be(0);
        }

        [Test]
        public Task Serialize_WhenCalled_ShouldBeVerified()
        {
            var type = new ALARM_DIGITAL();
            
            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}