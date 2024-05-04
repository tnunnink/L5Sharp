using FluentAssertions;

namespace L5Sharp.Tests.Components
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void New_Default_ShouldNotBeNull()
        {
            var task = new LTask();

            task.Should().NotBeNull();
        }

        [Test]
        public void New_Default_ShouldHaveDefaults()
        {
            var task = new LTask();

            task.Name.Should().BeEmpty();
            task.Type.Should().Be(TaskType.Periodic);
            task.Description.Should().BeNull();
            task.Priority.Should().Be(new TaskPriority(10));
            task.Rate.Should().Be(new ScanRate(10));
            task.Watchdog.Should().Be(new Watchdog(500));
            task.InhibitTask.Should().BeFalse();
            task.DisableUpdateOutputs.Should().BeFalse();
            task.Scheduled.Should().BeEmpty();
        }

        [Test]
        public void New_WithValues_ShouldHaveExpectedValues()
        {
            var task = new LTask
            {
                Name = "Test",
                Type = TaskType.Continuous,
                Description = "This is a test",
                Priority = new TaskPriority(13),
                Rate = new ScanRate(300),
                Watchdog = new Watchdog(501),
                InhibitTask = true,
                DisableUpdateOutputs = true,
            };

            task.Name.Should().Be("Test");
            task.Description.Should().Be("This is a test");
            task.Type.Should().Be(TaskType.Continuous);
            task.Priority.Should().Be(new TaskPriority(13));
            task.Rate.Should().Be(new ScanRate(300));
            task.Watchdog.Should().Be(new Watchdog(501));
            task.InhibitTask.Should().BeTrue();
            task.DisableUpdateOutputs.Should().BeTrue();
            task.Scheduled.Should().BeEmpty();
        }

        [Test]
        public void New_NameOverload_ShouldHaveExpectedName()
        {
            var task = new LTask("Test");

            task.Name.Should().Be("Test");
        }
        
        [Test]
        public void New_NameAndTypeOverload_ShouldHaveExpectedName()
        {
            var task = new LTask("Test", TaskType.Event);

            task.Name.Should().Be("Test");
            task.Type.Should().Be(TaskType.Event);
        }

        [Test]
        public void Clone_WhenCalled_ShouldBeAllNewReferences()
        {
            var task = new LTask { Name = "Test" };

            var clone = task.Clone<LTask>();

            clone.Should().NotBeSameAs(task);
            clone.Name.Should().Be(task.Name);

            clone.Name = "NewName";
            clone.Name.Should().NotBeSameAs(task.Name);
            task.Name.Should().Be("Test");

            clone.Type = TaskType.Event;
            task.Type.Should().Be(TaskType.Periodic);
        }

        [Test]
        public void Schedule_ValidName_ShouldHaveExpectedPrograms()
        {
            var task = new LTask();

            task.Schedule("Test");

            task.Scheduled.Should().HaveCount(1);
        }

        [Test]
        public void Cancel_Existing_ShouldHaveExpectedPrograms()
        {
            var task = new LTask();

            task.Schedule("Test");
            task.Scheduled.Should().HaveCount(1);

            task.Cancel("Test");
            task.Scheduled.Should().BeEmpty();
        }

        [Test]
        public Task Serialize_Default_ShouldBeVerified()
        {
            var task = new LTask();

            var xml = task.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task Serialize_Initialized_ShouldBeVerified()
        {
            var task = new LTask
            {
                Name = "Test",
                Type = TaskType.Continuous,
                Description = "This is a test",
                Priority = new TaskPriority(13),
                Rate = new ScanRate(300),
                Watchdog = new Watchdog(501),
                InhibitTask = true,
                DisableUpdateOutputs = true
            };

            task.Schedule("Program1");
            task.Schedule("Test");
            task.Schedule("Another");
            task.Schedule("Another");

            var xml = task.Serialize().ToString();

            return Verify(xml);
        }

        [Test]
        public Task AddProgram_ValidProgram_ShouldBeVerified()
        {
            var content = L5X.New("Test", "1756-L84E", new Revision(33, 1));
            var task = new LTask("Test");
            content.Tasks.Add(task);
            var program = new Program("Program1");
            task.AddProgram(program);

            var xml = content.Serialize().ToString();

            return VerifyXml(xml)
                .IgnoreMember("ExportDate");
        }
    }
}