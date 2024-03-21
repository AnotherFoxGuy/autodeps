using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace autodeps.models;

enum HashTypes
{
    MD5,
    SHA1,
    SHA256
}

public enum OperatingSystems
{
    any,
    linux,
    windows,
    macos
}

public class AutodepsPackage
{
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [JsonProperty("version")]
    public string Version { get; set; } = "0.0";

    [JsonProperty("url")]
    public string Url { get; set; } = "";

    [JsonProperty("hash")]
    public string Hash { get; set; } = "";

    [JsonProperty("include_paths")]
    public string[] IncludePaths { get; set; } = ["include"];

    [JsonProperty("library_paths")]
    public string[] LibraryPaths { get; set; } = ["lib"];

    [JsonProperty("library_names")]
    public string[]? LibraryNames { get; set; }

    [JsonProperty("cmake_variables")]
    public Dictionary<OperatingSystems, Dictionary<string, string>>? CMakeVariables { get; set; }

    [JsonIgnore]
    public string CleanName => Name.Replace("-", "");
    [JsonIgnore]
    public string FileName => Url.Split('/').Last();
}
