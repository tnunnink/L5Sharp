using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace LogixHelper.Utilities
{
    public static class Extensions
    {
        public static bool Contains(this string source, string value, StringComparison comparison)
        {
            return source?.IndexOf(value, comparison) >= 0;
        }
        
        /*public static T Materialize<T>(this T obj, XElement element) where T : new()
        {
            return new T(element);
        }*/
        
        /// <summary>
        /// Convert a lambda expression for a getter into a setter
        /// </summary>
        public static Action<TClass, TProperty> GetSetter<TClass, TProperty>(Expression<Func<TClass, TProperty>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod();

            var parameterT = Expression.Parameter(typeof(TClass), "x");
            var parameterTProperty = Expression.Parameter(typeof(TProperty), "y");

            var newExpression =
                Expression.Lambda<Action<TClass, TProperty>>(
                    Expression.Call(parameterT, setMethod, parameterTProperty),
                    parameterT,
                    parameterTProperty
                );

            return newExpression.Compile();
        }
    }
}