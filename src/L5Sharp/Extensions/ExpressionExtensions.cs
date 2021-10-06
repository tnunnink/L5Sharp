using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<XElement, bool>> Convert<T>(this Expression<Func<T, bool>> expression)
        {
            var parameters = new List<ParameterExpression>();

            var body = (BinaryExpression)expression.Body;

            foreach (var parameter in expression.Parameters)
            {
                var elementParameter = Expression.Parameter(typeof(XElement), parameter.Name);
                parameters.Add(elementParameter);
            }

            return Expression.Lambda<Func<XElement, bool>>(null);
        }
    }
}