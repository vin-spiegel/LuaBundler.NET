# LuaBundler.NET
Simple Lua Bundler in .NET

# Dependency
[.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)

# Install

### Step.1
```
$ git clone https://github.com/vin-spiegel/LuaBundler.NET.git
```

### Step.2
```
$ dotnet pack
```

### Step.3
```
$ dotnet tool install --global --add-source ./nupkg luabundler.cli --version <VERSION_NUMBER>
```

# Usage
Bundle Lua files into one:
```
$ lua-bundler "source path\source file.lua" "dist path\dist file.lua"
```

# Lisence
MIT
