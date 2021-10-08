

// ReSharper disable InconsistentNaming

namespace L5Sharp.Enums
{
    public class Revision
    {
        private Revision(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
        
        public int Major { get; }
        public int Minor { get; }
    }
}