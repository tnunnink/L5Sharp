using L5Sharp.Core;

namespace L5Sharp
{
    public interface IUserDefined : IDataType
    {
        IMembers Members { get; }
        void SetName(ComponentName name);
        void SetDescription(string description);
    }
}