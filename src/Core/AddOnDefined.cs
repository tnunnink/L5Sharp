using System.Collections.Generic;
using System.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class AddOnDefined : ComplexType
    {
        internal AddOnDefined(ComponentName name, IEnumerable<IMember<IDataType>>? members = null) 
            : base(name, members)
        {
        }

        /// <inheritdoc />
        public override DataTypeClass Class => DataTypeClass.AddOnDefined;

        /// <inheritdoc />
        protected override IDataType New()
        {
            return new AddOnDefined(string.Copy(Name), 
                Members.Select(m => new Member<IDataType>(string.Copy(m.Name), m.DataType.Instantiate(),
                    m.Radix, m.ExternalAccess, string.Copy(m.Description))));
        }
    }
}