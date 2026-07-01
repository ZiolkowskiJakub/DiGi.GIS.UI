#### [DiGi\.GIS\.UI](index.md 'index')

## DiGi\.GIS\.UI\.Classes Namespace
### Classes

<a name='DiGi.GIS.UI.Classes.GISWindowSettings'></a>

## GISWindowSettings Class

Represents the configuration settings for a GIS window, providing properties for persisting and retrieving window\-specific state such as associated file paths\.

```csharp
public class GISWindowSettings : DiGi.Core.Classes.SerializableObject
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → GISWindowSettings
### Constructors

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.GISWindowSettings()'></a>

## GISWindowSettings\(\) Constructor

Initializes a new instance of the [GISWindowSettings](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.GISWindowSettings 'DiGi\.GIS\.UI\.Classes\.GISWindowSettings') class\.

```csharp
public GISWindowSettings();
```

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.GISWindowSettings(DiGi.GIS.UI.Classes.GISWindowSettings)'></a>

## GISWindowSettings\(GISWindowSettings\) Constructor

Initializes a new instance of the [GISWindowSettings](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.GISWindowSettings 'DiGi\.GIS\.UI\.Classes\.GISWindowSettings') class by copying values from an existing [GISWindowSettings](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.GISWindowSettings 'DiGi\.GIS\.UI\.Classes\.GISWindowSettings') instance\.

```csharp
public GISWindowSettings(DiGi.GIS.UI.Classes.GISWindowSettings? gISWindowSettings);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.GISWindowSettings(DiGi.GIS.UI.Classes.GISWindowSettings).gISWindowSettings'></a>

`gISWindowSettings` [GISWindowSettings](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.GISWindowSettings 'DiGi\.GIS\.UI\.Classes\.GISWindowSettings')

The source settings instance to copy from, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.GISWindowSettings(System.Text.Json.Nodes.JsonObject)'></a>

## GISWindowSettings\(JsonObject\) Constructor

Initializes a new instance of the [GISWindowSettings](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.GISWindowSettings 'DiGi\.GIS\.UI\.Classes\.GISWindowSettings') class from the specified JSON object\.

