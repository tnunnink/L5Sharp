using System;
using System.Text;
using L5Sharp.Core;

namespace L5Sharp.Builders
{
    public class RungBuilder : IRungBuilderIf, IRungBuilderThen, IRungBuilder
    {
        private readonly StringBuilder _builder;
        private readonly int _number;
        private readonly string _comment;

        private RungBuilder(int number, string comment = null)
        {
            _builder = new StringBuilder();
            _number = number;
            _comment = comment;
        }

        public static IRungBuilderIf New(int number, string comment = null)
        {
            return new RungBuilder(number, comment);
        }

        public IRungBuilderThen If(INeutralText text, Action<IRungBuilderInput> input)
        {
            var builder = new RungBuilderInput();
            input.Invoke(builder);
            return this;
        }

        public IRungBuilderThen If<TInstruction>(INeutralText<TInstruction> text, Action<IRungBuilderInput> input)
            where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }

        public IRungBuilder Then(Action<IRungBuilderOutput> output)
        {
            var builder = new RungBuilderOutput();
            output.Invoke(builder);
            return this;
        }

        public IRungBuilderInput If(INeutralText text)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderInput If<TInstruction>(INeutralText<TInstruction> text) where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }

        public IRungBuilderOutput Then(INeutralText text)
        {
            throw new NotImplementedException();
        }

        public IRungBuilderOutput Then<TInstruction>(INeutralText<TInstruction> text) where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }

        public IRung Build()
        {
            return new Rung(_number, _comment, _builder.ToString());
        }
    }
}