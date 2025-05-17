using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GodotCustomTypes.Reflection;

public class AssembliesProvider
{
    public IEnumerable<Assembly> GetAssemblies()
        => AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(a =>
            {
                var references = a.GetReferencedAssemblies();
                return references.Any(r => r.Name == "GodotSharpEditor") &&
                       references.Any(r => r.Name == "GodotSharp");
            })
            .Where(a => !a.FullName.Contains("GodotTools"));
}