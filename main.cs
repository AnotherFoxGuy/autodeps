// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using autodeps.generators;
using autodeps.models;
using Nelibur.ObjectMapper;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

// Console.WriteLine("Hello, World!");


var bpkg = new AutodepsPackage
{
    Name = "fmt",
    Version = "10.2.1",
    Url = "https://github.com/fmtlib/fmt/releases/download/10.2.1/fmt-10.2.1.zip",
    Hash = "sha256:312151a2d13c8327f5c9c586ac6cf7cddc1658e8f53edae0ec56509c8fa516c9",
    CMakeVariables = new()
    {
        { OperatingSystems.any, new Dictionary<string, string> { { "FMT_TEST", "OFF" } } },
        { OperatingSystems.linux, new Dictionary<string, string> { { "LINUX", "ON" } } },
        { OperatingSystems.windows, new Dictionary<string, string> { { "Windows", "ON" } } }
    },
    PatchFiles = new()
    {
        { OperatingSystems.any, ["XD.patch"] },
        { OperatingSystems.linux, ["linux.patch"] } ,
        { OperatingSystems.windows, ["windows.patch"]  }
    }
};

TinyMapper.Bind<AutodepsPackage, TemplateAutodepsPackage>();

var pkg = TinyMapper.Map<TemplateAutodepsPackage>(bpkg);

// var serializer = new SerializerBuilder().Build();
// var yaml = serializer.Serialize(pkg);
// Console.WriteLine(yaml);

var rootCommand = new RootCommand("Generate cpp packages");

var conanGen = new ConanGenerator();
var conanCmd = new Command("conan", "generate conan pkgs");
conanCmd.SetHandler(() => conanGen.GeneratePkg("", pkg));
rootCommand.AddCommand(conanCmd);

var vcpkgGen = new VcpkgGenerator();
var vcpkgCmd = new Command("vcpkg", "generate vcpkg pkgs");
vcpkgCmd.SetHandler(() => vcpkgGen.GeneratePkg("", pkg));
rootCommand.AddCommand(vcpkgCmd);

var cmmGen = new CmakeMetaGenerator();
var cmmCmd = new Command("cmake_meta", "generate cmake_meta pkgs");
cmmCmd.SetHandler(() => cmmGen.GeneratePkg("", pkg));
rootCommand.AddCommand(cmmCmd);

var flatpakGen = new FlatpakGenerator();
var flatpakCmd = new Command("flatpak", "generate flatpak pkgs");
flatpakCmd.SetHandler(() => flatpakGen.GeneratePkg("", pkg));
rootCommand.AddCommand(flatpakCmd);

return await rootCommand.InvokeAsync(args);