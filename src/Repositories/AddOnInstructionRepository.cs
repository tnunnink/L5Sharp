namespace L5Sharp.Repositories
{
    internal class AddOnInstructionRepository : Repository<IAddOnInstruction>
    {
        public AddOnInstructionRepository(L5XContext context) : base(context)
        {
        }
    }
}