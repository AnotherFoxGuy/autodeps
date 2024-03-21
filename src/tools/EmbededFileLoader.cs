using System.Reflection;
using System.Text;

namespace autodeps.tools;

class EmbededFileLoader
{
    readonly string _assemblyName;

    private EmbededFileLoader()
    {
        _assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;
    }


    public string? GetFileContents(string filename)
    {
        var path = filename.Replace('/', '.');
        using var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream($"{_assemblyName}.{path}");

        if (stream == null)
            return null;

        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        return streamReader.ReadToEnd();
    }


    #region Singleton

    private static readonly Lazy<EmbededFileLoader> Lazy = new(() => new EmbededFileLoader());
    public static EmbededFileLoader Instance => Lazy.Value;

    #endregion
}