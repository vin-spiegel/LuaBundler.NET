using LuaBundler.Core.Model;
using LuaBundler.Core.Utils;

// ReSharper disable ConvertToUsingDeclaration

namespace LuaBundler.Core;

public class LuaFileBundler
{
    private readonly LuaFileGenerator _luaFileGenerator;
    
    /// <summary xml:lang="ko">
    /// 루아 파일이 레지스트리 될때마다 호출되는 이벤트입니다.
    /// </summary>
    public LuaFileRegistryEventHandler? RegistryFile;
    
    /// <summary xml:lang="ko">
    /// 루아 파일 번들링이 끝나면 호출되는 이벤트입니다.
    /// </summary>
    public Action? CompleteGenerateFiles;
    
    public LuaFileBundler()
    {
        _luaFileGenerator = new LuaFileGenerator();
        _luaFileGenerator.RegistryFile += filename =>
        {
            RegistryFile?.Invoke(this, new LuaFileRegistryEventArgs(filename));
        };
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
        CompleteGenerateFiles?.Invoke();
        
        return true;
    }
}