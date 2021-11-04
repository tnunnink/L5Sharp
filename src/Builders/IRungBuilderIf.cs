namespace L5Sharp.Builders
{
    public interface IRungBuilderIf
    {
        IRungBuilderInput If(INeutralText text);

        IRungBuilderInput If<TInstruction>(INeutralText<TInstruction> text) where TInstruction : IInstruction;
    }
}