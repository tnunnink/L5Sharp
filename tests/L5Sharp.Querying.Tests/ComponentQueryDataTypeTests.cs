using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.L5X;
using L5SharpTests;
using NUnit.Framework;

namespace L5Sharp.Querying.Tests
{
    [TestFixture]
    public class ComponentQueryDataTypeTests
    {
        [Test]
        public void All_WhenCalled_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.DataTypes().All().ToList();

            results.Should().NotBeNull();
            results.Should().NotBeEmpty();
        }

        [Test]
        public void Any_HasComponents_ShouldBeTrue()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Any();

            result.Should().BeTrue();
        }

        [Test]
        public void Any_NoComponents_ShouldBeFalse()
        {
            var context = LogixContext.Load(Known.Empty);

            var result = context.DataTypes().Any();

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_InvalidName_ShouldBeFalse()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Contains("FakeType");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_ValidName_ShouldBeTrue()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Contains("SimpleType");

            result.Should().BeTrue();
        }

        [Test]
        public void Count_HasComponents_ShouldBeGreaterThanZero()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Count();

            result.Should().BeGreaterThan(0);
        }
        
        [Test]
        public void Count_NoComponents_ShouldBeZero()
        {
            var context = LogixContext.Load(Known.Empty);

            var result = context.DataTypes().Count();

            result.Should().Be(0);
        }

        [Test]
        public void Find_NonExistingName_ShouldBeNull()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Find("FakeType");

            result.Should().BeNull();
        }

        [Test]
        public void Find_ExistingName_ShouldNotBeNull()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Find("SimpleType");

            result.Should().NotBeNull();
        }

        [Test]
        public void Find_ExistingNameCollection_ShouldHaveExpectedCount()
        {
            var context = LogixContext.Load(Known.Test);
            var names = new List<string> { "SimpleType", "ComplexType", "NestedType" };

            var results = context.DataTypes().Find(names);

            results.Should().HaveCount(3);
        }

        [Test]
        public void Find_NonExistingNameCollection_ShouldBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);
            var names = new List<string> { "FakeType", "DoesNotExist", "NotRealType" };

            var results = context.DataTypes().Find(names);

            results.Should().BeEmpty();
        }
        
        [Test]
        public void Get_NullName_ShouldThrowInvalidOperationException()
        {
            var context = LogixContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Get(null!)).Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void Get_InvalidName_ShouldThrowInvalidOperationException()
        {
            var context = LogixContext.Load(Known.Test);

            FluentActions.Invoking(() => context.DataTypes().Get("Fake")).Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Get_ValidName_ShouldNotBeNull()
        {
            var context = LogixContext.Load(Known.Test);

            var result = context.DataTypes().Get("ComplexType");

            result.Should().NotBeNull();
        }

        [Test]
        public void Names_HasNames_ShouldNotBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.DataTypes().Names().ToList();

            results.Should().NotBeEmpty();
        }
        
        [Test]
        public void Names_NoNames_ShouldBeEmpty()
        {
            var context = LogixContext.Load(Known.Empty);

            var results = context.DataTypes().Names().ToList();

            results.Should().BeEmpty();
        }
        
        [Test]
        public void Take_Negative_ShouldBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.DataTypes().Take(-1);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_Zero_ShouldBeEmpty()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.DataTypes().Take(0);

            results.Should().BeEmpty();
        }

        [Test]
        public void Take_One_ShouldHaveCountTen()
        {
            var context = LogixContext.Load(Known.Test);

            var results = context.DataTypes().Take(1);

            results.Should().HaveCount(1);
        }
    }
}