// ReSharper disable MemberCanBeMadeStatic.Global
namespace LuaBundler.Core.Providers;

internal class LuaFileNameProvider
{
    /// <summary xml:lang="ko">
    /// 디렉토리 내의 모든 루아 파일의 이름을 얻습니다.
    /// </summary>
    internal IEnumerable<string> GetAllLuaFileNames(string? dir)
    {
        var names = new List<string>();
        var files = Directory.GetFiles($@"{dir}", "*.lua", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var name = ReplaceFileName(dir, file);
            names.Add(name);
        }
        return names;
    }

    private string ReplaceFileName(string? dir, string? fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;
        
        return fileName
            .Replace($@"{dir}\", "")
            .Replace(".lua", "")
            .Replace(@"\", "/");
    }
}