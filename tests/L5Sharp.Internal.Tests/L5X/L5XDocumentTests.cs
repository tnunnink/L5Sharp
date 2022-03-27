using System.Xml.Linq;
using FluentAssertions;
using L5Sharp.Core;
using L5Sharp.L5X;
using NUnit.Framework;

namespace L5Sharp.Internal.Tests.L5X
{
    [TestFixture]
    public class L5XDocumentTests
    {
        [Test]
        public void New_ValidDocument_ShouldNotBeNull()
        {
            var document = new XDocument();

            var l5X = new L5XDocument(document);

            l5X.Should().NotBeNull();
        }

        [Test]
        public void Create_ValidController_ShouldNotBeNull()
        {
            var controller = new Controller("Test", "1756-L83E");

            var document = L5XDocument.Create(controller);

            document.Should().NotBeNull();
        }
    }
}