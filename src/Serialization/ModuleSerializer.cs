using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization.Data;
using L5Sharp.Utilities;

namespace L5Sharp.Serialization
{
    /// <summary>
    /// A logix serializer that performs serialization of <see cref="Module"/> components.
    /// </summary>
    public class ModuleSerializer : ILogixSerializer<Module>
    {
        private readonly PortSerializer _portSerializer = new();
        private readonly ModuleConnectionSerializer _connectionSerializer = new();

        /// <inheritdoc />
        public XElement Serialize(Module obj)
        {
            Check.NotNull(obj);

            var element = new XElement(L5XName.Module);
            element.AddValue(obj.Name, L5XName.Name);
            element.AddText(obj.Description, L5XName.Description);
            element.AddValue(obj.CatalogNumber, L5XName.CatalogNumber);
            element.AddValue(obj.Vendor, L5XName.Vendor);
            element.AddValue(obj.ProductType, L5XName.ProductType);
            element.AddValue(obj.ProductCode, L5XName.ProductCode);
            element.AddValue(obj.Revision.Major, L5XName.Major);
            element.AddValue(obj.Revision.Minor, L5XName.Minor);
            element.AddValue(obj.ParentModule, L5XName.ParentModule);
            element.AddValue(obj.ParentPortId, L5XName.ParentModPortId);
            element.AddValue(obj.Inhibited, L5XName.Inhibited);
            element.AddValue(obj.MajorFault, L5XName.MajorFault);
            element.AddValue(obj.SafetyEnabled, L5XName.SafetyEnabled);

            var eKey = new XElement(L5XName.EKey);
            eKey.AddValue(obj.Keying, L5XName.State);
            element.Add(eKey);

            var ports = new XElement(L5XName.Ports);
            ports.Add(obj.Ports.Select(p => _portSerializer.Serialize(p)));
            element.Add(ports);

            var communications = new XElement(L5XName.Communications);

            if (obj.Config is not null)
            {
                var config = new XElement(L5XName.ConfigTag);
                config.Add(TagDataSerializer.DecoratedData.Serialize(obj.Config.Data));
                communications.Add(config);
            }

            var connections = new XElement(L5XName.Connections);
            connections.Add(obj.Connections.Select(c => _connectionSerializer.Serialize(c)));
            communications.Add(connections);

            element.Add(communications);

            return element;
        }

        /// <inheritdoc />
        public Module Deserialize(XElement element)
        {
            Check.NotNull(element);

            var major = element.TryGetValue<ushort?>(L5XName.Major) ?? 1;
            var minor = element.TryGetValue<ushort?>(L5XName.Minor) ?? 0;

            return new Module
            {
                Name = element.LogixName(),
                Description = element.LogixDescription(),
                CatalogNumber = element.TryGetValue<string>(L5XName.CatalogNumber) ?? string.Empty,
                Vendor = element.TryGetValue<Vendor>(L5XName.Vendor) ?? Vendor.Unknown,
                ProductType = element.TryGetValue<ProductType>(L5XName.ProductType) ?? ProductType.Unknown,
                ProductCode = element.TryGetValue<ushort?>(L5XName.ProductCode) ?? 0,
                Revision = Revision.Parse($"{major}.{minor}"),
                ParentModule = element.TryGetValue<string>(L5XName.ParentModule) ?? string.Empty,
                ParentPortId = element.TryGetValue<int?>(L5XName.ParentModPortId) ?? 0,
                Inhibited = element.TryGetValue<bool?>(L5XName.Inhibited) ?? false,
                MajorFault = element.TryGetValue<bool?>(L5XName.MajorFault) ?? false,
                SafetyEnabled = element.TryGetValue<bool?>(L5XName.SafetyEnabled) ?? false,
                Keying = element.Element(L5XName.EKey)?.TryGetValue<ElectronicKeying>(L5XName.State) ??
                         ElectronicKeying.CompatibleModule,
                Config = DeserializeConfig(element),
                Ports = element.Descendants(L5XName.Port).Select(p => _portSerializer.Deserialize(p)).ToList(),
                Connections = element.Descendants(L5XName.Connection).Select(c => _connectionSerializer.Deserialize(c))
                    .ToList(),
            };
        }
        
        private static Tag? DeserializeConfig(XElement element)
        {
            var tag = element.Descendants(L5XName.ConfigTag).FirstOrDefault();
            var data = tag?.Elements(L5XName.Data)
                .FirstOrDefault(e => e.Attribute(L5XName.Format)?.Value == DataFormat.Decorated);

            if (tag is null || data is null) return null;

            return new Tag
            {
                Name = tag.ModuleTagName(),
                Data = TagDataSerializer.DecoratedData.Deserialize(data),
                ExternalAccess = element.TryGetValue<ExternalAccess>(L5XName.ExternalAccess) ?? ExternalAccess.ReadWrite
            };
        }
    }
}