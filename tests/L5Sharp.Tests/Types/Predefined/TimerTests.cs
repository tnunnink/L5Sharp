using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Enums;
using L5Sharp.Types.Predefined;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void New_WhenCalled_ShouldNotBeNull()
        {
            var type = new TIMER();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new TIMER();

            type.Class.Should().Be(DataTypeClass.Predefined);
            type.Members.Should().HaveCount(5);
            type.DN.Should().Be(0);
            type.EN.Should().Be(0);
            type.TT.Should().Be(0);
            type.ACC.Should().Be(0);
            type.PRE.Should().Be(0);
        }

        [Test]
        public void New_Overload_ShouldHaveExpectedValues()
        {
            var timer = new TIMER
            {
                DN = false,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            timer.PRE.Should().Be(6000);
            timer.ACC.Should().Be(3403);
            timer.DN.Should().Be(false);
            timer.TT.Should().Be(true);
            timer.EN.Should().Be(true);
        }

        [Test]
        public void New_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new TIMER(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ElementWithName_ShouldNotBeNull()
        {
            const string xml = @"<StructureMember Name=""TimeMember"" DataType=""TIMER""/>";
            var element = XElement.Parse(xml);
            
            var timer = new TIMER(element);

            timer.Should().NotBeNull();
        }

        [Test]
        public void New_RealElement_ShouldHaveExpectedValues()
        {
            const string xml = @"<StructureMember Name=""TimeMember"" DataType=""TIMER"">
                <DataValueMember Name=""PRE"" DataType=""DINT"" Radix=""Decimal"" Value=""5000""/>
                <DataValueMember Name=""ACC"" DataType=""DINT"" Radix=""Decimal"" Value=""1234""/>
                <DataValueMember Name=""EN"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""TT"" DataType=""BOOL"" Value=""1""/>
                <DataValueMember Name=""DN"" DataType=""BOOL"" Value=""1""/>
                </StructureMember>";
            var element = XElement.Parse(xml);
            
            var timer = new TIMER(element);

            timer.Should().NotBeNull();
            timer.PRE.Should().Be(5000);
            timer.ACC.Should().Be(1234);
            timer.EN.Should().Be(true);
            timer.TT.Should().Be(true);
            timer.DN.Should().Be(true);
        }

        [Test]
        public void Clone_WhenCalled_ShouldBeOfSameType()
        {
            var timer = new TIMER();

            var clone = timer.Clone();

            clone.Should().BeOfType<TIMER>();
        }
        
        [Test]
        public void Clone_WhenCalled_ShouldNotBeSameInstance()
        {
            var timer = new TIMER();

            var clone = timer.Clone();

            clone.Should().NotBeSameAs(timer);
        }

        [Test]
        public void Set_ValidType_ShouldUpdateAsExpected()
        {
            var timer = new TIMER();
            var expected = new TIMER { PRE = 5000 };

            var set = timer.Set(expected).As<TIMER>();
            
            set.PRE.Should().Be(expected.PRE);
            set.Should().NotBeSameAs(timer);
            timer.PRE.Should().NotBe(expected.PRE);
        }

        [Test]
        public Task Serialize_Overload_ShouldBeVerified()
        {
            var timer = new TIMER
            {
                DN = false,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            var xml = timer.Serialize().ToString();

            return Verify(xml);
        }
    }
}