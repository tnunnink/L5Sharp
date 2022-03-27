using System.ComponentModel;
using L5Sharp.Core;
using L5Sharp.Querying;
using L5Sharp.Repositories;

namespace L5Sharp
{
    /// <summary>
    /// The entry point for querying and manipulating an L5X file. Context is the 
    /// </summary>
    public interface ILogixContext : IRevertibleChangeTracking
    {
        /// <summary>
        /// Gets the <see cref="IController"/> instance of the current <see cref="ILogixContext"/>.
        /// </summary>
        IController? Controller();

        /// <summary>
        /// Get the <see cref="IUserDefined"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IDataTypeRepository DataTypes();

        /// <summary>
        /// Get the <see cref="IModule"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IModuleRepository Modules();
        
        /// <summary>
        /// Get the <see cref="IAddOnInstruction"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IInstructionRepository Instructions();

        /// <summary>
        /// Get the <see cref="ITag{TDataType}"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        ITagRepository Tags();
        
        ITagRepository Tags(ComponentName program);

        /// <summary>
        /// Get the <see cref="IProgram"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        IProgramRepository Programs();

        /// <summary>
        /// Get the <see cref="ITask"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        ITaskQuery Tasks();
        
        IRungQuery Rungs();
        
        IRungQuery Rungs(ComponentName program);
        
        void Save(string fileName);
    }
}