namespace L5Sharp.Base
{
    public interface IBuilder
    {
        
    }
    
    public interface IBuilder<out T> : IBuilder
    {
        T Build();
    }
}