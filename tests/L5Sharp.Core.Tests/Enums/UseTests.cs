using FluentAssertions;
using L5Sharp.Enums;
using NUnit.Framework;

namespace L5Sharp.Core.Tests.Enums
{
    [TestFixture]
    public class UseTests
    {
        [Test]
        public void New_Context_ShouldNotBeNull()
        {
            var use = Use.Context;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Target_ShouldNotBeNull()
        {
            var use = Use.Target;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Create_ShouldNotBeNull()
        {
            var use = Use.Create;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Append_ShouldNotBeNull()
        {
            var use = Use.Append;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Delete_ShouldNotBeNull()
        {
            var use = Use.Delete;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Insert_ShouldNotBeNull()
        {
            var use = Use.Insert;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Invalid_ShouldNotBeNull()
        {
            var use = Use.Invalid;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Overwrite_ShouldNotBeNull()
        {
            var use = Use.Overwrite;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Redefine_ShouldNotBeNull()
        {
            var use = Use.Redefine;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Reference_ShouldNotBeNull()
        {
            var use = Use.Reference;

            use.Should().NotBeNull();
        }
        
        [Test]
        public void New_Update_ShouldNotBeNull()
        {
            var use = Use.Update;

            use.Should().NotBeNull();
        }
    }
}