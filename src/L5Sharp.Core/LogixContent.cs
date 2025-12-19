using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class containing information regarding the L5X export file. This information is found on the root
/// RSLogix5000Content element and is used by the Logix software to determine the context of the L5X file.
/// </summary>
[LogixElement(L5XName.RSLogix5000Content)]
public class LogixContent : LogixElement
{
    /// <summary>
    /// The date/time format for the L5X content.
    /// </summary>
    private const string DateTimeFormat = "ddd MMM d HH:mm:ss yyyy";

    /// <summary>
    /// Represents metadata and contextual information about the L5X export file, used by RSLogix 5000 software.
    /// </summary>
    /// <remarks>
    /// This class encapsulates key details such as schema and software revisions, owner information, export date, and options.
    /// It derives from <see cref="LogixElement"/>, which provides the base functionality for handling XML elements.
    /// </remarks>
    public LogixContent(XElement element) : base(element)
    {
        if (element.Name.LocalName != L5XName.RSLogix5000Content)
            throw new InvalidOperationException("Expecting root content element to have name 'RSLogix5000Content'");

        NormalizeContent(Element);
    }

    /// <summary>
    /// Gets the revision of the schema used within the L5X export file.
    /// </summary>
    public Revision SchemaRevision => GetValue(Revision.Parse) ?? new Revision();

    /// <summary>
    /// Gets the version of the software used to create the L5X export file.
    /// </summary>
    public Revision SoftwareRevision => GetValue(Revision.Parse) ?? new Revision();

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName => GetValue();

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType => GetValue();

    /// <summary>
    /// The total number of targets defined in the L5X file.
    /// </summary>
    public int? TargetCount => GetValue(int.Parse);

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual.
    /// </summary>
    public bool ContainsContext => GetValue(bool.Parse);

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner => GetValue();

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime ExportDate => GetDateTime();

    /// <summary>
    /// Gets the set of configured export options for the L5X file.
    /// </summary>
    /// <value>A collection of <see cref="string"/> indicating the option values.</value>
    public IEnumerable<string> ExportOptions => GetValues();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{TargetType}/{TargetName}/{TargetCount}".Trim('/');
    }

    /// <summary>
    /// Creates an empty <see cref="LogixContent"/> instance with default attributes and values.
    /// </summary>
    /// <returns>
    /// A new <see cref="LogixContent"/> instance representing an empty RSLogix 5000 content element constructed with default schema revision,
    /// software revision, and other attributes.
    /// </returns>
    public static LogixContent Empty()
    {
        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        content.Add(new XAttribute(L5XName.SoftwareRevision, new Revision()));
        content.Add(new XAttribute(L5XName.TargetName, string.Empty));
        content.Add(new XAttribute(L5XName.TargetType, L5XName.Controller));
        content.Add(new XAttribute(L5XName.ContainsContext, false));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixContent(content);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="LogixContent"/> class with the specified name, processor, and revision.
    /// </summary>
    /// <param name="name">The name of the controller being created.</param>
    /// <param name="processor">The processor type to be associated with the created controller.</param>
    /// <param name="revision">The software revision of the controller. If not provided, the latest revision for the
    /// specified processor is used (if found).</param>
    /// <returns>A new instance of the <see cref="LogixContent"/> class that represents the configured controller metadata.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="name"/> or <paramref name="processor"/>
    /// parameter is null or empty.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the provided processor type is not found in the </exception>
    public static LogixContent Create(string name, string processor, Revision? revision = null)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrEmpty(processor))
            throw new ArgumentNullException(nameof(processor));

        var local = Module.Local(processor, revision);
        var controller = new Controller(name, processor, local.Revision!);
        controller.Modules.Add(local);

        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, "1.0"));
        content.Add(new XAttribute(L5XName.SoftwareRevision, local.Revision!));
        content.Add(new XAttribute(L5XName.TargetName, controller.Name));
        content.Add(new XAttribute(L5XName.TargetType, L5XName.Controller));
        content.Add(new XAttribute(L5XName.ContainsContext, false));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));
        content.Add(controller.Serialize());

        return new LogixContent(content);
    }

    /// <summary>
    /// Creates a new <see cref="LogixContent"/> instance based on the provided component and optional revision.
    /// </summary>
    /// <typeparam name="TComponent">The type of the Logix component used to create the <see cref="LogixContent"/>.</typeparam>
    /// <param name="component">The Logix component used to initialize the <see cref="LogixContent"/>. It cannot be null.</param>
    /// <param name="revision">The optional software revision information. If not provided, a default value will be used.</param>
    /// <returns>Returns a new instance of <see cref="LogixContent"/> with metadata populated based on the specified component and revision.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="component"/> parameter is null.</exception>
    public static LogixContent Create<TComponent>(LogixComponent<TComponent> component, Revision? revision = null)
        where TComponent : LogixComponent<TComponent>
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, 1.0));
        content.Add(new XAttribute(L5XName.SoftwareRevision, revision ?? new Revision()));
        content.Add(new XAttribute(L5XName.TargetName, component.Name));
        content.Add(new XAttribute(L5XName.TargetType, component.Serialize().Name.LocalName));
        content.Add(new XAttribute(L5XName.ContainsContext, true));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixContent(content);
    }

    /*/// <summary>
    /// Creates a new instance of the <see cref="LogixInfo"/> class with metadata and contextual information
    /// constructed from the provided <see cref="Rung"/> instance and optional <see cref="Revision"/>.
    /// </summary>
    /// <param name="rung">The rung instance used to derive the contextual metadata for the LogixInfo.</param>
    /// <param name="revision">The optional software revision used to specify the target schema version. If not provided, a default revision is used.</param>
    /// <returns>A new <see cref="LogixInfo"/> instance populated with metadata based on the provided parameters.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rung"/> parameter is null.</exception>
    public static LogixInfo Create(ILogixCode code, Revision? revision = null)
    {
        if (code is null)
            throw new ArgumentNullException(nameof(code));

        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        content.Add(new XAttribute(L5XName.SoftwareRevision, revision ?? new Revision()));
        content.Add(new XAttribute(L5XName.TargetType, LogixSerializer.NamesFor(code.GetType()).First()));
        content.Add(new XAttribute(L5XName.TargetCount, code.Number));
        content.Add(new XAttribute(L5XName.ContainsContext, true));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixInfo(content);
    }*/

    /// <summary>
    /// If no root controller element exists, adds a new context controller and moves all root elements into that controller
    /// element. Then adds missing top level containers to ensure a consistent structure of the root L5X.
    /// </summary>
    private static void NormalizeContent(XElement element)
    {
        //From what I can tell, as long as the controller element is present, we are fine.
        //When we instantiate a controller object, it will ensure all remaining containers are created.
        if (element.Element(L5XName.Controller) is not null) return;

        //Create a new contextual/virtual controller to contain shift the exported content to.
        //The default constructor already handles the initialization we want, so we are leveraging that here.
        var controller = new Controller { Name = "Context" }.Serialize();

        //So far the only outlier is modules. Modules can export without a container ("Modules") element.
        //This is the only special case we will handle here until we learn otherwise.
        if (element.Attribute(L5XName.TargetType)?.Value == nameof(Module))
        {
            var modules = element.Descendants(L5XName.Module);
            controller.Element(L5XName.Modules)?.Add(modules);
        }

        element.ReplaceNodes(controller);
    }
}