using System.Collections.Generic;

namespace GodotCustomTypes.Metadata;

public interface ITypesLoader
{
    IList<TypeMetadata> LoadCustomTypes();
}