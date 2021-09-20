using System.Collections.Generic;

namespace L5XParser.Model
{
    public class StructureMember
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        private IEnumerable<DataValueMember> Members { get; set; }
    }
}