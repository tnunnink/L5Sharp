using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.Serialization;
using L5Sharp.Core;
using L5Sharp.Enums;

[assembly: InternalsVisibleTo("L5Sharp.Extensions.Tests")]

namespace L5Sharp.Extensions
{
    internal static class ElementExtensions
    {
        public static string GetName(this XElement element) => element.Attribute("Name")?.Value;

        public static string GetDescription(this XElement element) => element.Element("Description")?.Value;

        public static string GetDataTypeName(this XElement element) => element.Attribute("DataType")?.Value;

        public static TReturn GetAttribute<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse) =>
            element.GetAttributeInternal(propertyExpression, parse);

        public static TProperty GetAttribute<TComponent, TProperty>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TProperty> parse)
            => element.GetAttributeInternal(propertyExpression, parse);

        #region TypedValueExtensions

        public static string GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, string>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, s => s);

        public static bool GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, bool>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToBoolean);

        public static byte GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, byte>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToByte);

        public static ushort GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, ushort>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToUInt16);

        public static short GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, short>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToInt16);

        public static uint GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, uint>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToUInt32);

        public static int GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, int>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToInt32);

        public static float GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, float>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, Convert.ToSingle);

        public static ComponentName GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, ComponentName>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, s => new ComponentName(s));

        public static DataTypeFamily GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeFamily>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression,
                v => v != null ? DataTypeFamily.FromName(v) : null);

        public static DataTypeClass GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, DataTypeClass>> propertyExpression)
            =>
                element.GetAttributeInternal(propertyExpression,
                    v => v != null ? DataTypeClass.FromName(v) : null);

        public static Dimensions GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, Dimensions>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, v => v != null ? Dimensions.Parse(v) : null);

        public static Radix GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, Radix>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, v => v != null ? Radix.FromName(v) : null);

        public static ExternalAccess GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, ExternalAccess>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression,
                v => v != null ? ExternalAccess.FromName(v) : null);

        public static TaskType GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, TaskType>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, v => v != null ? TaskType.FromName(v) : null);

        public static TagUsage GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, TagUsage>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, v => v != null ? TagUsage.FromName(v) : null);

        public static TaskTrigger GetAttribute<TComponent>(this XElement element,
            Expression<Func<TComponent, TaskTrigger>> propertyExpression)
            => element.GetAttributeInternal(propertyExpression, v => v != null ? TaskTrigger.FromName(v) : null);

        #endregion


        private static TReturn GetAttributeInternal<TComponent, TProperty, TReturn>(this XElement element,
            Expression<Func<TComponent, TProperty>> propertyExpression, Func<string, TReturn> parse)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"Expression must of type {typeof(MemberExpression)}");

            var memberName = memberExpression.Member.Name;

            var property = typeof(TComponent).GetProperty(memberName);
            var attribute = (XmlAttributeAttribute)property?
                .GetCustomAttributes(typeof(XmlAttributeAttribute), true).FirstOrDefault();
            var name = attribute != null ? attribute.AttributeName : memberName;

            var value = element.Attribute(name)?.Value;

            return parse(value);
        }
    }
}