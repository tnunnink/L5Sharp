namespace L5Sharp
{
    public interface IParameter : ITag
    {
        bool Required { get; }
        bool Visible { get; }
        object Min { get; }
        object Max { get; }
        object Default { get; }
        void SetMin(object min);
        void SetMax(object min);
        void SetDefault(object min);
    }
}