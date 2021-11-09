namespace L5Sharp
{
    public interface IInstantiable<out T>
    {
        T Instantiate();
    }

    public interface IPrototype<out T>
    {
        T Copy();
    }
}