
using autodeps.models;
using autodeps.tools;
using Scriban;

namespace autodeps.generators;

class FlatpakGenerator : IGenerator
{
    public bool GeneratePkg(string filename, AutodepsPackage pkg)
    {
        var temp = EmbededFileLoader.Instance.GetFileContents("templates/flatpak/pkg.yml.in");

        var template = Template.Parse(temp);
        var result = template.Render(pkg);
        Console.WriteLine(result);

        return true;
    }
}