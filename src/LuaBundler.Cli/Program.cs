using LuaBundler.Core;

if (args.Length < 2)
    return;

var sourcePath = args[0];
var distPath = args[1];

var bundler = new LuaFileBundler();
bundler.RegistryFile += (obj, luaFile) =>
{
    Console.WriteLine($"Registry success {luaFile.Name}");
};
bundler.CompleteGenerateFiles += () =>
{
    Console.WriteLine("Bundled lua files Complete");
};

bundler.ToFile(sourcePath, distPath);