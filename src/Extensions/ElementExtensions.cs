using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Utilities;

[assembly: InternalsVisibleTo("L5Sharp.Extensions.Tests")]

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
        public static string GetName(this XElement element) => element.Attribute("Name")?.Value;

        public static string GetDescription(this XElement element) => element.Element("Description")?.Value;

        public static string GetDataTypeName(this XElement element) => element.Attribute("DataType")?.Value;

        public static TReturn GetValue<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, parse);

        public static TProperty GetValue<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TProperty> parse)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, parse);

        #region TypedValueExtensions

         public static string GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, string>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, s => s);

        public static bool GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, bool>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToBoolean);
        public static byte GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, byte>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToByte);

        public static ushort GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, ushort>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToUInt16);

        public static short GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, short>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToInt16);

        public static uint GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, uint>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToUInt32);

        public static int GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, int>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToInt32);

        public static float GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, float>> propertyExpression)
            where TComponent : ILogixComponent => element.GetAttributeValueInternal(propertyExpression, Convert.ToSingle);

        public static DataTypeFamily GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeFamily>> propertyExpression)
            where TComponent : ILogixComponent =>
            element.GetAttributeValueInternal(propertyExpression, v => v != null ? DataTypeFamily.FromName(v) : null);

        public static DataTypeClass GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeClass>> propertyExpression)
            where TComponent : ILogixComponent =>
            element.GetAttributeValueInternal(propertyExpression, v => v != null ? DataTypeClass.FromName(v) : null);
        
        public static Dimensions GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, Dimensions>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? Dimensions.Parse(v) : null);

        public static Radix GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, Radix>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? Radix.FromName(v) : null);

        public static ExternalAccess GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, ExternalAccess>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression,
                v => v != null ? ExternalAccess.FromName(v) : null);

        public static TaskType GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, TaskType>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? TaskType.FromName(v) : null);
        
        public static TagUsage GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, TagUsage>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? TagUsage.FromName(v) : null);

        public static TaskTrigger GetValue<TComponent>(this XElement element,
            Expression<Func<TComponent, TaskTrigger>> propertyExpression)
            where TComponent : ILogixComponent
            => element.GetAttributeValueInternal(propertyExpression, v => v != null ? TaskTrigger.FromName(v) : null);

        #endregion

        private static TReturn GetAttributeValueInternal<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
            where TComponent : ILogixComponent
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"Expression must of type {typeof(MemberExpression)}");

            var memberName = memberExpression.Member.Name;
            
            var property = typeof(TComponent).GetProperty(memberName);
            var attribute = (XmlAttributeAttribute) property?
                .GetCustomAttributes(typeof(XmlAttributeAttribute), true).FirstOrDefault();
            var name = attribute != null ? attribute.AttributeName : memberName;
            
            var value = element.Attribute(name)?.Value;

            return parse(value);
        }
    }
}