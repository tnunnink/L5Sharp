using L5Sharp.Core;

namespace L5Sharp.Abstractions.Tests
{
    public class TestLogixComponent : ILogixComponent
    {
        public TestLogixComponent(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}