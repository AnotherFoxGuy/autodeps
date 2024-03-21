using autodeps.models;

namespace autodeps.generators;

interface IGenerator
{
    bool GeneratePkg(string filename, AutodepsPackage pkg);
}