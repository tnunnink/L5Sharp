using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class ExternalAccessTests
    {
        [Test]
        public void None_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.None.Should().NotBeNull();
        }

        [Test]
        public void ReadOnly_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.ReadOnly.Should().NotBeNull();
        }

        [Test]
        public void ReadWrite_WhenCalled_ShouldNotBeNull()
        {
            ExternalAccess.ReadWrite.Should().NotBeNull();
        }

        [Test]
        public void MostRestrictive_NoneToNone_ShouldBeNone()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.None, ExternalAccess.None)
                .Should().Be(ExternalAccess.None);
        }

        [Test]
        public void MostRestrictive_NoneToReadOnly_ShouldBeNone()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.None, ExternalAccess.ReadOnly)
                .Should().Be(ExternalAccess.None);
        }

        [Test]
        public void MostRestrictive_NoneToReadWrite_ShouldBeNone()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.None, ExternalAccess.ReadWrite)
                .Should().Be(ExternalAccess.None);
        }

        [Test]
        public void MostRestrictive_ReadOnlyToReadOnly_ShouldBeReadOnly()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.ReadOnly, ExternalAccess.ReadOnly)
                .Should().Be(ExternalAccess.ReadOnly);
        }


        [Test]
        public void MostRestrictive_ReadOnlyToReadWrite_ShouldBeReadOnly()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.ReadOnly, ExternalAccess.ReadWrite)
                .Should().Be(ExternalAccess.ReadOnly);
        }

        [Test]
        public void MostRestrictive_ReadWriteToReadWrite_ShouldBeReadWrite()
        {
            ExternalAccess.MostRestrictive(ExternalAccess.ReadWrite, ExternalAccess.ReadWrite)
                .Should().Be(ExternalAccess.ReadWrite);
        }

        [Test]
        public void MostRestrictive_NullFirst_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => ExternalAccess.MostRestrictive(null!, ExternalAccess.ReadWrite)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void MostRestrictive_NullSecond_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => ExternalAccess.MostRestrictive(ExternalAccess.ReadWrite, null!)).Should()
                .Throw<ArgumentNullException>();
        }
    }
}