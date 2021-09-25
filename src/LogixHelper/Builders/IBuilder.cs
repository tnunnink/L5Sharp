namespace LogixHelper.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}