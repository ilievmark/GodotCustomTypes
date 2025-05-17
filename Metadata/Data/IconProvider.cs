using System;
using Godot;

namespace CustomTypesLoader.Metadata.Data;

public class IconProvider
{
    public ImageTexture LoadIcon(Type type)
    {
        var iconAttribute = Attribute.GetCustomAttribute(type, typeof(IconAttribute)) as IconAttribute;

        if (iconAttribute != null && FileAccess.FileExists(iconAttribute.Path))
        {
            var rawIcon = ResourceLoader.Load<Texture2D>(iconAttribute.Path);
            
            if (rawIcon != null)
            {
                var image = rawIcon.GetImage();
                var editor = EditorInterface.Singleton;
                var length = (int)Mathf.Round(16 * editor.GetEditorScale());
                image.Resize(length, length);
                return ImageTexture.CreateFromImage(image);
            }
        }

        return null;
    }
}