using Ardalis.SmartEnum;

namespace L5Sharp.Enums
{
    public class ProcessorType : SmartEnum<ProcessorType, string>
    {
        private ProcessorType(string name, string value) : base(name, value)
        {
        }

        public static readonly ProcessorType L71 = new ProcessorType("1756-L71", "1756-L71");
        public static readonly ProcessorType L72 = new ProcessorType("1756-L72", "1756-L71");
        public static readonly ProcessorType L72S = new ProcessorType("1756-L72S", "1756-L72S");
        public static readonly ProcessorType L73 = new ProcessorType("1756-L73", "1756-L73");
        public static readonly ProcessorType L73S = new ProcessorType("1756-L73S", "1756-L73S");
        public static readonly ProcessorType L74 = new ProcessorType("1756-L74", "1756-L74");
        public static readonly ProcessorType L75 = new ProcessorType("1756-L75", "1756-L75");
        public static readonly ProcessorType L16ERBB1B = new ProcessorType("1769-L16ER-BB1B", "1769-L16ER-BB1B");
        public static readonly ProcessorType L18ERBB1B = new ProcessorType("1769-L18ER-BB1B", "1769-L18ER-BB1B");
        public static readonly ProcessorType L18ERMBB1B = new ProcessorType("1769-L18ERM-BB1B", "1769-L18ERM-BB1B");
        public static readonly ProcessorType L24ERQB1B = new ProcessorType("1769-L24ER-QB1B", "1769-L24ER-QB1B");
        public static readonly ProcessorType L24ERQBFC1B = new ProcessorType("1769-L24ER-QBFC1B", "1769-L24ER-QBFC1B");
        public static readonly ProcessorType L27ERMQBFC1B = new ProcessorType("1769-L27ERM-QBFC1B", "1769-L27ERM-QBFC1B");
        public static readonly ProcessorType L30ER = new ProcessorType("1769-L30ER", "1769-L30ER");
        public static readonly ProcessorType L30ERM = new ProcessorType("1769-L30ERM", "1769-L30ERM");
        public static readonly ProcessorType L30ERNSE = new ProcessorType("1769-L30ER-NSE", "1769-L30ER-NSE");
        public static readonly ProcessorType L33ER = new ProcessorType("1769-L33ER", "1769-L33ER");
        public static readonly ProcessorType L33ERM = new ProcessorType("1769-L33ERM", "1769-L33ERM");
        public static readonly ProcessorType L36ERM = new ProcessorType("1769-L36ERM", "1769-L36ERM");
    }
}