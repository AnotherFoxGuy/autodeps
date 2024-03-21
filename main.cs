// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Text;
using autodeps.models;
using autodeps.tools;
using Newtonsoft.Json;
using Scriban;
using Scriban.Parsing;

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

// var temp = EmbededFileLoader.Instance.GetFileContents("templates/conan/conanfile.py.in");
var temp = EmbededFileLoader.Instance.GetFileContents("templates/vcpkg/portfile.cmake.in");
// var temp = EmbededFileLoader.Instance.GetFileContents("templates/cmake_meta/pkg.CMakeLists.txt.in");

var template = Template.Parse(temp);
var result = template.Render(pkg);
Console.WriteLine(result);
