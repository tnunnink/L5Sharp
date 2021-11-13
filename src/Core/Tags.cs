using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Builders;

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
        
        public Tags(IAddOnDefined instruction, IEnumerable<ITag<IDataType>> tags = null)
        {
        }

        public void Add(string name, IDataType dataType, Action<ITagBuilder<IDataType>> builder = null)
        {
            throw new NotImplementedException();
        }
    }
}