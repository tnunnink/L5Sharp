using System.Collections.Generic;
using System.Linq;

namespace ModelTesting
{
    public interface IPredefined
    {
        
    }

    public abstract class DefinedType<T> where T : IPredefined
    {
    }
    
    public class Sint : DataTypeBase
    {
        public string Member01 { get; set; }
        public bool Member02 { get; set; }
        
        public override bool SupportsRadix(string radix)
        {
            return false;
        }
    }
}