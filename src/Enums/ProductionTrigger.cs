using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ProductionTrigger : SmartEnum<ProductionTrigger>
    {
        private ProductionTrigger(string name, int value) : base(name, value)
        {
        }
        
        public static readonly ProductionTrigger Cyclic = new ProductionTrigger(nameof(Cyclic), 0);
        public static readonly ProductionTrigger Cos = new ProductionTrigger(nameof(Cos), 1);
        public static readonly ProductionTrigger Application = new ProductionTrigger(nameof(Application), 2);
    }
}