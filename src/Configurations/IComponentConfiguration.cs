namespace L5Sharp.Configurations
{
    public interface IComponentConfiguration<out TComponent> where TComponent : ILogixComponent
    {
        TComponent Compile();

        internal void HasName(string name);
    }
}