using System;
using System.Text;
using Godot;

namespace CustomTypesLoader.Metadata.Data;

public class ScriptProvider
{
    public Script LoadScript(Type type)
    {
        var path = FindClassPath(type);
                
        if (path == null && !FileAccess.FileExists(path))
            return null;

        return GD.Load<Script>(path);
    }

    private string FindClassPath(Type type)
    {
        switch (Settings.SearchType)
        {
            case Settings.ResourceSearchType.Recursive:
                return FindClassPathRecursive(type);
            case Settings.ResourceSearchType.Namespace:
                return FindClassPathNamespace(type);
            default:
                throw new Exception($"ResourceSearchType {Settings.SearchType} not implemented!");
        }
    }
    
    private string FindClassPathNamespace(Type type)
    {
        foreach (string dir in Settings.ResourceScriptDirectories)
        {
            StringBuilder builder = new(dir);
            if (!dir.EndsWith('/'))
            {
                builder.Append('/');
            }
            if (type.Namespace is not null)
            {
                builder
                    .Append(type.Namespace.Replace(".", "/"))
                    .Append('/');
            }
            builder
                .Append(type.Name)
                .Append(".cs");
            string filePath = builder.ToString();
            if (FileAccess.FileExists(filePath))
                return filePath;
        }
        return null;
    }

    private string FindClassPathRecursive(Type type)
    {
        foreach (string directory in Settings.ResourceScriptDirectories)
        {
            string? fileFound = FindClassPathRecursiveHelper(type, directory);
            if (fileFound != null)
                return fileFound;
        }
        return null;
    }
    
    private string FindClassPathRecursiveHelper(Type type, string directory)
    {
        var dir = DirAccess.Open(directory);

        if (DirAccess.GetOpenError() == Error.Ok)
        {
            dir.ListDirBegin();

            while (true)
            {
                var fileOrDirName = dir.GetNext();

                // Skips hidden files like .
                if (fileOrDirName == "")
                    break;
                else if (fileOrDirName.StartsWith("."))
                    continue;
                else if (dir.CurrentIsDir())
                {
                    string? foundFilePath = FindClassPathRecursiveHelper(type, dir.GetCurrentDir() + "/" + fileOrDirName);
                    if (foundFilePath != null)
                    {
                        dir.ListDirEnd();
                        return foundFilePath;
                    }
                }
                else if (fileOrDirName == $"{type.Name}.cs")
                    return dir.GetCurrentDir() + "/" + fileOrDirName;
            }
        }
        return null;
    }
}