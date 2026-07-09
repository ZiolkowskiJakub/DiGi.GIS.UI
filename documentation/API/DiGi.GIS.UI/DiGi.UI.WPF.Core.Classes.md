#### [DiGi\.GIS\.UI](DiGi.GIS.UI.Overview.md 'DiGi\.GIS\.UI\.Overview')

## DiGi\.UI\.WPF\.Core\.Classes Namespace
### Classes

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs'></a>

## OrtoDataSelectionChangedEventArgs Class

Provides data for the event that is raised when the ortho data selection changes\.

```csharp
public class OrtoDataSelectionChangedEventArgs : System.EventArgs
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.EventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.eventargs 'System\.EventArgs') → OrtoDataSelectionChangedEventArgs
### Constructors

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs.OrtoDataSelectionChangedEventArgs(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_)'></a>

## OrtoDataSelectionChangedEventArgs\(OrtoDatas, Nullable\<short\>\) Constructor

Initializes a new instance of the [OrtoDataSelectionChangedEventArgs](DiGi.UI.WPF.Core.Classes.md#DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs 'DiGi\.UI\.WPF\.Core\.Classes\.OrtoDataSelectionChangedEventArgs') class\.

```csharp
public OrtoDataSelectionChangedEventArgs(DiGi.GIS.Classes.OrtoDatas? ortoDatas, System.Nullable<short> year);
```
#### Parameters

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs.OrtoDataSelectionChangedEventArgs(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_).ortoDatas'></a>

`ortoDatas` [DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

The orthophoto data associated with the selection change\.

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs.OrtoDataSelectionChangedEventArgs(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_).year'></a>

`year` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The year associated with the selected orthophoto data\.
### Properties

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs.OrtoDatas'></a>

## OrtoDataSelectionChangedEventArgs\.OrtoDatas Property

Gets the orthophoto data associated with the selection change event\.

```csharp
public DiGi.GIS.Classes.OrtoDatas? OrtoDatas { get; }
```

#### Property Value
[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

<a name='DiGi.UI.WPF.Core.Classes.OrtoDataSelectionChangedEventArgs.Year'></a>

## OrtoDataSelectionChangedEventArgs\.Year Property

Gets the year associated with the ortho data selection change\.

```csharp
public System.Nullable<short> Year { get; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')