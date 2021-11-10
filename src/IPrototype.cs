namespace L5Sharp
{
    public interface IPrototype<out T>
    {
        T Copy();
    }
}