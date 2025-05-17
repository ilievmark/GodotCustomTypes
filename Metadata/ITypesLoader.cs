using System.Collections.Generic;

namespace CustomTypesLoader.Metadata;

public interface ITypesLoader
{
    IList<TypeMetadata> LoadCustomTypes();
}