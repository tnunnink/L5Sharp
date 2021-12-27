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
        IUserDefinedRepository DataTypes { get; }
        
        /// <summary>
        /// Gets the <c>Tags</c> repository for the L5X. 
        /// </summary>
        ITagRepository Tags { get; }
        
        /// <summary>
        /// Gets the <c>Tasks</c> repository for the L5X. 
        /// </summary>
        ITaskRepository Tasks { get; }
        
        /// <summary>
        /// Saves the current context to the provided file name. 
        /// </summary>
        void Save(string fileName);
    }
}