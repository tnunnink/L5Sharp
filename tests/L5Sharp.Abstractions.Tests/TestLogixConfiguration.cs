using L5Sharp.Configurations;

namespace L5Sharp.Abstractions.Tests
{
    public interface ITestLogixConfiguration
    {
        ITestLogixConfiguration SetName(string name);
        ITestLogixConfiguration SetDescription(string description);
    }
    public class TestLogixConfiguration : ITestLogixConfiguration, IComponentConfiguration<TestLogixComponent>
    {
        private string _description;
        private string _name;

        public TestLogixComponent Compile()
        {
            return new TestLogixComponent(_name, _description);
        }

        public ITestLogixConfiguration SetName(string name)
        {
            _name = name;
            return this;
        }

        public ITestLogixConfiguration SetDescription(string description)
        {
            _description = description;
            return this;
        }
    }
}