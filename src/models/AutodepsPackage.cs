using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YamlDotNet.Serialization;

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
    [YamlMember(Alias = "name")]
    [JsonProperty("name")]
    public string Name { get; set; } = "";

    [YamlMember(Alias = "version")]
    [JsonProperty("version")]
    public string Version { get; set; } = "0.0";

    [YamlMember(Alias = "url")]
    [JsonProperty("url")]
    public string Url { get; set; } = "";

    [YamlMember(Alias = "hash")]
    [JsonProperty("hash")]
    public string Hash { get; set; } = "";

    [YamlMember(Alias = "include_paths")]
    [JsonProperty("include_paths")]
    public string[] IncludePaths { get; set; } = ["include"];

    [YamlMember(Alias = "library_paths")]
    [JsonProperty("library_paths")]
    public string[] LibraryPaths { get; set; } = ["lib"];

    [YamlMember(Alias = "library_names")]
    [JsonProperty("library_names")]
    public string[]? LibraryNames { get; set; }

    [YamlMember(Alias = "cmake_variables")]
    [JsonProperty("cmake_variables")]
    public Dictionary<OperatingSystems, Dictionary<string, string>>? CMakeVariables { get; set; }

    [YamlMember(Alias = "patch_files")]
    [JsonProperty("patch_files")]
    public Dictionary<OperatingSystems, List<string>>? PatchFiles { get; set; }
}

public class TemplateAutodepsPackage : AutodepsPackage
{
    public string CleanName => Name.Replace("-", "");

    public string FileName => Url.Split('/').Last();

    public string HashType => Hash.Split(':').First();

    public string HashValue => Hash.Split(':').Last();
}