using L5Sharp.L5X;

namespace L5Sharp.Repositories
{
    internal class ProgramRepository : ComponentRepository<IProgram>, IProgramRepository
    {
        public ProgramRepository(L5XDocument document) : base(document)
        {
        }
    }
}