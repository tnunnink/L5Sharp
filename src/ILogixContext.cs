using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <summary>
    /// Represents a context for reading and writing to a specific L5X file.
    /// </summary>
    /// <remarks>
    /// <c>LogixContext</c> is the main entry point for a 
    /// </remarks>
    public interface ILogixContext
    {
        /// <summary>
        /// Gets the <c>SchemaRevision</c> of the L5X. 
        /// </summary>
        string SchemaRevision { get; }
        
        /// <summary>
        /// Gets the <c>SoftwareRevision</c> of the L5X. 
        /// </summary>
        string SoftwareRevision { get; }
        
        /// <summary>
        /// Gets the <c>TargetName</c> of the L5X. 
        /// </summary>
        string TargetName { get; }
        
        /// <summary>
        /// Gets the <c>TargetType</c> of the L5X. 
        /// </summary>
        string TargetType { get; }
        
        /// <summary>
        /// Gets the <c>ContainsContext</c> of the L5X. 
        /// </summary>
        string ContainsContext { get; }
        
        /// <summary>
        /// Gets the <c>Owner</c> of the L5X. 
        /// </summary>
        string Owner { get; }
        
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