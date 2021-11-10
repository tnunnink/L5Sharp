using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using L5Sharp.Exceptions;
using NUnit.Framework;

namespace L5Sharp.Abstractions.Tests
{
    [TestFixture]
    public class ComponentCollectionTests
    {
        private ComponentCollection<TestLogixComponent> _collection;
        private TestLogixComponent _component1;
        private TestLogixComponent _component2;
        private TestLogixComponent _component3;

        [SetUp]
        public void Setup()
        {
            _component1 = new TestLogixComponent("Component1", "Test component 1");
            _component2 = new TestLogixComponent("Component2", "Test component 2");
            _component3 = new TestLogixComponent("Component3", string.Empty);
            var components = new List<TestLogixComponent> { _component1, _component2, _component3 };
            _collection = new ComponentCollection<TestLogixComponent>(components);
        }

        [Test]
        public void New_DefaultConstructor_ShouldNotBeNull()
        {
            var collection = new ComponentCollection<TestLogixComponent>();

            collection.Should().NotBeNull();
        }
        
        [Test]
        public void New_FromList_ShouldNotBeNull()
        {
            var list = new List<TestLogixComponent> { new("Test", "Test") };
            
            var collection = new ComponentCollection<TestLogixComponent>(list);

            collection.Should().NotBeNull();
            collection.Should().HaveCount(1);
        }
        
        [Test]
        public void New_Enumerable_ShouldNotBeNull()
        {
            var list = new List<TestLogixComponent> { new("Test1", "Test"), new("Test2", "Test") };
            
            var collection = new ComponentCollection<TestLogixComponent>(list);

            collection.Should().NotBeNull();
            collection.Should().HaveCount(2);
        }
        
        [Test]
        public void Contains_Exists_ShouldBeTrue()
        {
            var result = _collection.Contains("Component1");

            result.Should().BeTrue();
        }
        
        [Test]
        public void Contains_DoesNotExist_ShouldBeFalse()
        {
            var result = _collection.Contains("Test");

            result.Should().BeFalse();
        }

        [Test]
        public void Contains_Null_ShouldBeFalse()
        {
            var result = _collection.Contains(null);

            result.Should().BeFalse();
        }
        
        [Test]
        public void Get_Exists_ShouldBeExpected()
        {
            var result = _collection.Get(_component1.Name);

            result.Should().NotBeNull();
            result.Should().Be(_component1);
        }
        
        [Test]
        public void Get_DoesNotExist_ShouldBeNull()
        {
            var component = new TestLogixComponent("Test", "This is a test");
            
            var result = _collection.Get(component.Name);

            result.Should().BeNull();
        }

        [Test]
        public void Get_Null_ShouldBeNull()
        {
            var result = _collection.Get((string)null);

            result.Should().BeNull();
        }

        [Test]
        public void Get_PredicateExists_ShouldNotBeNullAndBeExpected()
        {
            var result = _collection.Get(c => c.Name == "Component2");

            result.Should().NotBeNull();
            result.Should().Be(_component2);
        }
        
        [Test]
        public void Get_PredicateDoesNotExist_ShouldBeNull()
        {
            var result = _collection.Get(c => c.Name == "Test");

            result.Should().BeNull();
        }
        
        [Test]
        public void Find_PredicateExists_ShouldNotBeEmpty()
        {
            var results = _collection.Find(c => c.Description != string.Empty).ToList();

            results.Should().NotBeEmpty();
            results.Should().Contain(_component1);
            results.Should().Contain(_component2);
            results.Should().NotContain(_component3);
        }
        
        [Test]
        public void Find_PredicateDoesNotExist_ShouldBeEmpty()
        {
            var result = _collection.Find(c => c.Description.Contains("Name"));

            result.Should().BeEmpty();
        }

        [Test]
        public void Ordered_WhenCalled_ShouldHaveExpectedOrder()
        {
            var ordered = _collection.Ordered().ToList();

            ordered[0].Should().Be(_component1);
            ordered[1].Should().Be(_component2);
            ordered[2].Should().Be(_component3);
        }
        
        [Test]
        public void Ordered_AfterInserting_ShouldHaveExpectedOrder()
        {
            var component = new TestLogixComponent("Test", "This is a test"); 
            _collection.Insert(1, component);
            
            var ordered = _collection.Ordered().ToList();

            ordered[0].Should().Be(_component1);
            ordered[1].Should().Be(component);
            ordered[2].Should().Be(_component2);
            ordered[3].Should().Be(_component3);
        }
        
        [Test]
        public void Add_Exists_ShouldThrowComponentNameCollisionException()
        {
            FluentActions.Invoking(() => _collection.Add(_component3)).Should()
                .Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void Add_DoesNotExist_ShouldContainExpected()
        {
            var component = new TestLogixComponent("Test", "This is a test");
            _collection.Add(component);

            _collection.Should().Contain(component);
        }

        [Test]
        public void Add_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.Add(null)).Should().Throw<ArgumentNullException>();
        }
        
        [Test]
        public void Insert_Exists_ShouldThrowComponentNameCollisionException()
        {
            var component = new TestLogixComponent("Component1", "This is a duplicate");
            FluentActions.Invoking(() => _collection.Insert(2, component)).Should()
                .Throw<ComponentNameCollisionException>();
        }
        
        [Test]
        public void Insert_DoesNotExist_ShouldContainExpected()
        {
            var component = new TestLogixComponent("Test", "This is a test");
            _collection.Insert(0, component);

            _collection.Should().Contain(component);
        }

        [Test]
        public void Insert_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.Insert(0, null)).Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Remove_Exists_ShouldBeTrue()
        {
            _collection.Remove(_component1.Name);

            _collection.Should().NotContain(_component1);
        }
        
        [Test]
        public void Remove_DoesNotExist_ShouldBeFalse()
        {
            var component = new TestLogixComponent("Test", "This is a test");
            _collection.Remove(component.Name);

            _collection.Should().NotContain(component);
        }

        [Test]
        public void Remove_Null_ShouldBeFalse()
        {
            FluentActions.Invoking(() => _collection.Remove(null)).Should().NotThrow();
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldNotBeNull()
        {
            var enumerator = _collection.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}