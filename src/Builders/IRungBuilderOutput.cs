namespace L5Sharp.Builders
{
    public interface IRungBuilderOutput : IRungBuilderSegment
    {
        IRungBuilderOutput And(string text);
    }
}