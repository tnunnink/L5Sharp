using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Program : IProgram
    {
        public Program(ComponentName name, string? description = null, 
            string? mainRoutineName = null, string? faultRoutineName = null,
            bool useAsFolder = false, bool testEdits = false, bool disabled = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? string.Empty;
            TestEdits = testEdits;
            Disabled = disabled;
            UseAsFolder = useAsFolder;
            MainRoutineName = mainRoutineName ?? string.Empty;
            FaultRoutineName = faultRoutineName ?? string.Empty;
        }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public string Description { get; }

        /// <inheritdoc />
        public ProgramType Type => ProgramType.Normal;

        /// <inheritdoc />
        public bool TestEdits { get; }

        /// <inheritdoc />
        public bool Disabled { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool UseAsFolder { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string MainRoutineName { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FaultRoutineName { get; }
        
        
        public IEnumerable<ITag<IDataType>> Tags { get; }
        public IEnumerable<IRoutine<ILogixContent>> Routines { get; }
    }
}