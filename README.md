# VoidLib2

Modding library containing various utilites for modding Miside Zero.

### Features:

- gltf importer (through vgltf)
- audio importer (through naudio/bass)
- Game settings helper
- Dynamic menu editor (wip)
- Other utilities I find useful

# Documentation

Presented by class name. 
> [!WARNING]
> Anything not documented in here may be subject to change!
> Do not rely on VoidLib2's undocumented apis!

## AudioImporter

It contains two nested classes: `NAudio` and `Bass`. Use the one you want based
on which dependencies are present. Bass is faster though so prefer it.

### NAudio

```csharp
VoidLib2.AudioImporter.NAudio.Load(string filePath, out int HResult)
```

Returns: `UnityEngine.AudioClip?`

- Throws if file not found
- Tries to load and if it's unsucessful returns null and you can inspect the HResult.
  
  > this is a dumb system and will change in the future
  

You will need `NAudio.dll`, `NAudio.Core.dll`,
`NAudio.Wasapi.dll`, and `NAudio.WinMN.dll` present in UserLibs in order
to load audio using NAudio.

### Bass

```csharp
VoidLib2.AudioImporter.Bass.LoadAudio(string filePath, out int code)
```

Returns: `UnityEngine.AudioClip?`

- Throws if file is not found
- If a bass stream error occurs, returns null
  - The output parameter `code` will be set to the bass error code so you can inspect it
  - On a successful import, `code` is 0

## DynamicMenuEditor

```csharp
public static bool AddMenuButton(
    float posY, 
    ButtonType buttonType, 
    string buttonText, 
    string name, 
    out GameObject? resultObject)
{
```

Adds a new menu button to the dynamic menu (_not_ the main menu)

#### Parameters:

