using System.Text;

namespace LuaBundler.Core.Utils;

public class Utility
{
    internal static bool IsNewLineAndEmpty(string line)
    {
        var a = Encoding.ASCII.GetBytes(line);
        return a.Length == 1 && a[0] == 13;
    }

    internal static string ReplaceWithPad(string file)
    {
        var result = new StringBuilder();
        var lines = file.Split('\n');

        foreach (var line in lines)
        {
            if (!IsNewLineAndEmpty(line))
                result.Append('\t');

            result.Append(line);
        }
        
        return result.ToString();
    }

    internal static bool CheckFileExist(string path)
    {
        var fi = new FileInfo(path).Exists;
        if (!fi)
            Console.WriteLine($"File not found - {path}");
        return fi;
    }
    
    internal static void CreateFileSync(string outPath, string file)
    {
        using var fs = File.Create(outPath);
        var info = new UTF8Encoding(true).GetBytes(file);
        fs.Write(info,0,info.Length);
    }
}