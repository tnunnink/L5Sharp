using System.Collections.Generic;
using LogixHelper.Primitives;

namespace LogixHelper.Helpers
{
    public class PredefinedGenerator
    {
        public static List<DataTypeMember> TimerMembers()
        {
            return new List<DataTypeMember>
            {
                new DataTypeMember("PRE", 	DataType.Dint),
                new DataTypeMember("ACC", 	DataType.Dint),
                new DataTypeMember("EN", 	DataType.Bool),
                new DataTypeMember("TT", 	DataType.Bool),
                new DataTypeMember("DN", 	DataType.Bool),
            };
        }
        
        //todo update members
        public static List<DataTypeMember> CounterMembers()
        {
            return new List<DataTypeMember>
            {
                new DataTypeMember("PRE", 	DataType.Dint),
                new DataTypeMember("ACC", 	DataType.Dint),
                new DataTypeMember("EN", 	DataType.Bool),
                new DataTypeMember("TT", 	DataType.Bool),
                new DataTypeMember("DN", 	DataType.Bool),
            };
        }
    }
}