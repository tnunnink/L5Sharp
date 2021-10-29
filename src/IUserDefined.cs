using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp
{
    public interface IUserDefined : IDataType
    {
        void SetName(string name);
        void SetDescription(string description);
        public IDataTypeMember GetMember(string name);
        void AddMember(string name, IDataType dataType, string description = null,
            Dimensions dimension = null, Radix radix = null, ExternalAccess access = null);
        void RemoveMember(string name);
    }
}