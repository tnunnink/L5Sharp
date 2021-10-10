using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class ComponentBase : IComponent
    {
        private string _name;
        
        protected ComponentBase(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public virtual string Description { get; set; }
    }
}