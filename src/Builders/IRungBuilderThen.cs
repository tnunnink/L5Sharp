namespace L5Sharp.Builders
{
    public interface IRungBuilderThen
    {
        IRungBuilderOutput Then(INeutralText text);
        IRungBuilderOutput Then<TInstruction>(INeutralText<TInstruction> text) where TInstruction : IInstruction;
    }
}