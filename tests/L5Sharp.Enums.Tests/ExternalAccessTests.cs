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
        public void IsMoreRestrictive_NoneToReadOnly_ShouldBeTrue()
        {
            ExternalAccess.None.IsMoreRestrictive(ExternalAccess.ReadOnly).Should().BeTrue();
        }
        
        [Test]
        public void IsMoreRestrictive_ReadOnlyToReadWrite_ShouldBeTrue()
        {
            ExternalAccess.ReadOnly.IsMoreRestrictive(ExternalAccess.ReadWrite).Should().BeTrue();
        }
        
        [Test]
        public void IsMoreRestrictive_NoneToReadWrite_ShouldBeTrue()
        {
            ExternalAccess.None.IsMoreRestrictive(ExternalAccess.ReadWrite).Should().BeTrue();
        }
        
        
        [Test]
        public void IsMoreRestrictive_ReadOnlyToNone_ShouldBeFalse()
        {
            ExternalAccess.ReadOnly.IsMoreRestrictive(ExternalAccess.None).Should().BeFalse();
        }
        
        [Test]
        public void IsMoreRestrictive_ReadWriteToReadOnly_ShouldBeFalse()
        {
            ExternalAccess.ReadWrite.IsMoreRestrictive(ExternalAccess.ReadOnly).Should().BeFalse();
        }
        
        [Test]
        public void IsMoreRestrictive_ReadWriteToNone_ShouldBeFalse()
        {
            ExternalAccess.ReadWrite.IsMoreRestrictive(ExternalAccess.None).Should().BeFalse();
        }

        [Test]
        public void IsMoreRestrictive_NoneToNone_ShouldBeFalse()
        {
            ExternalAccess.None.IsMoreRestrictive(ExternalAccess.None).Should().BeFalse();
        }
        
        [Test]
        public void IsMoreRestrictive_ReadOnlyToReadOnly_ShouldBeFalse()
        {
            ExternalAccess.ReadOnly.IsMoreRestrictive(ExternalAccess.ReadOnly).Should().BeFalse();
        }
        
        [Test]
        public void IsMoreRestrictive_ReadWriteToReadWrite_ShouldBeFalse()
        {
            ExternalAccess.ReadWrite.IsMoreRestrictive(ExternalAccess.ReadWrite).Should().BeFalse();
        }
        
        [Test]
        public void IsMoreRestrictive_Null_ShouldBeFalse()
        {
            ExternalAccess.ReadWrite.IsMoreRestrictive(null).Should().BeFalse();
        }
    }
}