namespace L5Sharp.Builders
{
    public interface IRungBuilderStart
    {
        IRungBuilderInput When(string text);
        IRungBuilderOutput Do(string text);
    }
}