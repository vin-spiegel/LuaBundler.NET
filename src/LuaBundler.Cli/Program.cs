using LuaBundler.Core;

if (args.Length < 2)
    return;

var sourcePath = args[0];
var distPath = args[1];

var bundler = new LuaFileBundler();

bundler.ToFile(sourcePath, distPath);