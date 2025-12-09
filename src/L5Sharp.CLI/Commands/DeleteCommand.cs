using CliFx;
using CliFx.Attributes;
using CliFx.Exceptions;
using CliFx.Infrastructure;
using L5Sharp.CLI.Internal;
using L5Sharp.Core;

namespace L5Sharp.CLI.Commands;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class DeleteCommand<TEntity> : ICommand where TEntity : LogixEntity<TEntity>
{
    [CommandOption("name", 'n',
        Description = "The name pattern to match for deletion (supports wildcards '*' and '?')")]
    public string? Name { get; init; }

    [CommandOption("where", Description = "A filter expression to limit which items are deleted.")]
    public string? Where { get; init; }

    [CommandOption("unused", Description = "When true, only delete components that are not being used.")]
    public bool Unused { get; init; }

    /// <inheritdoc />
    public async ValueTask ExecuteAsync(IConsole console)
    {
        try
        {
            var project = await console.ReadXmlAsync();

            project.Query<TEntity>()
                .FilterByPattern(e => e is ILogixComponent c ? c.Name : string.Empty, Name)
                .FilterByExpression(Where)
                .FilterBySwitch(e => e is ILogixComponent c && !c.Usages().Any(), Unused)
                .ToList()
                .ForEach(r => r.Remove());

            console.WriteXml(project);
        }
        catch (Exception e)
        {
            throw new CommandException($"Command Failed: {e.Message}", ExitCodes.InternalError);
        }
    }
}