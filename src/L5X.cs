using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Components;
using L5Sharp.Core;
using L5Sharp.Enums;
using L5Sharp.Extensions;
using L5Sharp.Serialization;
using L5Sharp.Utilities;

namespace L5Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public class L5X : XElement
    {
        private static readonly Dictionary<string, string> Containers = new()
        {
            { L5XName.DataType, L5XName.DataTypes },
            { L5XName.Module, L5XName.Modules },
            { L5XName.AddOnInstructionDefinition, L5XName.AddOnInstructionDefinitions },
            { L5XName.Tag, L5XName.Tags },
            { L5XName.Program, L5XName.Programs },
            { L5XName.Task, L5XName.Tasks },
            { L5XName.ParameterConnection, L5XName.ParameterConnections },
            { L5XName.Trend, L5XName.Trends },
            { L5XName.QuickWatchList, L5XName.QuickWatchLists },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public L5X(XElement content) : base(content)
        {
        }

        /// <summary>
        /// Gets the value of the schema revision for the current L5X content.
        /// </summary>
        /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the L5X schema.</value>
        /// <remarks>This is always 1.0. If the R</remarks>
        public Revision? SchemaRevision => this.TryGetValue<Revision>(L5XName.SchemaRevision);

        /// <summary>
        /// Gets the value of the software revision for the current L5X content.
        /// </summary>
        /// <value>A <see cref="Revision"/> type that represent the major/minor revision of the software.</value>
        public Revision? SoftwareRevision => this.TryGetValue<Revision>(L5XName.SoftwareRevision);

        /// <summary>
        /// Gets the name of the Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetName => this.TryGetValue<string>(L5XName.TargetName);

        /// <summary>
        /// Gets the type of Logix component that is the target of the current L5X context.
        /// </summary>
        public string? TargetType => this.TryGetValue<string>(L5XName.TargetType);

        /// <summary>
        /// Gets the value indicating whether the current L5X is contextual..
        /// </summary>
        public bool? ContainsContext => this.TryGetValue<bool>(L5XName.ContainsContext);

        /// <summary>
        /// Gets the owner that exported the current L5X file.
        /// </summary>
        public string? Owner => this.TryGetValue<string>(L5XName.Owner);

        /// <summary>
        /// Gets the date time that the L5X file was exported.
        /// </summary>
        public DateTime? ExportDate => this.LogixDateTimeOrDefault(L5XName.ExportDate);

        /// <summary>
        /// Gets the known container element for the specified component name.
        /// </summary>
        /// <param name="name">The name of the component.</param>
        /// <returns>The <see cref="XContainer"/> which representing the root for the component name.</returns>
        /// <exception cref="ArgumentException"><c>name</c> does not have a known container.</exception>
        public XContainer? GetContainer(XName name)
        {
            if (!Containers.TryGetValue(name.ToString(), out var container))
                throw new ArgumentException(
                    $"The provided name {name} does not have a corresponding component container.");
            
            return Descendants(container).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static XElement Create<TComponent>(TComponent target) where TComponent : ILogixComponent
        {
            var content = new XElement(L5XName.RSLogix5000Content);
            content.Add(new XAttribute(L5XName.SchemaRevision, new Revision().ToString()));
            content.Add(new XAttribute(L5XName.TargetName, target.Name));
            content.Add(new XAttribute(L5XName.TargetType, target.GetType().GetLogixName()));
            content.Add(new XAttribute(L5XName.ContainsContext, target is not Controller));
            content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
            //content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateFormat)));

            var serializer = LogixSerializer.GetSerializer<TComponent>();
            var component = serializer.Serialize(target);
            component.Add(new XAttribute(L5XName.Use, Use.Target));

            var controller = component.Name == L5XName.Controller ? component : CreateContext();
            EnsureContainersAdded(controller);

            if (component.Name != L5XName.Controller)
            {
                if (!Containers.TryGetValue(component.Name.ToString(), out var containerName))
                    throw new ArgumentException(
                        $"The provided name {component.Name} does not have a corresponding component container.");
                
                controller.Element(containerName)?.Add(component);
            }

            content.Add(controller);
            return content;
        }

        /// <summary>
        /// Modifies the existing root content element to ensure it contains a controller element with all
        /// known component containers (i.e. DataTypes, Modules, Tag. etc).
        /// </summary>
        /// <remarks>
        /// This ensures that adding new content will be successfully.
        /// This will maintain existing content by transferring it under the controller element
        /// if one is generate by calling this method.
        /// </remarks>
        /// <exception cref="InvalidOperationException">The current root content is not a component container.</exception>
        public void Normalize()
        {
            var content = Elements().FirstOrDefault();

            if (content is null)
            {
                EnsureContextAdded();
                return;
            }

            //if the root content is controller, then just ensure all containers are added if not.
            if (content.Name == L5XName.Controller)
            {
                EnsureContainersAdded(content);
                return;
            }

            if (!Containers.ContainsValue(content.Name.ToString()))
                throw new InvalidOperationException(
                    $"The root content element {content.Name} is not an valid component container.");

            //Get a new controller element as the context.
            var controller = CreateContext();

            //Add containers and preserve current content. 
            NormalizeContainers(controller, content);

            //Replace the current root content with the new normalized controller context.
            content.ReplaceWith(controller);
        }

        private void EnsureContextAdded()
        {
            var controller = CreateContext();
            EnsureContainersAdded(controller);
            Add(controller);
        }

        private static void NormalizeContainers(XContainer controller, XElement content)
        {
            foreach (var entry in Containers)
            {
                //Add the existing content in place of an empty container.
                if (entry.Value == content.Name)
                {
                    controller.Add(content);
                    continue;
                }

                controller.Add(new XElement(entry.Value));
            }
        }

        private static void EnsureContainersAdded(XContainer content)
        {
            foreach (var entry in Containers.Where(entry => content.Element(entry.Value) is null))
                content.Add(new XElement(entry.Value));
        }

        private static XElement CreateContext(string? name = null)
        {
            var element = new XElement(L5XName.Controller);

            element.Add(new XAttribute(L5XName.Use, Use.Context));

            if (!string.IsNullOrEmpty(name))
                element.Add(new XAttribute(L5XName.Use, Use.Context));

            return element;
        }
    }
}