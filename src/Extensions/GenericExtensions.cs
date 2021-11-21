using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using L5Sharp.Abstractions;
using L5Sharp.Core;
using L5Sharp.Serialization;
namespace L5Sharp.Extensions
{
    internal static class GenericExtensions
    {
        public static XAttribute ToAttribute<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be of type MemberExpression");

            var value = propertyExpression.Compile().Invoke(component);

            return new XAttribute(memberExpression.Member.Name, value);
        }

        public static XElement ToElement<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> propertyExpression, bool useCDataElement = true)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be of type MemberExpression");
            
            var value = propertyExpression.Compile().Invoke(component);

            return useCDataElement
                ? new XElement(memberExpression.Member.Name, new XCData(value.ToString()))
                : new XElement(memberExpression.Member.Name, value);
        }
    }
}