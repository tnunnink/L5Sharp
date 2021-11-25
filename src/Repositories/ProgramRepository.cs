namespace L5Sharp.Repositories
{
    internal class ProgramRepository : Repository<IProgram>, IProgramRepository
    {
        public ProgramRepository(LogixContext context) : base(context)
        {
        }
    }
}