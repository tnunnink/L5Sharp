using System;

namespace L5Sharp.Builders
{
    internal class RungBuilderInput : IRungBuilderInput
    {
        public RungBuilderInput()
        {
        }

        public IRungBuilderInput And(INeutralText text)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderInput Or(INeutralText text)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderInput Branch(INeutralText text, Action<IRungBuilderInput> branch)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderThen AreEnergized()
        {
            throw new NotImplementedException();
        }
    }
}