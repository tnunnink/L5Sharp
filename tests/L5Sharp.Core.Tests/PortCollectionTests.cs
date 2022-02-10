using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortCollectionTests
    {
        [Test]
        public void Index_Valid_ShouldReturnExpectedPort()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports[1];

            port.Should().NotBeNull();
            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
        }

        [Test]
        public void Index_Invalid_ShouldThrowKeyNotFoundException()
        {
            var module = new Module("Test", "1756-EN2T");

            FluentActions.Invoking(() => module.Ports[0]).Should().Throw<KeyNotFoundException>();
        }
    
        [Test]
        public void Primary_Valid_ShouldReturnExpectedPort()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Primary();

            port.Should().NotBeNull();
            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
        }

        [Test]
        public void Secondary_HasSecondary_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Secondary();

            port.Should().NotBeNull();
            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
        }

        [Test]
        public void Secondary_DoesNotHaveSecondary_ShouldBeNull()
        {
            var module = new Module("Test", "1756-IF8");

            var port = module.Ports.Secondary();

            port.Should().BeNull();
        }
        
        [Test]
        public void Connecting_HasUpstreamPort_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Connecting();

            port.Should().NotBeNull();
            port?.Id.Should().Be(1);
            port?.Type.Should().Be("ICP");
        }

        /*[Test]
        public void Connecting_DoesNotHaveUpstreamPort_ShouldBeNull()
        {
            var module = new Module("Test", "1756-IF8");

            var port = module.Ports.Connecting();

            port.Should().BeNull();
        }*/

        [Test]
        public void Local_HasDownstreamPort_ShouldNotBeNull()
        {
            var module = new Module("Test", "1756-EN2T");

            var port = module.Ports.Local();

            port.Should().NotBeNull();
            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
        }
        
        [Test]
        public void Local_DoesNotHaveDownstream_ShouldBeNull()
        {
            var module = new Module("Test", "1756-IF8");

            var port = module.Ports.Local();

            port.Should().BeNull();
        }

        [Test]
        public void Enumerate_WhenPerformed_ShouldWork()
        {
            var module = new Module("Test", "1756-IF8");

            foreach (var port in module.Ports)
            {
                port.Should().NotBeNull();
            }
        }
        
        [Test]
        public void Enumerate_AsEnumerable_ShouldWork()
        {
            var module = new Module("Test", "1756-IF8");
            
            var enumerable = (IEnumerable)module.Ports;

            foreach (var port in enumerable)
            {
                port.Should().NotBeNull();
            }
        }
    }
}