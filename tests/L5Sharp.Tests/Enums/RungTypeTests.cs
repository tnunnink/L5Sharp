using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Tests.Enums
{
    [TestFixture]
    public class RungTypeTests
    {
        [Test]
        public void New_Normal_ShouldNotBeNull()
        {
            var type = RungType.Normal;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Insert_ShouldNotBeNull()
        {
            var type = RungType.Insert;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Delete_ShouldNotBeNull()
        {
            var type = RungType.Delete;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_Replace_ShouldNotBeNull()
        {
            var type = RungType.Replace;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_InsertReplace_ShouldNotBeNull()
        {
            var type = RungType.InsertReplace;

            type.Should().NotBeNull();
        }
        
        [Test]
        public void New_PendingReplace_ShouldNotBeNull()
        {
            var type = RungType.PendingReplace;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_PendingDeleteRung_ShouldNotBeNull()
        {
            var type = RungType.PendingDeleteRung;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_PendingInsertRung_ShouldNotBeNull()
        {
            var type = RungType.PendingInsertRung;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_PendingReplaceNormal_ShouldNotBeNull()
        {
            var type = RungType.PendingReplaceNormal;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_PendingReplaceInsert_ShouldNotBeNull()
        {
            var type = RungType.PendingReplaceInsert;

            type.Should().NotBeNull();
        }

        [Test]
        public void New_PendingReplaceRung_ShouldNotBeNull()
        {
            var type = RungType.PendingReplaceRung;

            type.Should().NotBeNull();
        }
    }
}