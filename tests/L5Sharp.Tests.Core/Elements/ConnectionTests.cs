using FluentAssertions;

namespace L5Sharp.Tests.Core.Elements;

[TestFixture]
public class ConnectionTests
{
    [Test]
    public void New_Default_ShouldNotBeNull()
    {
        var connection = new Connection();

        connection.Should().NotBeNull();
    }

    [Test]
    public void New_Default_ShouldHaveExpectedDefaults()
    {
        var connection = new Connection();

        connection.Name.Should().BeEmpty();
        connection.RPI.Should().Be(0);
        connection.InputCxnPoint.Should().Be(0);
        connection.InputSize.Should().Be(0);
        connection.OutputCxnPoint.Should().Be(0);
        connection.OutputSize.Should().Be(0);
        connection.Type.Should().Be(ConnectionType.Unknown);
        connection.Priority.Should().Be(ConnectionPriority.Scheduled);
        connection.InputConnectionType.Should().Be(TransmissionType.Multicast);
        connection.InputProductionTrigger.Should().Be(ProductionTrigger.Cyclic);
        connection.OutputRedundantOwner.Should().BeFalse();
        connection.Unicast.Should().BeFalse();
        connection.EventId.Should().Be(0);
        connection.InputTagSuffix.Should().Be("I");
        connection.OutputTagSuffix.Should().Be("O");
        connection.InputTag.Should().BeNull();
        connection.OutputTag.Should().BeNull();
    }

    [Test]
    public void New_Overridden_ShouldHaveExpectedValues()
    {
        var connection = new Connection
        {
            Name = "Test",
            RPI = 4000,
            InputCxnPoint = 100,
            InputSize = 10,
            OutputCxnPoint = 100,
            OutputSize = 10,
            Type = ConnectionType.Input,
            Priority = ConnectionPriority.High,
            InputConnectionType = TransmissionType.Unicast,
            InputProductionTrigger = ProductionTrigger.Application,
            OutputRedundantOwner = true,
            Unicast = true,
            EventId = 1,
            InputTagSuffix = "II",
            OutputTagSuffix = "OO",
            InputTag = new Tag { Value = ArrayData.New<DINT>(100) },
            OutputTag = new Tag { Value = ArrayData.New<BOOL>(100) }
        };

        connection.Should().NotBeNull();
        connection.Name.Should().Be("Test");
        connection.RPI.Should().Be(4000);
        connection.InputCxnPoint.Should().Be(100);
        connection.InputSize.Should().Be(10);
        connection.OutputCxnPoint.Should().Be(100);
        connection.OutputSize.Should().Be(10);
        connection.Type.Should().Be(ConnectionType.Input);
        connection.Priority.Should().Be(ConnectionPriority.High);
        connection.InputConnectionType.Should().Be(TransmissionType.Unicast);
        connection.InputProductionTrigger.Should().Be(ProductionTrigger.Application);
        connection.OutputRedundantOwner.Should().BeTrue();
        connection.Unicast.Should().BeTrue();
        connection.EventId.Should().Be(1);
        connection.InputTagSuffix.Should().Be("II");
        connection.OutputTagSuffix.Should().Be("OO");
        connection.InputTag.Should().NotBeNull();
        connection.OutputTag.Should().NotBeNull();
    }
    
    [Test]
    public Task New_Overridden_ShouldBeVerified()
    {
        var connection = new Connection
        {
            Name = "Test",
            RPI = 4000,
            InputCxnPoint = 100,
            InputSize = 10,
            OutputCxnPoint = 100,
            OutputSize = 10,
            Type = ConnectionType.Input,
            Priority = ConnectionPriority.High,
            InputConnectionType = TransmissionType.Unicast,
            InputProductionTrigger = ProductionTrigger.Application,
            OutputRedundantOwner = true,
            Unicast = true,
            EventId = 1,
            InputTagSuffix = "II",
            OutputTagSuffix = "OO",
            InputTag = new Tag { Value = ArrayData.New<DINT>(100) },
            OutputTag = new Tag { Value = ArrayData.New<BOOL>(100) }
        };

        return Verify(connection.Serialize().ToString());
    }

}