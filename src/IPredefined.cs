namespace L5Sharp
{
    public interface IPredefined : IDataType
    {
        IMember<IDataType> GetMember(string name);
        IMember<TType> GetMember<TType>(string name) where TType : IDataType;
    }
}