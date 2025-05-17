using System.Collections.Generic;

namespace GodotCustomTypes.Metadata;

public class SortedTypesLoaderDecorator : ITypesLoader
{
    private readonly ITypesLoader _loader;

    public SortedTypesLoaderDecorator(ITypesLoader loader)
    {
        _loader = loader;
    }

    public IList<TypeMetadata> LoadCustomTypes()
    {
        var baseTypesRegistryLookup = new HashSet<string>();
        var queue = new Queue<TypeMetadata>();
        var sorted = new List<TypeMetadata>();
        var typeMetadatas = _loader.LoadCustomTypes();

        foreach (var metadata in typeMetadatas)
        {
            var btAssembly = metadata.BaseType.Assembly;

            if (btAssembly.FullName.Contains("GodotSharp") ||
                btAssembly.FullName.Contains("GodotSharpEditor") ||
                btAssembly.FullName.Contains("GodotTools"))
            {
                baseTypesRegistryLookup.Add(metadata.BaseTypeName);
            }

            if (baseTypesRegistryLookup.Contains(metadata.BaseTypeName))
            {
                baseTypesRegistryLookup.Add(metadata.TypeName);
                sorted.Add(metadata);
                continue;
            }
            
            queue.Enqueue(metadata);
        }

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            
            if (baseTypesRegistryLookup.Contains(next.BaseTypeName))
            {
                sorted.Add(next);
                continue;
            }
            
            queue.Enqueue(next);
        }

        return sorted;
    }
}