namespace L5Sharp
{
    public interface IAddOnDefined : IDataType
    {
        IParameters Parameters { get; }
        void SetName(string name);
        void SetDescription(string description);
    }
}