```csharp
public GISWindowSettings(System.Text.Json.Nodes.JsonObject? jsonObject);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.GISWindowSettings(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the settings data to be deserialized\.
### Properties

<a name='DiGi.GIS.UI.Classes.GISWindowSettings.Paths'></a>

## GISWindowSettings\.Paths Property

Gets or sets the collection of paths associated with the GIS window settings\.

```csharp
public System.Collections.Generic.List<string>? Paths { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions'></a>

## VoTTConversionOptions Class

Provides configuration options for the VoTT conversion process\.

```csharp
public class VoTTConversionOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → VoTTConversionOptions
### Constructors

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions()'></a>

## VoTTConversionOptions\(\) Constructor

Initializes a new instance of the [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions') class\.

```csharp
public VoTTConversionOptions();
```

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(DiGi.GIS.UI.Classes.VoTTConversionOptions)'></a>

## VoTTConversionOptions\(VoTTConversionOptions\) Constructor

Initializes a new instance of the [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions') class using the values from an existing [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions') instance\.

```csharp
public VoTTConversionOptions(DiGi.GIS.UI.Classes.VoTTConversionOptions voTTConversionOptions);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(DiGi.GIS.UI.Classes.VoTTConversionOptions).voTTConversionOptions'></a>

`voTTConversionOptions` [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions')

The source options instance to copy settings from\.

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(double)'></a>

## VoTTConversionOptions\(double\) Constructor

Initializes a new instance of the [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions') class with the specified offset\.

```csharp
public VoTTConversionOptions(double offset);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value to be used for conversion\.

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(System.Text.Json.Nodes.JsonObject)'></a>

## VoTTConversionOptions\(JsonObject\) Constructor

Initializes a new instance of the [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions') class from the specified [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')\.

```csharp
public VoTTConversionOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.VoTTConversionOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the configuration settings for VoTT conversion\.
### Properties

<a name='DiGi.GIS.UI.Classes.VoTTConversionOptions.Offset'></a>

## VoTTConversionOptions\.Offset Property

Gets or sets the offset value used during the VoTT conversion process\.

```csharp
public double Offset { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions'></a>

## YOLOConversionOptions Class

Provides configuration options for the YOLO conversion process, including offset values, category weights, and data clearing preferences\.

```csharp
public class YOLOConversionOptions : DiGi.Core.Classes.SerializableOptions
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [DiGi\.Core\.Classes\.Object](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.object 'DiGi\.Core\.Classes\.Object') → [DiGi\.Core\.Classes\.SerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableobject 'DiGi\.Core\.Classes\.SerializableObject') → [DiGi\.Core\.Classes\.SerializableOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.serializableoptions 'DiGi\.Core\.Classes\.SerializableOptions') → YOLOConversionOptions
### Constructors

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions()'></a>

## YOLOConversionOptions\(\) Constructor

Initializes a new instance of the [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') class\.

```csharp
public YOLOConversionOptions();
```

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(DiGi.GIS.UI.Classes.YOLOConversionOptions)'></a>

## YOLOConversionOptions\(YOLOConversionOptions\) Constructor

Initializes a new instance of the [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') class using the values from an existing [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') instance\.

```csharp
public YOLOConversionOptions(DiGi.GIS.UI.Classes.YOLOConversionOptions YOLOConversionOptions);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(DiGi.GIS.UI.Classes.YOLOConversionOptions).YOLOConversionOptions'></a>

`YOLOConversionOptions` [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions')

The source options instance to copy settings from\.

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(double)'></a>

## YOLOConversionOptions\(double\) Constructor

Initializes a new instance of the [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') class with the specified offset\.

```csharp
public YOLOConversionOptions(double offset);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value to be used for the YOLO conversion process\.

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(System.Text.Json.Nodes.JsonObject)'></a>

## YOLOConversionOptions\(JsonObject\) Constructor

Initializes a new instance of the [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') class using the specified JSON object\.

```csharp
public YOLOConversionOptions(System.Text.Json.Nodes.JsonObject jsonObject);
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.YOLOConversionOptions(System.Text.Json.Nodes.JsonObject).jsonObject'></a>

`jsonObject` [System\.Text\.Json\.Nodes\.JsonObject](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.nodes.jsonobject 'System\.Text\.Json\.Nodes\.JsonObject')

The JSON object containing the configuration data to initialize the options\.
### Properties

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.ClearData'></a>

## YOLOConversionOptions\.ClearData Property

Gets or sets a value indicating whether existing data should be cleared before starting the conversion process\.

```csharp
public bool ClearData { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.ConfigurationFilePath'></a>

## YOLOConversionOptions\.ConfigurationFilePath Property

Gets or sets the file path to the configuration file used for the YOLO conversion process\.

```csharp
public string? ConfigurationFilePath { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.GISModelFilesDirectory'></a>

## YOLOConversionOptions\.GISModelFilesDirectory Property

Gets or sets the directory path where the GIS model files are located\.

```csharp
public string? GISModelFilesDirectory { get; set; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.Offset'></a>

## YOLOConversionOptions\.Offset Property

Gets or sets the offset value applied during the YOLO conversion process\.

```csharp
public double Offset { get; set; }
```

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.this[DiGi.YOLO.Enums.Category]'></a>

## YOLOConversionOptions\.this\[Category\] Property

Gets the weight associated with the specified YOLO category\.
Returns a default value of 1 if no specific weight is defined for the category\.

```csharp
public double this[DiGi.YOLO.Enums.Category category] { get; set; }
```
#### Parameters

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.this[DiGi.YOLO.Enums.Category].category'></a>

`category` [DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')

The YOLO category to retrieve the weight for\.

#### Property Value
[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

<a name='DiGi.GIS.UI.Classes.YOLOConversionOptions.Weights'></a>

## YOLOConversionOptions\.Weights Property

Gets or sets the weights associated with each category used during the conversion process\.

```csharp
public System.Collections.Generic.Dictionary<DiGi.YOLO.Enums.Category,double> Weights { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')