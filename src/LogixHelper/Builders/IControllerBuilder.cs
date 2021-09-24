namespace LogixHelper.Builders
{
    public interface IControllerBuilder
    {
        IDataTypeBuilder DataType(string name, string description);
    }
}