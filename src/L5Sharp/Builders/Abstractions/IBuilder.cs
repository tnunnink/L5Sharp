namespace L5Sharp.Builders.Abstractions
{
    public interface IBuilder
    {
        
    }
    
    public interface IBuilder<out T> : IBuilder
    {
        T Build();
    }
}