namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="IComponentQuery{TResult}"/> that adds advanced querying for <see cref="IAddOnInstruction"/>
    /// elements within the L5X context.  
    /// </summary>
    public interface IInstructionQuery : IComponentQuery<IAddOnInstruction>
    {
    }
}