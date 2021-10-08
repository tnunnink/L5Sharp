using System.Linq;
using FluentAssertions;
using L5Sharp.Abstractions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void New_ValidName_ShouldNotBeNull()
        {
            var program = new Program("Test");

            program.Should().NotBeNull();
        }
    }
}