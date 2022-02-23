using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <summary>
    /// The entry point for querying and manipulating an L5X file. Context is the 
    /// </summary>
    public interface ILogixContext
    {
        /// <summary>
        /// Gets the <see cref="IController"/> instance of the current <see cref="ILogixContext"/>.
        /// </summary>
        IController Controller { get; }

        /// <summary>
        /// Get the <see cref="IUserDefined"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IRepository<IUserDefined> DataTypes { get; }
        
        /// <summary>
        /// Get the <see cref="IModule"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IRepository<IModule> Modules { get; }
        
        /// <summary>
        /// Get the <see cref="ITag{TDataType}"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        IRepository<ITag<IDataType>> Tags { get; }
        
        /// <summary>
        /// Get the <see cref="IProgram"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        IRepository<IProgram> Programs { get; }
        
        /// <summary>
        /// Get the <see cref="ITask"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IReadOnlyRepository<ITask> Tasks { get; }
        
        /// <summary>
        /// Saves the content of the <see cref="ILogixContext"/>.
        /// </summary>
        void Save();
    }
}