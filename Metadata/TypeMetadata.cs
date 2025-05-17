using System;
using Godot;

namespace CustomTypesLoader.Metadata;

public record TypeMetadata(string TypeName, Type Type, string BaseTypeName, Type BaseType, Script Script, ImageTexture Icon);