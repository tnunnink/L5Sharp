using System;
using System.Collections.Generic;
using L5Sharp.Abstractions;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class DataTypeMember<TDataType> :
        LogixComponent,
        IDataTypeMember<TDataType>,
        IEquatable<DataTypeMember<TDataType>>
        where TDataType : IDataType
    {
        public DataTypeMember(string name, TDataType dataType, Dimensions dimension, Radix radix,
            ExternalAccess externalAccess, string description)
            : base(name, description)
        {
            DataType = dataType;
            Dimensions = dimension ?? Dimensions.Empty;
            ExternalAccess = externalAccess != null ? externalAccess : ExternalAccess.ReadWrite;

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            var elements = new List<IMember<TDataType>>(Dimensions);
            for (var i = 0; i < Dimensions; i++)
                elements.Add(new Member<TDataType>($"{name}[{i}]", (TDataType) DataType.Instantiate(),
                    Dimensions.Empty, radix, externalAccess, description));

            Elements = elements.ToArray();
        }

        public TDataType DataType { get; }
        public Dimensions Dimensions { get; private set; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; private set; }
        public IMember<TDataType>[] Elements { get; private set; }

        public void SetDimensions(Dimensions dimensions)
        {
            dimensions ??= Dimensions.Empty;

            if (dimensions.AreMultiDimensional)
                throw new InvalidOperationException("Can not set data type member to a multidimensional array");

            Dimensions = dimensions;
        }

        public void SetRadix(Radix radix)
        {
            if (DataType is IAtomic atomic)
                atomic.SetRadix(radix);

            if (Elements.Length == 0) return;

            foreach (var element in Elements)
                if (element is IDataTypeMember<TDataType> dataTypeMember)
                    dataTypeMember.SetRadix(radix);
        }

        public void SetExternalAccess(ExternalAccess externalAccess)
        {
            if (externalAccess == null)
                throw new ArgumentNullException(nameof(externalAccess), "External Access property can not be null");

            ExternalAccess = externalAccess;
        }

        public bool Equals(DataTypeMember<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Equals(Radix, other.Radix) &&
                   Equals(ExternalAccess, other.ExternalAccess) && Description == other.Description &&
                   Dimensions == other.Dimensions;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((DataTypeMember<TDataType>)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Name);
            hashCode.Add(DataType);
            hashCode.Add(Radix);
            hashCode.Add(ExternalAccess);
            hashCode.Add(Dimensions);
            return hashCode.ToHashCode();
        }

        public static bool operator ==(DataTypeMember<TDataType> left, DataTypeMember<TDataType> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DataTypeMember<TDataType> left, DataTypeMember<TDataType> right)
        {
            return !Equals(left, right);
        }
    }

    public static class DataTypeMember
    {
        public static IDataTypeMember<IDataType> New(string name, IDataType dataType, 
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new DataTypeMember<IDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IDataTypeMember<IDataType> New(string name, IDataType dataType, Dimensions dimension,
            Radix radix = null, ExternalAccess externalAccess = null,
            string description = null)
        {
            return new DataTypeMember<IDataType>(name, dataType, dimension, radix, externalAccess, description);
        }

        public static IDataTypeMember<IDataType> Copy(IDataTypeMember<IDataType> member)
        {
            var dataType = member.DataType.Instantiate();
            return new DataTypeMember<IDataType>(member.Name, dataType, new Dimensions(member.Dimensions.X),
                member.Radix, member.ExternalAccess, member.Description);
        }

        public static IDataTypeMember<TDataType> OfType<TDataType>(string name, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new DataTypeMember<TDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IDataTypeMember<TDataType> OfType<TDataType>(string name, Dimensions dimensions,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new DataTypeMember<TDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }
    }
}