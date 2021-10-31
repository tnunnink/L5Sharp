using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class Tags : ComponentCollection<ITag>, ITags
    {
        public void Add(string name, IDataType dataType, Action<ITagConfiguration> config = null)
        {
            var configuration = new TagConfiguration(name, dataType);

            config?.Invoke(configuration);

            Add(configuration);
        }
    }
}