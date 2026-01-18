using System.Collections.Generic;
using Godot;
using GodotCustomTypes.Metadata.Data;
using GodotCustomTypes.Reflection;

namespace GodotCustomTypes.Metadata;

public class TypesLoader : ITypesLoader
{
    private readonly TypesProvider _typesProvider = new();
    private readonly ScriptProvider _scriptProvider = new();
    private readonly IconProvider _iconProvider = new();
    private readonly BaseTypeProvider _baseTypeProvider = new();

    public IList<TypeMetadata> LoadCustomTypes()
    {
        var result = new List<TypeMetadata>();
        var types = _typesProvider.GetCustomTypes();

        foreach (var type in types)
        {
            var script = _scriptProvider.LoadScript(type);
            if (script == null)
            {
                GD.PrintErr($"Unable to load script for type {type.FullName}");
                continue;
            }
            
            var icon = _iconProvider.LoadIcon(type);
            var baseType = _baseTypeProvider.LoadBaseType(type);
            var typeName = $"{Settings.ClassPrefix}{type.Name}";
            var metadata = new TypeMetadata(typeName, type, baseType.Name, baseType, script, icon);
            
            result.Add(metadata);
        }

        return result;
    }
}