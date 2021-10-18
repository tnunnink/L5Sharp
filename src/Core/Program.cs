using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Program : ProgramBase
    {
        public Program(string name, string description = null, string mainRoutineName = null,
            string faultRoutineName = null, bool useAsFolder = false, bool testEdits = false, bool disabled = false)
            : base(name, description, testEdits, disabled)
        {
            Disabled = disabled;
            MainRoutineName = mainRoutineName;
            FaultRoutineName = faultRoutineName;
            UseAsFolder = useAsFolder;
            TestEdits = testEdits;
        }

        public override ProgramType Type => ProgramType.Normal;
        public bool UseAsFolder { get; }
        public string MainRoutineName { get; set; }
        public string FaultRoutineName { get; set; }
    }
}