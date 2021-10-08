using System;
using Ardalis.SmartEnum;
using L5Sharp.Abstractions;
using L5Sharp.Core;

namespace L5Sharp.Enums
{
    public abstract class RoutineType : SmartEnum<RoutineType, string>
    {
        
        
        private RoutineType(string name, string value) : base(name, value)
        {
        }

        public abstract IRoutine Create(string name);

        public static readonly RoutineType Typeless = new TypelessType();
        public static readonly RoutineType Ladder = new LadderType();
        public static readonly RoutineType FunctionBlock = new FunctionBlockType();
        public static readonly RoutineType SequentialFunction = new SequentialFunctionType();
        public static readonly RoutineType StructuredText = new StructuredTextType();
        /*public static readonly RoutineType External = new RoutineType("External", 0); //Not supported yet
        public static readonly RoutineType Encrypted = new RoutineType("Encrypted", 0);*/ //Not supported yet
        
        private class TypelessType : RoutineType
        {
            public TypelessType() : base("Typeless", "Typeless")
            {
                
            }

            public override IRoutine Create(string name)
            {
                return null;
            }
        }
        
        private class LadderType : RoutineType
        {
            public LadderType() : base("RLL", "RLL")
            {
                
            }

            public override IRoutine Create(string name)
            {
                return new RllRoutine(name);
            }
        }
        
        private class FunctionBlockType : RoutineType
        {
            public FunctionBlockType() : base("FBD", "FBD")
            {
                
            }

            public override IRoutine Create(string name)
            {
                throw new NotImplementedException();
            }
        }
        
        private class SequentialFunctionType : RoutineType
        {
            public SequentialFunctionType() : base("SFC", "SFC")
            {
                
            }

            public override IRoutine Create(string name)
            {
                throw new NotImplementedException();
            }
        }
        
        private class StructuredTextType : RoutineType
        {
            public StructuredTextType() : base("ST", "ST")
            {
                
            }

            public override IRoutine Create(string name)
            {
                throw new NotImplementedException();
            }
        }
    }
}