using System.Collections.Generic;

namespace L5Sharp.Core
{
    internal class Parameters : DataTypeMembers, IMemberCollection<IParameter<IDataType>>
    {
        public Parameters(IDataType parent, IEnumerable<IMember<IDataType>> members = null)
            : base(parent, members)
        {
        }
        
        public new IEnumerator<IParameter<IDataType>> GetEnumerator()
        {
            return (IEnumerator<IParameter<IDataType>>)base.GetEnumerator();
        }

        public new IParameter<IDataType> Get(ComponentName name)
        {
            return (IParameter<IDataType>) base.Get(name);
        }

        public void Add(IParameter<IDataType> member)
        {
            base.Add(member);
        }

        public void AddRange(IEnumerable<IParameter<IDataType>> members)
        {
            base.AddRange(members);
        }

        public void Update(IParameter<IDataType> member)
        {
            base.Update(member);
        }

        public void UpdateRange(IEnumerable<IParameter<IDataType>> members)
        {
            base.UpdateRange(members);
        }

        public void Insert(int index, IParameter<IDataType> member)
        {
            base.Insert(index, member);
        }
    }
}