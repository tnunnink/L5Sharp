namespace L5Sharp
{
    public interface IInstantiable<out T>
    {
        T Create();
    }
}