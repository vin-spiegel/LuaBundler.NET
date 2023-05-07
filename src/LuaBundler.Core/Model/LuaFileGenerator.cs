using System.Globalization;
using System.Text;
using LuaBundler.Core.Context;
using LuaBundler.Core.Utils;
// ReSharper disable ConvertToUsingDeclaration

namespace LuaBundler.Core.Model;

internal class LuaFileGenerator
{
    private string? _workDir = ".";
    private readonly StringBuilder _distCode = new StringBuilder();
    private readonly RequireModuleRegistry _registry = new RequireModuleRegistry();

    internal Action<string> RegistryFile = null!;
    
    private void RecursiveFiles(string name)
    {
        if (_registry.ContainsKey(name) && _registry[name])
            return;

        if (_workDir == null)
            throw new Exception("work directory not found");
        
        var filePath = Path.Combine(_workDir, name + ".lua");

        if (!Utility.CheckFileExist(filePath))
            return;

        var file = Utility.ReplaceWithPad(File.ReadAllText(filePath));

        _distCode
            .Append("\n----------------\n")
            .Append($"__modules[\"{name}\"] = ").Append("{ initialized = false, cached = false, loader = function(...)")
            .Append($"\n---- START {name}.lua ----\n")
            .Append(file)
            .Append($"\n---- END {name}.lua ----\n")
            .Append("end }");
        
        _registry[name] = true;
        RegistryFile.Invoke(filePath);
        
        // 뎁스 추적하며 require 예약어가 걸린 파일들 생성하기
        foreach (var newName in _registry.GetNewFileNames(file))
        {
            RecursiveFiles(newName);
        }
    }
    
    /// <summary xml:lang="ko">
    /// 전체 코드 생성기
    /// </summary>
    internal string GenerateCode(string mainPath)
    {
        if (!Utility.CheckFileExist(mainPath))
            return string.Empty;
        
        var fi = new FileInfo(mainPath);

        _workDir = fi.DirectoryName;
        
        var mainFunctionName = Path.GetFileNameWithoutExtension(mainPath);

        RecursiveFiles(mainFunctionName);
        
        var unusedFiles = _registry.GetUnusedFiles(_workDir);
        
        foreach (var unusedFile in unusedFiles)
        {
            Console.WriteLine($"Unused File: {unusedFile}");
        }

        // 헤더 생성
        var header = new StringBuilder()
            .Append(string.Format(Constants.Snippet.RemarkHeader + "\n", _registry.Count, unusedFiles.Count, DateTime.Now.ToString(CultureInfo.InvariantCulture)))
            .Append(Constants.Snippet.CodeHeader);
        
        // 헤더 및 푸터 삽입
        _distCode.Insert(0, header);
        _distCode.Append(string.Format(Constants.Snippet.CodeFooter, mainFunctionName));
        
        // 결과물 출력
        return _distCode.ToString();
    }

    internal void Dispose()
    {
        _registry.Dispose();
    }
}