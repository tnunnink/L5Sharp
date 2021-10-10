using L5Sharp.Utilities;

namespace L5Sharp.Abstractions
{
    public abstract class ComponentBase : IComponent
    {
        private string _name;
        private string _description;
        
        protected ComponentBase(string name, string description, bool validateName = true)
        {
            if (validateName)
                Validate.Name(name);
            
            _name = name;
            _description = description;
        }
        
        public virtual string Name
        {
            get => _name;
            set
            {
                Validate.Name(value);
                _name = value;
            }
        }

        public virtual string Description
        {
            get => _description;
            set => _description = value;
        }
    }
}