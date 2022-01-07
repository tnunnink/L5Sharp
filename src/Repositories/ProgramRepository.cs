namespace L5Sharp.Repositories
{
    internal class ProgramRepository : Repository<IProgram>
    {
        public ProgramRepository(LogixContext context) : base(context)
        {
        }
    }
}