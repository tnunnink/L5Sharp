namespace L5Sharp
{
    public interface IMutableLogixComponent : ILogixComponent
    {
        void SetName(string name);
        void SetDescription(string description);
    }
}