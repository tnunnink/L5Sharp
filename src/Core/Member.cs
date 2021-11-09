using System;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Core
{
    public class Member<TDataType> :
        IMember<TDataType>,
        IEquatable<Member<TDataType>>,
        IPrototype<IMember<TDataType>>
        where TDataType : IDataType
    {
        public Member(string name, TDataType dataType, Dimensions dimensions,
            Radix radix, ExternalAccess externalAccess, string description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Name can not be null");
            DataType = dataType;
            Dimensions = dimensions ?? Dimensions.Empty;
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);
            ExternalAccess = externalAccess != null ? externalAccess : ExternalAccess.ReadWrite;
            Description = description;
            var elements = new List<IMember<TDataType>>(Dimensions);
            for (var i = 0; i < Dimensions; i++)
                elements.Add(Member.Copy(this, $"{name}[{i}]"));
            Elements = elements.ToArray();
        }

        /*public Member(string name, TDataType dataType, Dimensions dimensions = null, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            : this(name, dimensions, externalAccess, description)
        {
            DataType = dataType;

            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            var elements = new List<IMember<TDataType>>(Dimensions);
            for (var i = 0; i < Dimensions; i++)
                elements.Add((IMember<TDataType>)Member.Copy((IMember<IDataType>)this, $"{name}[{i}]"));
            Elements = elements.ToArray();
        }

        public Member(string name, IReadOnlyCollection<TDataType> dataTypes, Dimensions dimensions, Radix radix = null,
            ExternalAccess externalAccess = null, string description = null)
            : this(name, dimensions, externalAccess, description)
        {
            DataType = dataTypes.FirstOrDefault();
            
            if (DataType is IAtomic atomic && radix != null)
                atomic.SetRadix(radix);

            if (Dimensions.AreEmpty)
                throw new ArgumentException("Dimensions must have length greater than zero");
            if (Dimensions.AreMultiDimensional)
                throw new ArgumentException("Member Dimensions must be of a single dimension");
            if (Dimensions.Length != dataTypes.Count)
                throw new ArgumentException("Dimensions size must match provided data type array length");

            var elements = new List<IMember<TDataType>>(Dimensions);
            elements.AddRange(dataTypes.Select((t, i) =>
                new Member<TDataType>($"{name}[{i}]", t, Dimensions.Empty, radix, externalAccess, description)));
            Elements = elements.ToArray();
        }*/

        public string Name { get; }
        public string Description { get; }
        public TDataType DataType { get; }
        public Dimensions Dimensions { get; }
        public Radix Radix => DataType.Radix;
        public ExternalAccess ExternalAccess { get; }
        public IMember<TDataType>[] Elements { get; }

        public IMember<TDataType> Copy()
        {
            var dataType = (TDataType)DataType.Instantiate();
            return (IMember<TDataType>)Member.New(Name, dataType, Dimensions.Copy(), Radix, ExternalAccess,
                Description);
        }

        public bool Equals(Member<TDataType> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(DataType, other.DataType) && Dimensions == other.Dimensions &&
                   Equals(Radix, other.Radix) && Equals(ExternalAccess, other.ExternalAccess) &&
                   Description == other.Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Member<TDataType>)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, DataType, Dimensions, Radix, ExternalAccess, Description);
        }

        public static bool operator ==(Member<TDataType> left, Member<TDataType> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Member<TDataType> left, Member<TDataType> right)
        {
            return !Equals(left, right);
        }
    }

    public static class Member
    {
        public static IMember<IDataType> New(string name, IDataType dataType, Dimensions dimensions = null,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
        {
            return new Member<IDataType>(name, dataType, dimensions, radix, externalAccess, description);
        }

        public static IMember<IDataType> Copy(IMember<IDataType> member, string newName = null)
        {
            var name = newName ?? member.Name;
            var dataType = member.DataType.Instantiate();
            var dimensions = member.Dimensions.Copy();
            var radix = Radix.FromName(member.Radix.Name);
            var externalAccess = ExternalAccess.FromName(member.ExternalAccess.Name);
            return new Member<IDataType>(name, dataType, dimensions, radix, externalAccess, member.Description);
        }

        public static IMember<TDataType> Copy<TDataType>(IMember<TDataType> member, string newName = null)
            where TDataType : IDataType
        {
            var name = newName ?? member.Name;
            var dataType = member.DataType.Instantiate();
            var dimensions = member.Dimensions.Copy();
            var radix = Radix.FromName(member.Radix.Name);
            var externalAccess = ExternalAccess.FromName(member.ExternalAccess.Name);
            return new Member<TDataType>(name, (TDataType)dataType, dimensions, radix, externalAccess,
                member.Description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Member<TDataType>(name, dataType, Dimensions.Empty, radix, externalAccess, description);
        }

        public static IMember<TDataType> OfType<TDataType>(string name, Dimensions dimension,
            Radix radix = null, ExternalAccess externalAccess = null, string description = null)
            where TDataType : IDataType, new()
        {
            var dataType = new TDataType();
            return new Member<TDataType>(name, dataType, dimension, radix, externalAccess, description);
        }
    }
}