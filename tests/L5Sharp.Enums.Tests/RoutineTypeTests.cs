using System;
using FluentAssertions;
using NUnit.Framework;

namespace L5Sharp.Enums.Tests
{
    [TestFixture]
    public class RoutineTypeTests
    {
        [Test]
        public void New_Typeless_ShouldNotBeNull()
        {
            var sut = RoutineType.Typeless;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Typeless");
        }
        
        [Test]
        public void New_Ladder_ShouldNotBeNull()
        {
            var sut = RoutineType.Rll;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Rll");
        }
        
        [Test]
        public void New_FunctionBlock_ShouldNotBeNull()
        {
            var sut = RoutineType.Fbd;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Fbd");
        }
        
        [Test]
        public void New_SequentialFunction_ShouldNotBeNull()
        {
            var sut = RoutineType.Sfc;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("Sfc");
        }
        
        [Test]
        public void New_StructuredText_ShouldNotBeNull()
        {
            var sut = RoutineType.St;

            sut.Should().NotBeNull();
            sut.Name.Should().Be("St");
        }

        [Test]
        public void CreateContent_Typeless_ShouldThrowNotSupportException()
        {
            FluentActions.Invoking(() => RoutineType.Typeless.CreateContent()).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void CreateContent_Rll_ShouldNotBeNull()
        {
            var content = RoutineType.Rll.CreateContent();

            content.Should().NotBeNull();
        }
        
        [Test]
        public void CreateContent_St_ShouldThrowNotSupportException()
        {
            FluentActions.Invoking(() => RoutineType.St.CreateContent()).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void CreateContent_Fbd_ShouldThrowNotSupportException()
        {
            FluentActions.Invoking(() => RoutineType.Fbd.CreateContent()).Should().Throw<NotSupportedException>();
        }
        
        [Test]
        public void CreateContent_Sfc_ShouldThrowNotSupportException()
        {
            FluentActions.Invoking(() => RoutineType.Sfc.CreateContent()).Should().Throw<NotSupportedException>();
        }

        [Test]
        public void FromType_ILadderLogic_ShouldBeRll()
        {
            var type = RoutineType.ForType<ILadderLogic>();

            type.Should().Be(RoutineType.Rll);
        }
    }
}