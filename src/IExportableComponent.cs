namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExportableComponent
    {
        /// <summary>
        /// Exports the current component into a new <see cref="ILogixContext"/> as the target component of the document. 
        /// </summary>
        /// <returns></returns>
        ILogixContext Export();
    }
}