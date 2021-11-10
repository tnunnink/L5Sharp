namespace L5Sharp
{
    public interface IUserDefined : IDataType
    {
        IMembers Members { get; }
        void SetName(string name);
        void SetDescription(string description);
    }
}