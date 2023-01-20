using System;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace L5Sharp.Serialization
{
    public interface ISerializationPropertyOptionBuilder<TProperty>
    {
        ISerializationPropertyOptionBuilder<TProperty> As(Expression<Func<XElement, XElement>> selectorExpression);
        
        ISerializationPropertyOptionBuilder<TProperty> AsAttribute();
        ISerializationPropertyOptionBuilder<TProperty> AsAttribute(string name);
        
        ISerializationPropertyOptionBuilder<TProperty> AsElement();

        ISerializationPropertyOptionBuilder<TProperty> AsElement(string name);

        ISerializationPropertyOptionBuilder<TProperty> HasConvertion(Func<string, TProperty> convertFrom,
            Func<TProperty, string> convertTo);
    }

    public interface ISerializationConfiguration<TEntity>
    {
        ISerializationPropertyOptionBuilder<TProperty> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression);

        ISerializationConfiguration<TEntity> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Expression<Func<XElement, XAttribute?>> attributeExpression,
            Func<string, TProperty>? convertFrom = null,
            Func<TProperty, string>? convertTo = null);

        ISerializationConfiguration<TEntity> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Expression<Func<XElement, XElement?>> elementExpression,
            Func<string, TProperty>? convertFrom = null,
            Func<TProperty, string>? convertTo = null);
    }

    public class SerializationConfiguration<TEntity> : ISerializationConfiguration<TEntity>
    {
        public ISerializationPropertyOptionBuilder<TProperty> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ISerializationConfiguration<TEntity> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Expression<Func<XElement, XAttribute?>> attributeExpression,
            Func<string, TProperty>? convertFrom = null,
            Func<TProperty, string>? convertTo = null)
        {
            throw new NotImplementedException();
        }

        public ISerializationConfiguration<TEntity> Register<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            Expression<Func<XElement, XElement?>> elementExpression,
            Func<string, TProperty>? convertFrom = null,
            Func<TProperty, string>? convertTo = null)
        {
            throw new NotImplementedException();
        }
    }
}