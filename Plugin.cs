using Godot;
using System.Collections.Generic;
using CustomTypesLoader.Attributes;
using CustomTypesLoader.Metadata;

// Originally written by wmigor
// Edited by Atlinx to recursively search for files.
// Edited by bls220 to update for Godot 4.0
// wmigor's Public Repo: https://github.com/wmigor/godot-mono-custom-resource-register
namespace CustomTypesLoader
{
#if TOOLS
    [Tool]
    [MuteTypeLoading]
    public partial class Plugin : EditorPlugin
    {
        private readonly ITypesLoader _typesLoader = new SortedTypesLoaderDecorator(new TypesLoader());
        
        // We're not going to hijack the Mono Build button since it actually takes time to build
        // and we can't be sure how long that is. I guess we have to leave refreshing to the user for now.
        // There isn't any automation we can do to fix that.
        // private Button MonoBuildButton => GetNode<Button>("/root/EditorNode/@@580/@@581/@@589/@@590/Button");
        private readonly List<string> customTypes = new List<string>();
        private Button? refreshButton;

        public override void _EnterTree()
        {
            refreshButton = new Button();
            refreshButton.Text = "CCR";

            AddControlToContainer(CustomControlContainer.Toolbar, refreshButton);
            refreshButton.Icon = EditorInterface.Singleton.GetBaseControl().GetThemeIcon("Reload", "EditorIcons");
            refreshButton.Pressed += OnRefreshPressed;

            Settings.Init();
            RefreshCustomClasses();
            GD.PushWarning("You may change any setting for MonoCustomResourceRegistry in Project -> ProjectSettings -> General -> MonoCustomResourceRegistry");
        }

        public override void _ExitTree()
        {
            UnregisterCustomClasses();
            RemoveControlFromContainer(CustomControlContainer.Toolbar, refreshButton);
            refreshButton?.QueueFree();
        }

        public void RefreshCustomClasses()
        {
            GD.Print("\nRefreshing Registered Resources...");
            UnregisterCustomClasses();
            RegisterCustomClasses();
        }

        private void RegisterCustomClasses()
        {
            customTypes.Clear();

            var types = _typesLoader.LoadCustomTypes();

            foreach (var type in types)
            {
                AddCustomType(type.TypeName, type.BaseTypeName, type.Script, type.Icon);
                customTypes.Add(type.TypeName);
                GD.Print($"Loaded custom type: {type.TypeName} -> {type.Script.GetPath()}");
            }
        }
        
        private void UnregisterCustomClasses()
        {
            foreach (var script in customTypes)
            {
                RemoveCustomType(script);
                GD.Print($"Unregister custom resource: {script}");
            }

            customTypes.Clear();
        }

        private void OnRefreshPressed()
        {
            RefreshCustomClasses();
        }
    }
#endif
}