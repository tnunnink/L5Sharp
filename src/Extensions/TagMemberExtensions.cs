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
            
            if (!targetData.StructureEquals(sourceData))
                throw new InvalidTagDataException(targetData, sourceData);
            
            if (targetData is IAtomic atomic && target.Dimensions.AreEmpty)
            {
                atomic.SetValue(sourceData);
                return;
            }

            foreach (var targetMember in target.GetMembers())
            {
                var sourceMember = source[targetMember.Name];
                targetMember.SetData(sourceMember);
            }
        }
    }
}