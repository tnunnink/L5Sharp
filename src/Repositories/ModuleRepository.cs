namespace L5Sharp.Repositories
{
    /// <summary>
    /// A repository for Logix <see cref="IModule"/> components.
    /// </summary>
    internal class ModuleRepository : Repository<IModule>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ModuleRepository"/> object.
        /// </summary>
        /// <param name="context">The logix context for which to read/write data to.</param>
        public ModuleRepository(LogixContext context) : base(context)
        {
        }
        
        
    }
}