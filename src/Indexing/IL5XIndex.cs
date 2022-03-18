namespace L5Sharp.Indexing
{
    /// <summary>
    /// A class that contains an index of names and corresponding XElement instance for quick retrieval.
    /// </summary>
    /// <typeparam name="TComponent">The type of logix component the index represents.</typeparam>
    internal interface IL5XIndex<out TComponent>  where TComponent : ILogixComponent
    {
        /// <summary>
        /// Gets a component by name from the current index.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TComponent Lookup(string name);
    }
}