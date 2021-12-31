namespace L5Sharp.Referencing
{
    public interface ITagReference
    {
        string Element { get; }
        
        string Program { get; }
        
        string Routine { get; }
        
        int Rung { get; }
        
        string Reference { get; }
        
        string BaseTag { get; }
        
        bool Destructive { get; }
        
        string Description { get; }
    }
}