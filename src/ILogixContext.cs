using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <summary>
    /// A <c>LogixContext</c> instance represents a set of  that are used to query and update a specified L5X file.
    /// </summary>
    /// <remarks>
    /// <c>LogixContext</c> is the main entry point for operating over and L5X file. 
    /// </remarks>
    public interface ILogixContext
    {
        /// <summary>
        /// Gets the <c>Content</c> for the current L5X.
        /// </summary>
        LogixContextInfo Info { get; }  
        
        /// <summary>
        /// Gets the <c>DataTypes</c> repository for the L5X. 
        /// </summary>
        IRepository<IUserDefined> DataTypes { get; }
        
        /// <summary>
        /// Gets the <c>Tags</c> repository for the L5X. 
        /// </summary>
        IRepository<ITag<IDataType>> Tags { get; }
        
        /// <summary>
        /// A <see cref="IReadOnlyRepository{TComponent}"/> of <see cref="ITask"/> components for reading Logix Tasks
        /// from the current <see cref="LogixContext"/>. 
        /// </summary>
        IReadOnlyRepository<ITask> Tasks { get; }
        
        /// <summary>
        /// /// Saves the content of the <see cref="ILogixContext"/> to the provided file name.
        /// </summary>
        /// <param name="fileName">The full path to the desired file name for which to save the content.</param>
        void Save(string fileName);
    }
}