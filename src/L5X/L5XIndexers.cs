using System;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Indexing;

namespace L5Sharp.L5X
{
    internal class L5XIndexers
    {
        private readonly Dictionary<Type, IL5XIndex<ILogixComponent>> _indexers;
        
        public L5XIndexers(L5XContext context)
        {
            _indexers = new Dictionary<Type, IL5XIndex<ILogixComponent>>
            {
                { typeof(IDataType), new DataTypeIndex(context) },
                { typeof(IModule), new ComponentIndex<IModule>(context) },
                { typeof(IAddOnInstruction), new ComponentIndex<IAddOnInstruction>(context) },
                //todo we either don't need/want a tag index or need to scope this somehow
                //{ typeof(ITag<IDataType>),new ComponentIndex<ITag<IDataType>>(context) },
                { typeof(IProgram), new ComponentIndex<IProgram>(context) },
                { typeof(ITask), new ComponentIndex<ITask>(context) }
            };
        }
        
        /// <summary>
        /// Gets a index based on the specified <see cref="ILogixComponent"/> type.
        /// </summary>
        /// <typeparam name="TComponent">The logix component for which to retrieve a index.</typeparam>
        /// <returns>The index instance that maps to the specified component type.</returns>
        public IL5XIndex<TComponent> GetFor<TComponent>() where TComponent : ILogixComponent
        {
            var target = _indexers.FirstOrDefault(t => t.Key == typeof(TComponent)).Value;
            
            if (target is not IL5XIndex<TComponent> index)
                throw new InvalidOperationException($"No index defined for'{typeof(TComponent)}'");

            return index;
        }
    }
}