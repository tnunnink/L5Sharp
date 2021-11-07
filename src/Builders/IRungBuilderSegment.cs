namespace L5Sharp.Builders
{
    public interface IRungBuilderSegment
    {
        IRungBuilderInput When(string text);
        IRungBuilderOutput Do(string text);
    }
}