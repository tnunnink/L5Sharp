using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LogixSerializer<T> : ILogixSerializer<T> where T : class, new()
    {
        private readonly Dictionary<LambdaExpression, LambdaExpression> _expressions = new();

        /// <inheritdoc />
        public abstract XElement Serialize(T obj);

        /// <inheritdoc />
        public abstract T Deserialize(XElement element);

        /// <summary>
        /// 
        /// </summary>
        protected virtual void Configure(ISerializationConfiguration<T> configuration)
        {
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyExpression"></param>
        /// <param name="attributeExpression"></param>
        /// <param name="convertFrom"></param>
        /// <param name="convertTo"></param>
        /// <typeparam name="TProperty"></typeparam>
        protected void Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression,
            Expression<Func<XElement, XAttribute?>> attributeExpression,
            Func<string, TProperty>? convertFrom = null, 
            Func<TProperty, string>? convertTo = null)
        {
            _expressions.Add(propertyExpression, attributeExpression);
        }
        
        protected void Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression,
            Expression<Func<XElement, XElement?>> elementExpression,
            Func<string, TProperty>? convertFrom = null, 
            Func<TProperty, string>? convertTo = null)
        {
            _expressions.Add(propertyExpression, elementExpression);
        }

        protected XAttribute GetAttribute(T obj, Expression<Func<T, string>> propertyExpression)
        {
            if (propertyExpression.Body is not MemberExpression memberExpression)
                throw new ArgumentException();

            var memberName = memberExpression.Member.Name;

            return new XAttribute(memberName, propertyExpression.Compile().Invoke(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected string GetRequiredAttribute(XElement element, string name)
        {
            return element.Attribute(name)?.Value ?? throw new ArgumentException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selector"></param>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected string GetRequiredAttribute<TProperty>(XElement element, Expression<Func<T, TProperty>> selector)
        {
            if (selector.Body is not MemberExpression memberExpression)
                throw new ArgumentException();

            var memberName = memberExpression.Member.Name;

            return element.Attribute(memberName)?.Value ?? throw new ArgumentException();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="selector"></param>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected string? GetOptionalAttribute<TProperty>(XElement element, Expression<Func<T, TProperty>> selector)
        {
            if (selector.Body is not MemberExpression memberExpression)
                throw new ArgumentException();

            var memberName = memberExpression.Member.Name;

            return element.Attribute(memberName)?.Value;
        }
        
        
    }
}