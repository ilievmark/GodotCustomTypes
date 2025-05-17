using System;

namespace CustomTypesLoader.Metadata.Data;

public class BaseTypeProvider
{
    public Type LoadBaseType(Type type)
        => Settings.BaseTypeSearching switch
        {
            Settings.BaseTypeSearchingType.ClosestDefaultType => GetClosestGodotBaseType(type),
            Settings.BaseTypeSearchingType.ConsiderCustomTypes => GetClosestCustomBaseType(type),
            _ => throw new NotSupportedException(Settings.BaseTypeSearching.ToString())
        };

    private Type GetClosestGodotBaseType(Type type)
    {
        var baseType = type.BaseType;
        
        while (!IsGodotType(baseType))
        {
            baseType = baseType.BaseType;
        }

        return baseType;
    }

    private bool IsGodotType(Type type)
        => type.Assembly.FullName.Contains("GodotSharp") ||
           type.Assembly.FullName.Contains("GodotSharpEditor") ||
           type.Assembly.FullName.Contains("GodotTools");

    private Type GetClosestCustomBaseType(Type type)
    {
        var baseType = type.BaseType;
        
        while (baseType.IsAbstract)
        {
            baseType = baseType.BaseType;
        }

        return baseType;
    }
}