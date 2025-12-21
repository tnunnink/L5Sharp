using CliFx.Attributes;
using CliFx.Infrastructure;
using JetBrains.Annotations;
using L5Sharp.CLI.Internal;
using RockwellAutomation.LogixDesigner;
using RockwellAutomation.LogixDesigner.Logging;
using Spectre.Console;

namespace L5Sharp.CLI.Commands.Projects;

/// <summary>
/// Represents a command for converting an Allen-Bradley ACD project file to L5X format.
/// </summary>
/// <remarks>
/// This command facilitates the conversion of Rockwell Automation ControlLogix project files
/// from the proprietary ACD format into the open L5X XML format. It ensures that the file
/// is properly loaded, converted, and saved to the specified location. The command also
/// validates the input file path for existence and compatibility before processing.
/// </remarks>
[PublicAPI]
[Command("convert", Description = "Converts between ACD and L5X formats using the Logix SDK.")]
public class ConvertCommand : ProjectCommand
{
    #region Arguments

    [CommandOption("output", 'o', Description = "The output file path. Default project path with opposite extension.")]
    public string? Output { get; set; }

    [CommandOption("detailed", Description = "Provides additional info in L5X file conversion.")]
    public bool Detailed { get; set; }

    [CommandOption("force", Description = "Overwrites the output file if it exists.")]
    public bool Force { get; set; }

    #endregion

    /// <inheritdoc />
    protected override bool IsSupported(string filePath) => filePath.IsLogixFile();

    /// <inheritdoc />
    public override async ValueTask ExecuteAsync(IConsole console)
    {
        var cancellation = console.RegisterCancellationHandler();
        var projectPath = ResolveProjectPath(console);
        var savePath = ResolveSavePath(projectPath, Output);

        try
        {
            await console.Ansi().Status()
                .StartAsync($"Converting project file '{Project}'", async ctx =>
                {
                    ctx.Spinner(Spinner.Known.Sand);
                    ctx.SpinnerStyle(Style.Parse("blue"));

                    using var project = await LogixProject.OpenLogixProjectAsync(
                        projectPath,
                        new StdOutEventLogger(),
                        cancellation
                    );

                    await project.SaveAsAsync(savePath, Force, Detailed, cancellation);
                });
        }
        catch (Exception e)
        {
            console.Ansi().WriteException(e);
        }
    }
}