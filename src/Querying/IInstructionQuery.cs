namespace L5Sharp.Querying
{
    /// <summary>
    /// A fluent <see cref="ILogixQuery{TResult}"/> that adds advanced querying for <see cref="IAddOnInstruction"/>
    /// elements within the L5X context.  
    /// </summary>
    public interface IInstructionQuery : ILogixQuery<IAddOnInstruction>
    {
    }
}