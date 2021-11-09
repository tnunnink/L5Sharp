namespace L5Sharp
{
    public interface IUserDefined : IDataType
    {
        IDataTypeMembers Members { get; }
        IUserDefined Instantiate();
        void SetName(string name);
        void SetDescription(string description);
    }
}