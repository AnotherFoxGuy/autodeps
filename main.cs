// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using autodeps.generators;
using autodeps.models;

// Console.WriteLine("Hello, World!");


var pkg = new AutodepsPackage
{
    Name = "fmt",
    Version = "10.2.1",
    Url = "https://github.com/fmtlib/fmt/releases/download/10.2.1/fmt-10.2.1.zip",
    Hash = "sha256:312151a2d13c8327f5c9c586ac6cf7cddc1658e8f53edae0ec56509c8fa516c9",
    CMakeVariables = new(){
         { OperatingSystems.any, new Dictionary<string, string> { {"FMT_TEST" , "OFF"} } },
         { OperatingSystems.linux, new Dictionary<string, string> { {"LINUX" , "ON"} } },
         { OperatingSystems.windows, new Dictionary<string, string> { {"Windows" , "ON"} } }
    }
};

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

return await rootCommand.InvokeAsync(args);