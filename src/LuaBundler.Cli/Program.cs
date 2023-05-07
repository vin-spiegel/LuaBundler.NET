using System.Drawing;
using LuaBundler.Core;
using Console = Colorful.Console;

if (args.Length < 2)
    return;

var sourcePath = args[0];
var distPath = args[1];

var bundler = new LuaFileBundler();
bundler.RegistryFile += (obj, luaFile) =>
{
    
    Console.WriteLine($"Registry success {luaFile.Name}", Color.GreenYellow);
};
bundler.CompleteGenerateFiles += () =>
{
    Console.WriteLine("Bundled lua files Complete", Color.GreenYellow);
};

bundler.ToFile(sourcePath, distPath);