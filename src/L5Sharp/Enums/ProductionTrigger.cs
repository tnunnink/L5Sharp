using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ProductionTrigger : SmartEnum<ProductionTrigger>
    {
        public ProductionTrigger(string name, int value) : base(name, value)
        {
        }

        public static readonly ProductionTrigger Cyclic = new ProductionTrigger("Cyclic", 0);
        public static readonly ProductionTrigger Cos = new ProductionTrigger("COS", 1);
        public static readonly ProductionTrigger Application = new ProductionTrigger("Application", 2);
    }
}