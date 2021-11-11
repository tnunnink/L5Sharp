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
        public void Count_GetValue_ShouldBeExpected()
        {
            var count = _collection.Count;
            count.Should().Be(3);
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
        public void ChangeComponentName_ExistingComponent_ShouldUpdateInCollection()
        {
            _component1.SetName("Test");

            var component = _collection.Get("Test");

            component.Should().NotBeNull();
            component.Name.Should().Be("Test");
            component.Should().BeSameAs(_component1);
        }

        [Test]
        public void Add_SmallCollection_ShouldHaveExpectedCount()
        {
            for (var i = 0; i < 100; i++)
                _collection.Add(new TestLogixComponent($"Test{i}", "Test component"));

            _collection.Should().HaveCount(103);
        }

        [Test]
        public void Add_LargeCollection_ShouldHaveExpectedCount()
        {
            for (var i = 0; i < 10000; i++)
                _collection.Add(new TestLogixComponent($"Test{i}", "Test component"));

            _collection.Should().HaveCount(10003);
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
        public void Ordered_AfterInsertingAndRemoving_ShouldHaveExpectedOrder()
        {
            var c1 = new TestLogixComponent("Test1", "This is a test");
            var c2 = new TestLogixComponent("Test2", "This is a test");
            _collection.Insert(1, c1);
            _collection.Remove(_component1.Name);
            _collection.Insert(1, c2);

            var ordered = _collection.Ordered().ToList();

            ordered[0].Should().Be(c1);
            ordered[1].Should().Be(c2);
            ordered[2].Should().Be(_component2);
            ordered[3].Should().Be(_component3);
        }

        [Test]
        public void IndexOf_ValidComponent_ShouldBeExpectedIndex()
        {
            var index = _collection.IndexOf(_component2);

            index.Should().Be(1);
        }

        [Test]
        public void IndexOf_InvalidComponent_ShouldBeNegativeOne()
        {
            var index = _collection.IndexOf(new TestLogixComponent("Invalid", "This does not exist"));

            index.Should().Be(-1);
        }

        [Test]
        public void IndexOf_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.IndexOf(null)).Should().Throw<ArgumentNullException>();
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
        public void Add_Configuration_ShouldThrowArgumentNullException()
        {
            var config = new TestLogixConfiguration();
            config.SetName("Test").SetDescription("This is a test");
            var component = config.Compile();

            _collection.Add(config);

            _collection.Should().Contain(component);
        }

        [Test]
        public void Add_NullConfiguration_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.Add<TestLogixConfiguration>(null)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void AddRange_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.AddRange(null)).Should().Throw<ArgumentNullException>();
        }


        [Test]
        public void AddRange_ValidComponents_ShouldHaveExpectedCount()
        {
            var list = new List<TestLogixComponent>()
            {
                new("NewItem1", "Test"),
                new("NewItem2", "Test"),
                new("NewItem3", "Test")
            };

            _collection.AddRange(list);

            _collection.Should().HaveCount(6);
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
        public void Update_Null_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.Update(null)).Should().Throw<ArgumentNullException>();
        }

        /*[Test]
        public void Update_EmptyName_ShouldThrowArgumentException()
        {
            var component = new TestLogixComponent(string.Empty, "This is the updated component");
            
            FluentActions.Invoking(() => _collection.Update(component))
                .Should().Throw<ArgumentException>();
        }*/
        
        [Test]
        public void Update_NonExistingComponent_ShouldAddToCollection()
        {
            var component = new TestLogixComponent("NewComponent1", "This is the updated/new component");

            _collection.Update(component);

            _collection.Should().Contain(component);
            _collection.Should().HaveCount(4);
        }

        [Test]
        public void Update_ValidNameAndComponent_ShouldUpdatedExpectedComponent()
        {
            var component = new TestLogixComponent("Component1", "This is the updated component");

            _collection.Update(component);

            var result = _collection.Get(component.Name);
            result.Should().NotBeNull();
            result.Name.Should().Be("Component1");
            result.Description.Should().Be("This is the updated component");
            result.Should().Be(component);
        }

        [Test]
        public void Update_ValidNameAndComponent_MaintainOrder()
        {
            var component = new TestLogixComponent("Component1", "This is the updated component");

            _collection.Update(component);

            var index = _collection.IndexOf(component);
            index.Should().Be(0);
        }
        
        [Test]
        public void Update_Configuration_ShouldThrowArgumentNullException()
        {
            var config = new TestLogixConfiguration();
            config.SetName("Component1").SetDescription("This is and update test config");
            var component = config.Compile();

            _collection.Update(config);

            _collection.Should().Contain(component);
            _collection.IndexOf(component).Should().Be(0);
        }

        [Test]
        public void Update_NullConfiguration_ShouldThrowArgumentNullException()
        {
            FluentActions.Invoking(() => _collection.Update<TestLogixConfiguration>(null)).Should()
                .Throw<ArgumentNullException>();
        }

        [Test]
        public void GetEnumerator_WhenCalled_ShouldNotBeNull()
        {
            var enumerator = _collection.GetEnumerator();

            enumerator.Should().NotBeNull();
        }
    }
}