# GodotCustomTypes
Registers all custom classes in Godot project (Godot 4.X)
(Inspired from https://github.com/Atlinx/Godot-Mono-CustomResourceRegistry)

## Instalation
- Download the repo via .zip file
- Create a folder addons/GodotCustomTypes in root folder of your godot project
- Unzip containings into addons/GodotCustomTypes
- Build the project
- Enable the plugin in project settings
- Press CCR button on top right corner - it will refresh registrations

## Usage
It will load all types derived from Node and Resource automatically
After you build projset is built, CCR button on top right corner

### Excluding types from registration
Use [MuteTypeLoading] attribute to mute your custom class

### Using icons
Use [Icon] attribute (it comes from Godot) to provide path to your icon.
It should be full path defined in Godot resources namespace (ex. [Icon("res://addons/Beehave/Icons/blackboard.svg")])

## Notes

### Limitation of base types

Unfortunatelly, Godot does not support custom base classes. So when you create your own hierarchy of classes, they will be flatened like they would be derived from nearest Godot type:

<img width="624" alt="image" src="https://github.com/user-attachments/assets/b4e80b9f-78d2-4508-af2c-3253fbd1dde5" />

That will appear in node explorer as:

<img width="688" alt="image" src="https://github.com/user-attachments/assets/08fdb5d3-3a9b-4700-b76f-d4de87197d19" />

### Limitation of class per script

You can declare only one class derived from node/resource per file. Name of the class should match name of its file.
Otherwise, the type will be invisible in type menu.

