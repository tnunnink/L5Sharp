using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Core.Tests
{
    [TestFixture]
    public class BulletinTests
    {
        [Test]
        public void New_InvalidLength_ShouldThrowArgumentException()
        {
            FluentActions.Invoking(() => new Bulletin(12345)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_ValidLength_ShouldNotBeNull()
        {
            var bulletin = new Bulletin(1234);

            bulletin.Should().NotBeNull();
        }

        [Test]
        public void New_EmptyString_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin(string.Empty)).Should().Throw<ArgumentException>();
        }

        [Test]
        public void New_NullString_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => new Bulletin(null!)).Should().Throw<ArgumentException>();
        }
    }
}