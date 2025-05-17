using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac.Util;
using GodotCustomTypes.Attributes;

namespace GodotCustomTypes.Reflection;

public class RelatedTypesProvider
{
    private readonly Type[] _types;

    public RelatedTypesProvider(params Type[] types)
    {
        _types = types;
    }
    
    public List<Type> Process(Assembly assembly)
    {
        var result = new List<Type>();
        var allTypes = assembly.GetLoadableTypes();

        foreach (var type in allTypes)
        {
            if (IsTypeToBeSkipped(type))
            {
                continue;
            }
            
            result.Add(type);
        }

        return result;
    }

    private bool IsTypeToBeSkipped(Type type)
        => type.IsAbstract ||
           !IsRelatedType(type) ||
           Attribute.GetCustomAttribute(type, typeof(MuteTypeLoadingAttribute)) != null;

    private bool IsRelatedType(Type type)
    {
        foreach (var baseType in _types)
        {
            if (type.IsSubclassOf(baseType))
            {
                return true;
            }
        }

        return false;
    }
}