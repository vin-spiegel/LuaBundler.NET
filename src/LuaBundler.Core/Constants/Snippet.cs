namespace LuaBundler.Core.Constants;

public static class Snippet
{
    public const string RemarkHeader = 
@"-- Bundled Files: {0}
-- Unused Files: {1}
-- Bundled At: {2}";

    public const string CodeHeader = 
        @"local __modules = {}
local require = function(path)
local module = __modules[path]
if module ~= nil then
    if not module.inited then
        module.cached = module.loader()
        module.inited = true
    end
    return module.cached
else
    error('module not found')
    return nil
end
end";
    
    public const string CodeFooter = "\n-- Execute Main Function" +
                                     "\n__modules[\"{0}\"].loader()";
}