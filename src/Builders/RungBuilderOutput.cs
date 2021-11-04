using System;

namespace L5Sharp.Builders
{
    internal class RungBuilderOutput : IRungBuilderOutput
    {
        public RungBuilderOutput()
        {
        }

        public IRungBuilderOutput And(INeutralText text)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderOutput Branch(INeutralText text, Action<IRungBuilderOutput> branch)
        {
            throw new NotImplementedException();
        }

        public IRungBuilder Return()
        {
            throw new NotImplementedException();
        }
    }
}