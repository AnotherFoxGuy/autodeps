

using autodeps.models;
using autodeps.tools;
using Scriban;

namespace autodeps.generators;

class CmakeMetaGenerator : IGenerator
{
    public bool GeneratePkg(string filename, TemplateAutodepsPackage pkg)
    {
        var temp = EmbededFileLoader.Instance.GetFileContents("templates/cmake_meta/pkg.CMakeLists.txt.in");

        var template = Template.Parse(temp);
        var result = template.Render(pkg);
        Console.WriteLine(result);

        return true;
    }
}