using System;
using Godot;

namespace GodotCustomTypes.Metadata;

public record TypeMetadata(string TypeName, Type Type, string BaseTypeName, Type BaseType, Script Script, ImageTexture Icon);