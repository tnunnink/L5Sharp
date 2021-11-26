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
            Expression<Func<TComponent, TProperty>> propertyExpression, string nameOverride = null)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be of type MemberExpression");

            var name = nameOverride ?? memberExpression.Member.Name;
            var value = propertyExpression.Compile().Invoke(component);

            return new XAttribute(name, value);
        }

        public static XElement ToElement<TComponent, TProperty>(this TComponent component,
            Expression<Func<TComponent, TProperty>> propertyExpression, bool useCDataElement = true,
            string nameOverride = null)
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be of type MemberExpression");

            var name = nameOverride ?? memberExpression.Member.Name;
            var value = propertyExpression.Compile().Invoke(component);

            return useCDataElement
                ? new XElement(name, new XCData(value.ToString()))
                : new XElement(name, value);
        }
    }
}