using L5Sharp.L5X;

namespace L5Sharp.Repositories
{
    internal class InstructionRepository : ComponentRepository<IAddOnInstruction>, IInstructionRepository
    {
        public InstructionRepository(L5XDocument document) : base(document)
        {
        }
    }
}