namespace LogixHelper.Abstractions
{
    public interface IControllerBuilder
    {
        IDataTypeBuilder DataType(string name, string description);
    }
}