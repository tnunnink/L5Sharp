using System.Xml.Linq;
using FluentAssertions;

namespace L5Sharp.Tests.Types.Predefined
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var type = new TIMER();

            type.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveExpectedValues()
        {
            var type = new TIMER();

            type.Name.Should().Be(nameof(TIMER));
            type.Family.Should().Be(DataTypeFamily.None);
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
            var type = new TIMER
            {
                DN = true,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            type.PRE.Should().Be(6000);
            type.ACC.Should().Be(3403);
            type.DN.Should().Be(true);
            type.TT.Should().Be(true);
            type.EN.Should().Be(true);
        }

        [Test]
        public void New_NullElement_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new TIMER(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ElementWithName_ShouldNotBeNull()
        {
            const string xml = @"<StructureMember Name=""TimeMember"" DataType=""TIMER""/>";
            var element = XElement.Parse(xml);
            
            var type = new TIMER(element);

            type.Should().NotBeNull();
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
            
            var type = new TIMER(element);

            type.Should().NotBeNull();
            type.PRE.Should().Be(5000);
            type.ACC.Should().Be(1234);
            type.EN.Should().Be(true);
            type.TT.Should().Be(true);
            type.DN.Should().Be(true);
        }

        [Test]
        public void Clone_WhenCalled_ShouldBeOfSameType()
        {
            var type = new TIMER();

            var clone = type.Clone();

            clone.Should().BeOfType<TIMER>();
        }
        
        [Test]
        public void Clone_WhenCalled_ShouldNotBeSameInstance()
        {
            var type = new TIMER();

            var clone = type.Clone();

            clone.Should().NotBeSameAs(type);
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var type = new TIMER();
            
            var xml = type.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Overload_ShouldBeVerified()
        {
            var type = new TIMER
            {
                DN = false,
                PRE = 6000,
                ACC = 3403,
                TT = true,
                EN = true
            };

            var xml = type.Serialize().ToString();

            return Verify(xml);
        }
    }
}