using System;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.Core
{
    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void New_NullName_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Connection(null!, 10, ConnectionType.Input)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void New_ValidParameters_ShouldNotBeNull()
        {
            var connection = new Connection("Input", 1000, ConnectionType.Input);

            connection.Should().NotBeNull();
        }

        [Test]
        public void New_ValidParameters_ShouldHaveExpectedValues()
        {
            var connection = new Connection("Input", 1000, ConnectionType.Input);

            connection.Name.Should().Be("Input");
            connection.Rpi.Should().Be(1000);
            connection.Type.Should().Be(ConnectionType.Input);
            connection.Priority.Should().Be(ConnectionPriority.Scheduled);
            connection.InputConnectionType.Should().Be(TransmissionType.Multicast);
            connection.InputProductionTrigger.Should().Be(ProductionTrigger.Cyclic);
            connection.OutputRedundantOwner.Should().Be(false);
            connection.Unicast.Should().Be(false);
            connection.EventId.Should().Be(0);
        }
    }
}