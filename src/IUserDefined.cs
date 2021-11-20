using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserDefined : IDataType
    {
        IComponentCollection<IMember<IDataType>> Members { get; }
        void SetName(ComponentName name);
        void SetDescription(string description);
    }
}