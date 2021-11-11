using System;
using L5Sharp.Exceptions;

namespace L5Sharp.Extensions
{
    public static class TagMemberExtensions
    {
        public static void SetData<TDataType>(this ITagMember<TDataType> target, ITagMember<TDataType> source)
            where TDataType : IDataType
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), "The target tag can not be null");
            
            if (source == null)
                throw new ArgumentNullException(nameof(source), "The source tag can not be null");

            var targetData = target.GetData();
            var sourceData = source.GetData();
            
            if (targetData.GetType() != sourceData.GetType() || targetData.Name != sourceData.Name)
                throw new InvalidTagDataException(targetData, sourceData);
            
            foreach (var targetMember in target.GetMembers())
            {
                var sourceMember = source.GetMember(targetMember.Name);
                
                //perhaps they are both user defined types with the same name and different structures?
                if (sourceMember == null) continue;

                if (targetMember.GetData() is IAtomic atomic && targetMember.Dimensions.AreEmpty)
                {
                    atomic.SetValue(sourceMember.GetData());
                    continue;
                }
                
                targetMember.SetData(sourceMember);
            }
        }
    }
}