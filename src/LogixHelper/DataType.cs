using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LogixHelper.Tests")]

namespace LogixHelper
{
    public class DataType
    {
        internal DataType(string name)
        {
            Class = "User";
        }
        
        public string Name { get; set; }
        public string Family { get; }
        public string Class { get; }
        public string Description { get; set; }
        public IEnumerable<DataTypeMember> Members { get; set; }
    }
}