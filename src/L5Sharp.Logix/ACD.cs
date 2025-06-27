using L5Sharp.Core;
using RockwellAutomation.LogixDesigner;

namespace L5Sharp.Logix;

public static class ACD
{
    /// <summary>
    /// Loads the specified ACD file and returns the contents as a new <see cref="L5X"/> instance.
    /// </summary>
    /// <param name="filePath">The path referencing the ACD file to load.</param>
    /// <param name="options">The <see cref="L5XOptions"/> that control how the L5X is initialized.
    /// The default is none, meaning no extra initialization steps are performed.</param>
    /// <param name="token">A token that can be used to request cancellation of the asynchronous operation.</param>
    /// <returns>A new <see cref="L5X"/> containing the contents of the specified file.</returns>
    /// <remarks>
    /// This method uses the new Logix Designer SDK to load the ACD project and convert it to an L5X file.
    /// Note that this code requires access to the SDK software which is not included in this library so to not
    /// violate Rockwell EULA. Also note that this conversion takes around 30 seconds to complete depending on the
    /// project size.
    /// </remarks>
    public static async Task<L5X> LoadAsync(string filePath,
        L5XOptions options = L5XOptions.None,
        CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("File name can not be null or empty.", nameof(filePath));

        if (!filePath.EndsWith(".ACD"))
            throw new ArgumentException("File must be an ACD file.", nameof(filePath));

        //Generate file temp path for L5X
        var path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.L5X");

        //Uses SDK to load a project file into memory and save as an L5X.
        using var project = await LogixProject.OpenLogixProjectAsync(filePath, cancellationToken: token);
        await project.SaveAsAsync(path, cancellationToken: token);

        //Load the L5X instance.
        var content = await L5X.LoadAsync(path, options, token);

        //Cleanup temp L5X
        File.Delete(path);

        return content;
    }
}