namespace LuaBundler.Core;

public class LuaFileRegistryEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LuaFileRegistryEventArgs"/>
    /// class.
    /// </summary>
    /// 
    /// <param name="filename">
    /// The registered item.
    /// </param>
    internal LuaFileRegistryEventArgs(string filename)
    {
        Name = filename;
    }

    public string Name { get; set; }
}

public delegate void LuaFileRegistryEventHandler(object sender, LuaFileRegistryEventArgs e);
