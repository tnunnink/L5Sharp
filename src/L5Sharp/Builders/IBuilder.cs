namespace L5Sharp.Builders
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}