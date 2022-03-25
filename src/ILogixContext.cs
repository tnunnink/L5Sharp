using L5Sharp.Core;
using L5Sharp.Querying;
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
        IController? Controller();

        /// <summary>
        /// Get the <see cref="IUserDefined"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IComponentRepository<IComplexType> DataTypes();

        /// <summary>
        /// Get the <see cref="IModule"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IModuleRepository Modules();
        
        /// <summary>
        /// Get the <see cref="IAddOnInstruction"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        IComponentRepository<IAddOnInstruction> Instructions();

        /// <summary>
        /// Get the <see cref="ITag{TDataType}"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        ITagRepository Tags();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        ITagRepository Tags(ComponentName program);

        /// <summary>
        /// Get the <see cref="IProgram"/> repository instance for the current <see cref="ILogixContext"/>. 
        /// </summary>
        IComponentRepository<IProgram> Programs();

        /// <summary>
        /// Get the <see cref="ITask"/> repository instance for the current <see cref="ILogixContext"/>.
        /// </summary>
        ITaskQuery Tasks();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IRungQuery Rungs();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        IRungQuery Rungs(ComponentName program);
    }
}