using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace L5Sharp.Core
{
    /// <summary>
    /// A collection of <see cref="ITag{TDataType}"/> for the given <see cref="Module"/> that represent all existing
    /// config, input, and output controller scoped tags for the module.
    /// </summary>
    public class ModuleTags : IEnumerable<ITag<IDataType>>
    {
        private readonly List<KeyValuePair<string, ITag<IDataType>>> _tags;

        internal ModuleTags()
        {
            _tags = new List<KeyValuePair<string, ITag<IDataType>>>();
        }
        
        internal ModuleTags(ITag<IDataType>? config, IReadOnlyCollection<Connection> connections) : this()
        {
            if (config is not null)
                _tags.Add(new KeyValuePair<string, ITag<IDataType>>(nameof(Config), config));
            
            foreach (var input in connections.Select(c => c.Input))
            {
                if (input is null) continue;
                _tags.Add(new KeyValuePair<string, ITag<IDataType>>(nameof(Input), input));
            }
            
            foreach (var output in connections.Select(c => c.Output))
            {
                if (output is null) continue;
                _tags.Add(new KeyValuePair<string, ITag<IDataType>>(nameof(Output), output));
            }
        }

        /// <summary>
        /// Gets the single config tag for the <see cref="Module"/>.
        /// </summary>
        public ITag<IDataType>? Config => _tags.SingleOrDefault(t => t.Key == nameof(Config)).Value;
        
        /// <summary>
        /// Gets the first input tag for the module connection.
        /// </summary>
        public ITag<IDataType>? Input => _tags.FirstOrDefault(t => t.Key == nameof(Input)).Value;
        
        /// <summary>
        /// Gets the output tag for the <see cref="Module"/>.
        /// </summary>
        public ITag<IDataType>? Output => _tags.FirstOrDefault(t => t.Key == nameof(Output)).Value;


        /// <inheritdoc />
        public IEnumerator<ITag<IDataType>> GetEnumerator()
        {
            return _tags.Select(x => x.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}