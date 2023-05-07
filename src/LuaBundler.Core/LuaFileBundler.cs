using LuaBundler.Core.Model;
using LuaBundler.Core.Utils;

// ReSharper disable ConvertToUsingDeclaration

namespace LuaBundler.Core;

public class LuaFileBundler
{
    private readonly LuaFileGenerator _luaFileGenerator;
    
    public LuaFileBundler()
    {
        _luaFileGenerator = new LuaFileGenerator();
    }
    
    /// <summary xml:lang="ko">
    /// 루아 파일들을 하나로 묶어서 번들링 해줍니다.
    /// </summary>
    public bool ToFile(string mainPath, string outPath)
    {
        if (!Utility.CheckFileExist(mainPath))
            return false;

        var luaFile = _luaFileGenerator.GenerateCode(mainPath);
        Utility.CreateFileSync(outPath, luaFile);
        
        _luaFileGenerator.Dispose();

        return true;
    }
}