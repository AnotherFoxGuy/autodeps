
using autodeps.models;
using autodeps.tools;
using Scriban;

namespace autodeps.generators;

class ConanGenerator : IGenerator
{
    public bool GeneratePkg(string filename, AutodepsPackage pkg)
    {
        var temp = EmbededFileLoader.Instance.GetFileContents("templates/conan/conanfile.py.in");

        var template = Template.Parse(temp);
        var result = template.Render(pkg);
        Console.WriteLine(result);

        return true;
    }
}