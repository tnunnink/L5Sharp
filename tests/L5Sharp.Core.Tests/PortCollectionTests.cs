using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class PortCollectionTests
    {
        [Test]
        public void New_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new PortCollection(null!)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void New_EmptyList_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new PortCollection(new List<Port>())).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_MultipleUpstreamPorts_ShouldThrowArgumentException()
        {
            var ports = new List<Port>
            {
                new(1, "ICP", "0", true),
                new(2, "Ethernet", "1.1.1.1", true)
            };

            FluentActions.Invoking(() => new PortCollection(ports)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_DuplicateIds_ShouldThrowSomething()
        {
            var ports = new List<Port>
            {
                new(1, "ICP", "0", true),
                new(1, "Ethernet", "1.1.1.1")
            };

            FluentActions.Invoking(() => new PortCollection(ports)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValidList_ShouldNotBeNull()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP")
            });

            collection.Should().NotBeNull();
        }

        [Test]
        public void Index_Valid_ShouldReturnExpectedPort()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP")
            });

            var port = collection[1];

            port.Should().NotBeNull();
            port.Id.Should().Be(1);
            port.Type.Should().Be("ICP");
        }

        [Test]
        public void Index_Invalid_ShouldThrowKeyNotFoundException()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP")
            });

            FluentActions.Invoking(() => collection[0]).Should().Throw<KeyNotFoundException>();
        }
        
        [Test]
        public void Upstream_DoesNotExist_ShouldBeNull()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0")
            });

            var port = collection.Upstream;

            port.Should().BeNull();
        }

        [Test]
        public void Upstream_Exists_ShouldReturnExpectedPort()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0", true)
            });

            var port = collection.Upstream;

            port.Should().NotBeNull();
            port?.Id.Should().Be(1);
            port?.Type.Should().Be("ICP");
            port?.Address.Should().Be(new Address("0"));
            port?.Upstream.Should().BeTrue();
        }
        
        [Test]
        public void Downstream_None_ShouldBeEmpty()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0", true)
            });

            var ports = collection.Downstream;

            ports.Should().BeEmpty();
        }

        [Test]
        public void Downstream_One_ShouldBeExcepted()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0", true),
                new(2, "Ethernet", "1.2.3.4")
            });

            var ports = collection.Downstream.ToList();

            ports.Should().HaveCount(1);
            ports.First().Id.Should().Be(2);
            ports.First().Type.Should().Be("Ethernet");
            ports.First().Address.Should().Be(new Address("1.2.3.4"));
            ports.First().Upstream.Should().BeFalse();
        }
        
        [Test]
        public void Backplane_DoesNotExist_ShouldBeNull()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "Ethernet", "1.2.3.4", true)
            });

            var port = collection.Backplane;

            port.Should().BeNull();
        }

        [Test]
        public void Backplane_Exists_ShouldReturnExpectedPort()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0"),
                new(2, "Ethernet", "1.2.3.4", true)
            });

            var port = collection.Backplane;

            port.Should().NotBeNull();
            port?.Id.Should().Be(1);
            port?.Type.Should().Be("ICP");
            port?.Address.Should().Be(new Address("0"));
            port?.Upstream.Should().BeFalse();
        }
        
        [Test]
        public void Ethernet_DoesNotExist_ShouldBeNull()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "1")
            });

            var port = collection.Ethernet;

            port.Should().BeNull();
        }

        [Test]
        public void Ethernet_Exists_ShouldReturnExpectedPort()
        {
            var collection = new PortCollection(new List<Port>
            {
                new(1, "ICP", "0", true),
                new(2, "Ethernet", "1.2.3.4")
            });

            var port = collection.Ethernet;

            port.Should().NotBeNull();
            port?.Id.Should().Be(2);
            port?.Type.Should().Be("Ethernet");
            port?.Address.Should().Be(new Address("1.2.3.4"));
            port?.Upstream.Should().BeFalse();
        }
    }
}