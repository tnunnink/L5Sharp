using System.Collections.Generic;

namespace L5Sharp.Referencing
{
    public interface ICrossReferencer<out TReference>
    {
        IEnumerable<TReference> Find(string name);
    }
}