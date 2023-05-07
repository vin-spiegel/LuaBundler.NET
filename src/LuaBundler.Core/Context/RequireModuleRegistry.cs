using System.Text.RegularExpressions;
using LuaBundler.Core.Providers;

namespace LuaBundler.Core.Context;

public class RequireModuleRegistry
{
    private readonly Regex _regex = new Regex("require[\\s*]?[\\(]?[\"']([\\s*]?[0-9\\/a-zA-Z_-]+)[\"'][\\)]?");
    private readonly IDictionary<string, bool> _requires = new Dictionary<string, bool>();
    private readonly LuaFileNameProvider _luaFileNameProvider = new LuaFileNameProvider();
    
    /// <summary xml:lang="ko">
    /// require 예약어 걸린 파일 이름 리스트 얻기
    /// </summary>
    internal List<string> GetNewFileNames(string file)
    {
        var matches = _regex.Matches(file);
        
        // 중복 파일일 경우 Emit 하지 않기
        var newNames = new List<string>();
        foreach (Match match in matches)
        {
            var newName = match.Groups[1].ToString();
            if (_requires.ContainsKey(newName))
            {
                _requires[newName] = false;
            }
            else
            {
                newNames.Add(newName);
            }
        }

        return newNames;
    }
    
    /// <summary xml:lang="ko">
    /// 폴더 내에 안쓰는 루아 모듈 얻기
    /// </summary>
    internal List<string> GetUnusedFiles(string? dir)
    {
        var list = new List<string>();
        var names = _luaFileNameProvider.GetAllLuaFileNames(dir);
        foreach(var name in names)
        {
            if (!_requires.ContainsKey(name))
            {
                list.Add(name);
            }
        }
        return list;
    }

    public bool ContainsKey(string name)
    {
        return _requires.ContainsKey(name);
    }

    public bool this[string name]
    {
        get => _requires[name];
        set => _requires[name] = value;
    }

    public int Count => _requires.Count;

    public void Dispose()
    {
        _requires.Clear();
    }
}