using System;

namespace CustomTypesLoader.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class MuteTypeLoadingAttribute : Attribute
{
}