using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace GodotCustomTypes.Reflection;

public class TypesProvider
{
    private readonly RelatedTypesProvider _typesProvider = new(
        typeof(Node),
        typeof(Resource)
    );
    private readonly AssembliesProvider _assembliesProvider = new();

    public IEnumerable<Type> GetCustomTypes()
        => _assembliesProvider
            .GetAssemblies()
            .Select(assembly => _typesProvider.Process(assembly))
            .SelectMany(assembly => assembly);
}