using System;
using L5Sharp.Abstractions;
using L5Sharp.Configurations;

namespace L5Sharp.Core
{
    public class Tags : ComponentCollection<ITag<IDataType>>, ITags
    {
        public Tags(IController controller)
        {
        }
        
        public Tags(IProgram program)
        {
        }
        
        public Tags(IAddOnDefined instruction)
        {
        }
        
        public void Add(string name, IDataType dataType, Action<ITagConfiguration> config = null)
        {
            var configuration = TagConfiguration.New(name);

            config?.Invoke(configuration);

            Add(configuration);
        }
    }
}