namespace L5Sharp
{
    public interface IUserDefined : IDataType
    {
        new IDataTypeMembers Members { get; }
        void SetName(string name);
        void SetDescription(string description);
    }
}