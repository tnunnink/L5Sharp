using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// A class containing information regarding the L5X export file. This information is found on the root
/// RSLogix5000Content element and is used by the Logix software to determine the context of the L5X file.
/// </summary>
public class LogixInfo : LogixElement
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
    public LogixInfo(XElement element) : base(element)
    {
        if (element.Name.LocalName != L5XName.RSLogix5000Content)
            throw new InvalidOperationException("Expecting root content element to have name 'RSLogix5000Content'");

        NormalizeContent(Element);
    }

    /// <summary>
    /// Gets the revision of the schema used within the L5X export file.
    /// </summary>
    public Revision SchemaRevision => GetValue<Revision>() ?? new Revision();

    /// <summary>
    /// Gets the version of the software used to create the L5X export file.
    /// </summary>
    public Revision SoftwareRevision => GetValue<Revision>() ?? new Revision();

    /// <summary>
    /// Gets the name of the Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetName => GetValue<string>();

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public string? TargetType => GetValue<string>();

    /// <summary>
    /// Gets the type of Logix component that is the target of the current L5X context.
    /// </summary>
    public int? TargetCount => GetValue<int>();

    /// <summary>
    /// Gets the value indicating whether the current L5X is contextual.
    /// </summary>
    public bool ContainsContext => GetValue<bool?>() ?? false;

    /// <summary>
    /// Gets the owner that exported the current L5X file.
    /// </summary>
    public string? Owner => GetValue<string>();

    /// <summary>
    /// Gets the date time that the L5X file was exported.
    /// </summary>
    public DateTime ExportDate => GetDateTime();

    /// <summary>
    /// Gets the set of configured export options for the L5X file.
    /// </summary>
    /// <value>A collection of <see cref="string"/> indicating the option values.</value>
    public IEnumerable<string> ExportOptions => GetValues<string>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{TargetType}/{TargetName}/{TargetCount}".Trim('/');
    }

    /// <summary>
    /// Creates an empty <see cref="LogixInfo"/> instance with default attributes and values.
    /// </summary>
    /// <returns>
    /// A new <see cref="LogixInfo"/> instance representing an empty RSLogix 5000 content element constructed with default schema revision,
    /// software revision, and other attributes.
    /// </returns>
    public static LogixInfo Empty()
    {
        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        content.Add(new XAttribute(L5XName.SoftwareRevision, new Revision()));
        content.Add(new XAttribute(L5XName.TargetName, string.Empty));
        content.Add(new XAttribute(L5XName.TargetType, L5XName.Controller));
        content.Add(new XAttribute(L5XName.ContainsContext, false));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixInfo(content);
    }

    /// <summary>
    /// Creates a new <see cref="LogixInfo"/> instance targeting the provided component instance.
    /// </summary>
    /// <param name="component">The <see cref="LogixComponent"/> representing content target of the new info object.</param>
    /// <param name="revision">An optional software <see cref="Revision"/> to be applied. If null, it will default to 1.0.</param>
    /// <returns>A new instance of the <see cref="LogixInfo"/> class with the specified configuration.</returns>
    public static LogixInfo Create(LogixComponent component, Revision? revision = null)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));

        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        content.Add(new XAttribute(L5XName.SoftwareRevision, revision ?? new Revision()));
        content.Add(new XAttribute(L5XName.TargetName, component.Name));
        content.Add(new XAttribute(L5XName.TargetType, component.L5XType));
        content.Add(new XAttribute(L5XName.ContainsContext, component is not Controller));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixInfo(content);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="LogixInfo"/> class with metadata and contextual information
    /// constructed from the provided <see cref="Rung"/> instance and optional <see cref="Revision"/>.
    /// </summary>
    /// <param name="rung">The rung instance used to derive the contextual metadata for the LogixInfo.</param>
    /// <param name="revision">The optional software revision used to specify the target schema version. If not provided, a default revision is used.</param>
    /// <returns>A new <see cref="LogixInfo"/> instance populated with metadata based on the provided parameters.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="rung"/> parameter is null.</exception>
    public static LogixInfo Create(Rung rung, Revision? revision = null)
    {
        if (rung is null)
            throw new ArgumentNullException(nameof(rung));

        var content = new XElement(L5XName.RSLogix5000Content);
        content.Add(new XAttribute(L5XName.SchemaRevision, new Revision()));
        content.Add(new XAttribute(L5XName.SoftwareRevision, revision ?? new Revision()));
        content.Add(new XAttribute(L5XName.TargetType, nameof(Rung)));
        content.Add(new XAttribute(L5XName.TargetCount, rung.Number));
        content.Add(new XAttribute(L5XName.ContainsContext, true));
        content.Add(new XAttribute(L5XName.Owner, Environment.UserName));
        content.Add(new XAttribute(L5XName.ExportDate, DateTime.Now.ToString(DateTimeFormat)));

        return new LogixInfo(content);
    }

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

        //So far the only outlier is modules. Modules can export without even a container element.
        //This is the only special case we will handle here until we learn otherwise.
        if (element.Attribute(L5XName.TargetType)?.Value == nameof(Module))
        {
            var modules = element.Descendants(L5XName.Module);
            controller.Element(L5XName.Modules)?.Add(modules);
        }

        element.ReplaceNodes(controller);
    }
}