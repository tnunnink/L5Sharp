namespace L5Sharp.Builders
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public interface IComponentBuilder<out TComponent> where TComponent : ILogixComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TComponent Create();
    }
}