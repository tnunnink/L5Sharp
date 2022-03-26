using L5Sharp.Core;
using L5Sharp.L5X;
using L5Sharp.Querying;
using L5Sharp.Serialization.Components;

namespace L5Sharp.Repositories
{
    internal class InstructionRepository : InstructionQuery, IInstructionRepository
    {
        private readonly L5XDocument _document;

        public InstructionRepository(L5XDocument document)
            : base(document.Components.Get<IAddOnInstruction>(), document.Serializers.Get<AddOnInstructionSerializer>())
        {
            _document = document;
        }

        public void Add(IAddOnInstruction component)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(ComponentName name)
        {
            throw new System.NotImplementedException();
        }

        public void Update(IAddOnInstruction component)
        {
            throw new System.NotImplementedException();
        }
    }
}