namespace L5Sharp.Builders
{
    public interface IBuilder
    {
        
    }
    
    public interface IBuilder<out T> : IBuilder
    {
        T Build();
    }
}