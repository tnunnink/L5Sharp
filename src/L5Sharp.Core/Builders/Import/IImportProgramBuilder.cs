namespace L5Sharp.Core;

/// <summary>
/// Defines functionality for building and customizing the import configuration specific to a program within a Logix project.
/// Provides methods to configure program-level settings such as scheduling within a task or defining parent programs.
/// </summary>
public interface IImportProgramBuilder : IImportConfigBuilder<IImportProgramBuilder>
{
    /// <summary>
    /// Configures the program to be scheduled within the specified task in a Logix project.
    /// </summary>
    /// <param name="taskName">The name of the task to schedule the program within.</param>
    /// <returns>An instance of the <see cref="IImportProgramBuilder"/> for further configuration.</returns>
    IImportProgramBuilder ScheduleIn(string taskName);

    /// <summary>
    /// Specifies the parent program for the current program within a Logix project.
    /// </summary>
    /// <param name="programName">The name of the parent program to associate with the current program.</param>
    /// <returns>An instance of the <see cref="IImportProgramBuilder"/> for further configuration.</returns>
    IImportProgramBuilder WithParent(string programName);
}