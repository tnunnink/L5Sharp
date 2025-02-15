using L5Sharp.Core;
using RockwellAutomation.LogixDesigner;

namespace L5Sharp.Logix;

public static class ACD
{
    /// <summary>
    /// Asynchronously loads the specified file path and returns the contents as a new <see cref="L5X"/> instance.
    /// </summary>
    /// <param name="fileName">A URI string referencing the file to load.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <param name="token">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <remarks>This method can support opening either</remarks>
    public static async Task<L5X> LoadAsync(string fileName,
        L5XOptions options = L5XOptions.None,
        CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(fileName))
            throw new ArgumentException("File name can not be null or empty.", nameof(fileName));

        //Generate file temp path for L5X
        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.L5X");

        //Uses SDK to load project file into memory and save as an L5X.
        using var project = await LogixProject.OpenLogixProjectAsync(fileName, cancellationToken: token);
        await project.SaveAsAsync(path, cancellationToken: token);

        //Load the L5X instance.
        var file = await L5X.LoadAsync(path, options, token);

        //Cleanup temp L5X
        File.Delete(path);

        return file;
    }
}