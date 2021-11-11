namespace L5Sharp.Builders
{
    public interface IComponentBuilder<out TComponent> where TComponent : ILogixComponent
    {
        TComponent Create();
    }
}