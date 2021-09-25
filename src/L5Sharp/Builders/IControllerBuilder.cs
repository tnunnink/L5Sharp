namespace L5Sharp.Builders
{
    public interface IControllerBuilder
    {
        IDataTypeBuilder DataType(string name, string description);
    }
}