- `float posY`: The Y position (in the menu's plane) of the button to be created.
- `DynamicMenuEditor.ButtonType` buttonType: Which kind of button will be created.
- `string buttonText`: The text the button will display.
- `string name`: The name of the GameObject (button) that will be created.
- `out GameObject? resultObject`: The resulting GameObject.
  Returns: Whether the operation was successful. So:
- If you call it on any other scene besides Version 1.9 POST or Sample Level Setup
- If it fails to find the Menu gameobject
  This will probably throw an InvalidOperationException if you call it on the wrong scene soon.

`AddMenuGroup()`, `AddMenuSlider()`, and `AddMenuCheckbox()` are planned.

## ExitDoor

```csharp
public static bool SetCollison(bool state)
```

Sets whether the BoxColliders on the ExitDoor and it's related interactables should be enabled.

Returns false if it is unable to find one or more of these GameObjects:

- `"World/House/Doors/ExitFrame/ExitDoor"`
- `"World/Game/Acts/Hello Mita/Interactables 1/I Exit Door 1 "`
  (that space at the end isnt a typo thats actually what its called)
- `"World/Game/Acts/Quality Time/Interactables 2/I ExitDoor 2"`

```csharp
public static bool SetEnabled(bool state)
```

Does **NOT** disable the GameObject, since that can cause issues. Sets whether
the collision should be enabled **and** if it's MeshRenderers are enabled, disabling it for
all practical purposes.

## Extensions

> [!WARNING]
> This class is going to be moved to a new namespace soon

```csharp
public static GameObject FindFirstChild(this GameObject g, string name)
```

Finds the first child of the given GameObject with a matching name.

## GltfImporter

```csharp
public static GameObject LoadGlb(string path, string? name = null)
```

Loads a .glb file and builds a Unity GameObject hierarchy.

1. Checks file exists (otherwise throws `FileNotFoundException`)
2. Parses GLB via `VGltf.GltfContainer`
3. Loads the glb into the game, preserving the structure of the different parts of the glb.

Gltf will be supported soon since it's only a few code changes

---

**Helper methods it uses**

```csharp
public static void BuildNode(GltfContainer container, int nodeIndex, Transform parent)
```

Builds a GLTF node into a Unity GameObject.

---

```csharp
public static (UnityEngine.Mesh, int?) BuildMesh(GltfContainer container, int meshIndex)
```

Converts GLTF mesh into Unity Mesh.

Returns: `(Mesh, materialIndex?)`

---

```csharp
public static Texture2D? LoadTexture(GltfContainer container, int materialIndex)
```

Extracts base color texture from material.

---

```csharp
public static Vector3[] ReadVec3Array(GltfContainer container, int accessorIndex)
```

Reads float3 vertex data from buffer. Flips X axis for Unity space

---

```csharp
public static Vector2[] ReadVec2Array(GltfContainer container, int accessorIndex)\
```

Reads UV data. Flips V coordinate to match Unity

---

```csharp
public static int[] ReadIntArray(GltfContainer container, int accessorIndex)
```

Reads triangle indices.

---

## Music
Contains one method:
```csharp
public static bool SetMusicMuted(bool muted)
```
Tries to find this GameObject:
<br>
`"World/Ambience/Ambient Music"`

And set it's `AudioSource` `mute` property to `muted`.

Useful if you want to mute the music for a bit without touching settings.

## NativeBass
P/Invoke methods for using bass.dll. 
Check the [bass documentation](https://www.un4seen.com/doc/#bass/bass.html) for how to use it

## Settings

A wrapper class for getting and setting game settings easily. Completely reimplements what SettingsManager does.

### MasterVolume

Gets or sets the global audio volume (0–1).  
Updates the audio mixer master channel.

---

### MusicVolume

Gets or sets the music volume (0–1).  
Applies gain to the Music mixer group.

---

### SFXVolume

Gets or sets the sound effects volume (0–1).  
Applies gain to the SFX mixer group.

---

### VocalVolume

Gets or sets the voice/dialogue volume (0–1).  
Applies gain to the Vocals mixer group.

---

### AmbienceVolume

Gets or sets the ambient audio volume (0–1).  
Applies gain to the Ambience mixer group.

---

### Fullscreen

Gets or sets whether fullscreen mode is enabled.  
Applies directly to `Screen.fullScreen`.

---

### VSync

Gets or sets whether vertical sync is enabled.  
Updates `QualitySettings.vSyncCount`.

---

### FOV

Gets or sets the camera field of view (degrees).  
Also applies to the active player camera if available.

---

### Sensitivity

Gets or sets mouse look sensitivity.  
Also updates the player look controller if present.

---

### Brightness

Gets or sets screen brightness.  
Applied through post-processing color grading.

> ⚠ Uses reflection to access a private `colorGrading` field inside `SettingsManager`.

---

### Quality

Gets or sets the Unity quality level index.  
Applies via `QualitySettings.SetQualityLevel`.

---

### AntiAliasing

Gets or sets anti-aliasing level (0–3 mapped to 0/2/4/8 samples).  
Applied via `QualitySettings.antiAliasing`.

#### Why use this?

Although all these settings are technically accessible through `Void.settings.Instance.ApplyAllSettings()`,
VoidLib2's wrapper is better, faster, more explicit, and localized. `ApplyAllSettings()` through the 
SettingsManager instance applies EVERY setting even when you want to change just one.
For example, changing FOV would trigger graphics updates. 

There is no downside of using this except maybe the brightness setting since it uses reflection.

## ShaderUtils
Some cool methods having to do with shaders.

```csharp
public static void SetShaderRecursive(GameObject root, Shader shader)
```

Does exactly what you think it does. Sets all the shaders of every `Renderer` whose 
GameObject is a child of `root`, to whatever you want.

```csharp
public static void SetColorRecursive(GameObject root, Color color)
```

Just for fun! Exactly like the previous method, but sets the material color to whatever you want instead. 
<br>
Used it to create the "Miside Zero but it's the Epstien Files" video
