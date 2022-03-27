using L5Sharp.Core;

namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="IComponentQuery{TResult}"/> that adds advanced querying for <see cref="ITag{TDataType}"/>
    /// elements within the L5X context.  
    /// </summary>
    public interface ITagQuery : IComponentQuery<ITag<IDataType>>
    {
        ITagQuery InProgram(ComponentName programName);
        
        ITagQuery WithType<TDataType>() where TDataType : IDataType;
    }
}