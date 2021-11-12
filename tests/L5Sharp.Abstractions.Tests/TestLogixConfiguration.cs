namespace L5Sharp.Abstractions.Tests
{
    public interface ITestLogixConfiguration
    {
        ITestLogixConfiguration SetName(string name);
        ITestLogixConfiguration SetDescription(string description);
    }
}