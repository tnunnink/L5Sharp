using System.Xml.Linq;
using L5Sharp.Enumerations;

namespace L5Sharp.Types
{
    public class Null : Predefined
    {
        public Null() : base(LoadElement(nameof(Null).ToUpper()))
        {
        }
        
        
    }
}