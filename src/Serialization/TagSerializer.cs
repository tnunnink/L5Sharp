using System;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Types;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Tag"/> components.
    /// </summary>
    public class TagSerializer : ILogixSerializer<Tag>
    {
        private readonly FormattedDataSerializer _formattedDataSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(Tag obj)
        {
            Check.NotNull(obj);

            var element = new XElement(typeof(Tag).GetLogixName());

            element.AddValue(obj, t => t.Name);
            element.AddText(obj, t => t.Description);
            element.AddValue(obj, t => t.TagType);
            element.AddValue(obj, t => t.AliasFor);
            element.AddValue(obj, t => t.DataType);

            if (!obj.Dimensions.IsEmpty)
                element.AddValue(obj, t => t.Dimensions);

            if (obj.MemberType == MemberType.ValueMember)
                element.AddValue(obj, t => t.Radix);

            element.AddValue(obj, t => t.Constant);
            element.AddValue(obj, t => t.ExternalAccess);

            if (obj.Comments.Any())
            {
                var comments = new XElement(L5XName.Comments);
                comments.Add(obj.Comments.Select(c =>
                {
                    var comment = new XElement(L5XName.Comment);
                    comment.AddValue(c.Key.Operand, L5XName.Operand);
                    comment.Add(new XCData(c.Value));
                    return comment;
                }));

                element.Add(comments);
            }

            if (obj.Units.Any())
            {
                var units = new XElement(L5XName.EngineeringUnits);
                units.Add(obj.Comments.Select(u =>
                {
                    var unit = new XElement(L5XName.EngineeringUnit);
                    unit.AddValue(u.Key.Operand, L5XName.Operand);
                    unit.Add(new XCData(u.Value));
                    return unit;
                }));

                element.Add(units);
            }

            var data = _formattedDataSerializer.Serialize(obj.Data);
            element.Add(data);

            return element;
        }

        /// <inheritdoc />
        public Tag Deserialize(XElement element)
        {
            Check.NotNull(element);

            var data = element.Descendants(L5XName.Data)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value != DataFormat.L5K);

            return new Tag
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                Data = data is not null ? _formattedDataSerializer.Deserialize(data) : LogixType.Null,
                ExternalAccess = element.ValueOrDefault<ExternalAccess>(L5XName.ExternalAccess) ??
                                 ExternalAccess.ReadWrite,
                TagType = element.ValueOrDefault<TagType>(L5XName.TagType) ?? TagType.Base,
                Usage = element.ValueOrDefault<TagUsage>(L5XName.Usage) ?? TagUsage.Normal,
                AliasFor = element.ValueOrDefault<TagName>(L5XName.AliasFor) ?? TagName.Empty,
                Constant = element.ValueOrDefault<bool?>(L5XName.Constant) ?? false,
                Comments = element.Descendants(L5XName.Comment)
                    .ToDictionary(
                        k => TagName.Combine(element.LogixName(), k.GetValue<string>(L5XName.Operand)),
                        e => e.Value),
                Units = element.Descendants(L5XName.EngineeringUnit)
                    .ToDictionary(
                        k => TagName.Combine(element.LogixName(), k.GetValue<string>(L5XName.Operand)),
                        e => e.Value)
            };
        }
    }
}