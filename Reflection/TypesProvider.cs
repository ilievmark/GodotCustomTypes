using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace CustomTypesLoader.Reflection;

public class TypesProvider
{
    private readonly RelatedTypesProvider _typesProvider;
    private readonly AssembliesProvider _assembliesProvider;

    public TypesProvider()
    {
        _assembliesProvider = new AssembliesProvider();
        _typesProvider = new RelatedTypesProvider(
            typeof(Node),
            typeof(Resource)
        );
    }

    public IEnumerable<Type> GetCustomTypes()
        => _assembliesProvider
            .GetAssemblies()
            .Select(assembly => _typesProvider.Process(assembly))
            .SelectMany(assembly => assembly);
}