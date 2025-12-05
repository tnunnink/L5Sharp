using L5Sharp.Core;

namespace L5Sharp.CLI.Records;

/// <summary>
/// A record structure for the <see cref="Controller"/> component.
/// </summary>
public record ControllerInfo()
{
    public ControllerInfo(Controller controller) : this()
    {
        Name = controller.Name;
        Description = controller.Description;
        ProcessorType = controller.ProcessorType;
        Revision = controller.Revision?.ToString();
        CreatedOn = controller.ProjectCreationDate.ToString("o");
        ModifiedOn = controller.LastModifiedDate.ToString("o");
    }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? ProcessorType { get; init; }
    public string? Revision { get; init; }
    public string? CreatedOn { get; init; }
    public string? ModifiedOn { get; init; }

    /// <summary>
    /// Defines an implicit conversion operator from <see cref="Controller"/> to <see cref="ControllerInfo"/>.
    /// </summary>
    /// <param name="controller">The <see cref="Controller"/> instance to convert.</param>
    /// <returns>A new <see cref="ControllerInfo"/> instance populated with data from the provided <see cref="Controller"/>.</returns>
    public static implicit operator ControllerInfo(Controller controller) => new(controller);
}