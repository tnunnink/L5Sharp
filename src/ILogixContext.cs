using System;
using L5Sharp.Core;
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
        /// Gets the value of the schema revision for the current L5X context.
        /// </summary>
        Revision SchemaRevision { get; }
        
        /// <summary>
        /// Gets the value of the software revision for the current L5X context.
        /// </summary>
        Revision SoftwareRevision { get; }
        
        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        ComponentName TargetName { get; }
        
        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        string TargetType { get; }
        
        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        bool ContainsContext { get; }
        
        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        string Owner { get; }
        
        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        DateTime ExportDate { get; }

        /// <summary>
        /// Get the <see cref="IUserDefined"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IRepository<IUserDefined> UserDefined { get; }
        
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
        /// Saves the content of the <see cref="ILogixContext"/> to the provided file name.
        /// </summary>
        /// <param name="fileName">The full path to the desired file name for which to save the content.
        /// If not provided, will save to the file specified when the context was loaded.</param>
        void Save(string? fileName = null);
    }
}