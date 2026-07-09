#### [DiGi\.GIS\.UI](DiGi.GIS.UI.Overview.md 'DiGi\.GIS\.UI\.Overview')

## DiGi\.GIS\.UI Namespace
### Classes

<a name='DiGi.GIS.UI.Convert'></a>

## Convert Class

```csharp
public static class Convert
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Convert
### Methods

<a name='DiGi.GIS.UI.Convert.ToDiGi_GISModelFiles(System.Windows.Window)'></a>

## Convert\.ToDiGi\_GISModelFiles\(Window\) Method

Opens a file dialog to select and process DiGi GIS model files from a ZIP archive\.

```csharp
public static void ToDiGi_GISModelFiles(System.Windows.Window owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Convert.ToDiGi_GISModelFiles(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window that will host the open file dialog\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.ComparisonTableConversionOptions)'></a>

## Convert\.ToDiGi\_Table\(this GISModelFile, IEnumerable\<string\>, ComparisonTableConversionOptions\) Method

Converts a [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile') to a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')\.

```csharp
public static DiGi.Core.IO.Table.Classes.Table? ToDiGi_Table(this DiGi.GIS.Classes.GISModelFile? gISModelFile, System.Collections.Generic.IEnumerable<string>? references=null, DiGi.GIS.Classes.ComparisonTableConversionOptions? comparisonTableConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.ComparisonTableConversionOptions).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file to convert\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.ComparisonTableConversionOptions).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of references used during the conversion process\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.ComparisonTableConversionOptions).comparisonTableConversionOptions'></a>

`comparisonTableConversionOptions` [DiGi\.GIS\.Classes\.ComparisonTableConversionOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.comparisontableconversionoptions 'DiGi\.GIS\.Classes\.ComparisonTableConversionOptions')

Optional options for the conversion of comparison tables\.

#### Returns
[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')  
A [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') instance if the conversion is successful; otherwise, `null`\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.PredictionTableConversionOptions)'></a>

## Convert\.ToDiGi\_Table\(this GISModelFile, IEnumerable\<string\>, PredictionTableConversionOptions\) Method

Converts a [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile') to a [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')\.

```csharp
public static DiGi.Core.IO.Table.Classes.Table? ToDiGi_Table(this DiGi.GIS.Classes.GISModelFile? gISModelFile, System.Collections.Generic.IEnumerable<string>? references=null, DiGi.GIS.Classes.PredictionTableConversionOptions? predictionTableConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.PredictionTableConversionOptions).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file to convert\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.PredictionTableConversionOptions).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of references used during the conversion process\.

<a name='DiGi.GIS.UI.Convert.ToDiGi_Table(thisDiGi.GIS.Classes.GISModelFile,System.Collections.Generic.IEnumerable_string_,DiGi.GIS.Classes.PredictionTableConversionOptions).predictionTableConversionOptions'></a>

`predictionTableConversionOptions` [DiGi\.GIS\.Classes\.PredictionTableConversionOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.predictiontableconversionoptions 'DiGi\.GIS\.Classes\.PredictionTableConversionOptions')

Optional options for configuring the conversion of prediction tables\.

#### Returns
[DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table')  
A [DiGi\.Core\.IO\.Table\.Classes\.Table](https://learn.microsoft.com/en-us/dotnet/api/digi.core.io.table.classes.table 'DiGi\.Core\.IO\.Table\.Classes\.Table') instance if the conversion is successful; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create'></a>

## Create Class

```csharp
public static class Create
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Create
### Methods

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double)'></a>

## Create\.BitmapImage\(this Building2D, int, int, double\) Method

Creates a bitmap image representation of the specified 2D building based on the provided dimensions and offset\.

```csharp
public static System.Windows.Media.Imaging.BitmapImage? BitmapImage(this DiGi.GIS.Classes.Building2D? building2D, int width, int height, double offset=0.1);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building entity to convert into a bitmap image\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double).width'></a>

`width` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The width of the resulting bitmap image\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double).height'></a>

`height` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The height of the resulting bitmap image\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset used when fitting the building's polygonal face to the bounding box\.

#### Returns
[System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage')  
A [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') representing the building, or `null` if the [building2D](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,int,int,double).building2D 'DiGi\.GIS\.UI\.Create\.BitmapImage\(this DiGi\.GIS\.Classes\.Building2D, int, int, double\)\.building2D') is null or if invalid dimensions are provided\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,string,int)'></a>

## Create\.BitmapImage\(this Building2D, string, int\) Method

Creates a [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') for the specified 2D building based on the provided directory and year\.

```csharp
public static System.Windows.Media.Imaging.BitmapImage? BitmapImage(this DiGi.GIS.Classes.Building2D? building2D, string? directory, int year);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,string,int).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building entity to create the image for\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,string,int).directory'></a>

`directory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The directory path where the ortho data is located\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.Building2D,string,int).year'></a>

`year` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The specific year of the imagery to retrieve\.

#### Returns
[System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage')  
A [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') if the image could be created; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.OrtoData)'></a>

## Create\.BitmapImage\(this OrtoData\) Method

Creates a [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') from the specified orthophoto data\.

```csharp
public static System.Windows.Media.Imaging.BitmapImage? BitmapImage(this DiGi.GIS.Classes.OrtoData? ortoData);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.OrtoData).ortoData'></a>

`ortoData` [DiGi\.GIS\.Classes\.OrtoData](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodata 'DiGi\.GIS\.Classes\.OrtoData')

The orthophoto data to convert into a bitmap image\.

#### Returns
[System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage')  
A [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') if [ortoData](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.BitmapImage(thisDiGi.GIS.Classes.OrtoData).ortoData 'DiGi\.GIS\.UI\.Create\.BitmapImage\(this DiGi\.GIS\.Classes\.OrtoData\)\.ortoData') is not null; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.BuildingModels(thisSystem.Windows.Window)'></a>

## Create\.BuildingModels\(this Window\) Method

Prompts the user to select a folder containing GIS model files and loads the building models from the specified directory\.

```csharp
public static System.Collections.Generic.Dictionary<string,System.Collections.Generic.List<DiGi.Analytical.Building.Classes.BuildingModel>>? BuildingModels(this System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.BuildingModels(thisSystem.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used to display the folder selection dialog\.

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A dictionary mapping identifiers to lists of [DiGi\.Analytical\.Building\.Classes\.BuildingModel](https://learn.microsoft.com/en-us/dotnet/api/digi.analytical.building.classes.buildingmodel 'DiGi\.Analytical\.Building\.Classes\.BuildingModel') objects if a folder was selected; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_)'></a>

## Create\.ItemPath\(this Typology, TypologyPath, string, Func\<Typology,string\>\) Method

Creates an [ItemPath\(this Typology, TypologyPath, string, Func&lt;Typology,string&gt;\)](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_) 'DiGi\.GIS\.UI\.Create\.ItemPath\(this DiGi\.Typology\.Classes\.Typology, DiGi\.Typology\.Classes\.TypologyPath, string, System\.Func\<DiGi\.Typology\.Classes\.Typology,string\>\)') based on the provided typology and typology path\.

```csharp
public static DiGi.UI.WPF.Classes.ItemPath? ItemPath(this DiGi.Typology.Classes.Typology? typology, DiGi.Typology.Classes.TypologyPath? typologyPath, out string? name, System.Func<DiGi.Typology.Classes.Typology?,string>? func=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_).typology'></a>

`typology` [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')

The typology instance to search within\.

<a name='DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_).typologyPath'></a>

`typologyPath` [DiGi\.Typology\.Classes\.TypologyPath](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typologypath 'DiGi\.Typology\.Classes\.TypologyPath')

The hierarchical path used to locate the item within the typology\.

<a name='DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_).name'></a>

`name` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

When this method returns, contains the name of the item found at the specified path; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_).func'></a>

`func` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')

An optional function used to resolve a string value from the typology object\.

#### Returns
[DiGi\.UI\.WPF\.Classes\.ItemPath](https://learn.microsoft.com/en-us/dotnet/api/digi.ui.wpf.classes.itempath 'DiGi\.UI\.WPF\.Classes\.ItemPath')  
An [ItemPath\(this Typology, TypologyPath, string, Func&lt;Typology,string&gt;\)](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.ItemPath(thisDiGi.Typology.Classes.Typology,DiGi.Typology.Classes.TypologyPath,string,System.Func_DiGi.Typology.Classes.Typology,string_) 'DiGi\.GIS\.UI\.Create\.ItemPath\(this DiGi\.Typology\.Classes\.Typology, DiGi\.Typology\.Classes\.TypologyPath, string, System\.Func\<DiGi\.Typology\.Classes\.Typology,string\>\)') if the item is successfully located; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModel,string,DiGi.GIS.Classes.Building2D)'></a>

## Create\.OrtoDataControls\(this GISModel, string, Building2D\) Method

Creates or retrieves a list of orthodata controls associated with the specified GIS model and 2D building entity based on the provided file path\.

```csharp
public static System.Collections.Generic.List<DiGi.GIS.UI.Controls.OrtoDataControl>? OrtoDataControls(this DiGi.GIS.Classes.GISModel? gISModel, string? path_GISModel, DiGi.GIS.Classes.Building2D? building2D);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModel,string,DiGi.GIS.Classes.Building2D).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model that manages unique objects such as Building2D and AdministrativeAreal2D\.

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModel,string,DiGi.GIS.Classes.Building2D).path_GISModel'></a>

`path_GISModel` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The file path to the GIS model source\.

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModel,string,DiGi.GIS.Classes.Building2D).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building entity for which orthodata controls are being generated\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[OrtoDataControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDataControl 'DiGi\.GIS\.UI\.Controls\.OrtoDataControl')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [OrtoDataControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDataControl 'DiGi\.GIS\.UI\.Controls\.OrtoDataControl') objects if all parameters are valid and the directory exists; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModelFile,DiGi.GIS.Classes.Building2D)'></a>

## Create\.OrtoDataControls\(this GISModelFile, Building2D\) Method

Creates a list of Orto data controls for the specified GIS model file and 2D building entity\.

```csharp
public static System.Collections.Generic.List<DiGi.GIS.UI.Controls.OrtoDataControl>? OrtoDataControls(this DiGi.GIS.Classes.GISModelFile? gISModelFile, DiGi.GIS.Classes.Building2D? building2D);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModelFile,DiGi.GIS.Classes.Building2D).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file that stores the GIS model data\.

<a name='DiGi.GIS.UI.Create.OrtoDataControls(thisDiGi.GIS.Classes.GISModelFile,DiGi.GIS.Classes.Building2D).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The 2D building entity associated with the controls\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[OrtoDataControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDataControl 'DiGi\.GIS\.UI\.Controls\.OrtoDataControl')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [OrtoDataControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDataControl 'DiGi\.GIS\.UI\.Controls\.OrtoDataControl') instances if both parameters are non\-null; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.OrtoDatasBuilding2DOptions()'></a>

## Create\.OrtoDatasBuilding2DOptions\(\) Method

Creates a new instance of [OrtoDatasBuilding2DOptions\(\)](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.OrtoDatasBuilding2DOptions() 'DiGi\.GIS\.UI\.Create\.OrtoDatasBuilding2DOptions\(\)') with default configuration settings\.

```csharp
public static DiGi.GIS.Classes.OrtoDatasBuilding2DOptions? OrtoDatasBuilding2DOptions();
```

#### Returns
[DiGi\.GIS\.Classes\.OrtoDatasBuilding2DOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatasbuilding2doptions 'DiGi\.GIS\.Classes\.OrtoDatasBuilding2DOptions')  
An instance of [OrtoDatasBuilding2DOptions\(\)](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.OrtoDatasBuilding2DOptions() 'DiGi\.GIS\.UI\.Create\.OrtoDatasBuilding2DOptions\(\)') containing the default options\.

<a name='DiGi.GIS.UI.Create.Typologies(thisSystem.Windows.Window,string)'></a>

## Create\.Typologies\(this Window, string\) Method

Prompts the user to select a folder containing GIS model files and retrieves the associated typologies\.

```csharp
public static System.Collections.Generic.List<DiGi.Typology.Classes.Typology>? Typologies(this System.Windows.Window? owner, out string? directory);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typologies(thisSystem.Windows.Window,string).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Create.Typologies(thisSystem.Windows.Window,string).directory'></a>

`directory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

When this method returns, contains the path of the selected directory, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if no directory was selected\.

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
A list of [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') objects found in the selected folder, or [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null') if the operation was cancelled or failed\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_1(DiGi.GIS.Classes.GISModel,string,double,double)'></a>

## Create\.Typology\_NonResidential\_1\(GISModel, string, double, double\) Method

Creates a non\-residential typology instance based on the provided GIS model and configuration parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_1(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_1(DiGi.GIS.Classes.GISModel,string,double,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model used to retrieve building data for the typology creation\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_1(DiGi.GIS.Classes.GISModel,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description to assign to the created typology object\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_1(DiGi.GIS.Classes.GISModel,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to determine the thinness of the generated geometry\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_1(DiGi.GIS.Classes.GISModel,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during the creation process\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if successful; otherwise, `null` if the provided GIS model is null\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_2(DiGi.GIS.Classes.GISModel,string,double)'></a>

## Create\.Typology\_NonResidential\_2\(GISModel, string, double\) Method

Creates a non\-residential typology based on the provided GIS model and specified parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_2(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_2(DiGi.GIS.Classes.GISModel,string,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the building data to be processed\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_2(DiGi.GIS.Classes.GISModel,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology object\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_2(DiGi.GIS.Classes.GISModel,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to evaluate thinness, with a default value of 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if the model is valid and objects are found; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_3(DiGi.GIS.Classes.GISModel,string,double)'></a>

## Create\.Typology\_NonResidential\_3\(GISModel, string, double\) Method

Creates a non\-residential typology object of type 3 based on the provided GIS model, description, and thinness ratio\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_3(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_3(DiGi.GIS.Classes.GISModel,string,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the spatial data used to generate the typology\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_3(DiGi.GIS.Classes.GISModel,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description to associate with the created typology object\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_3(DiGi.GIS.Classes.GISModel,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The thinness ratio used as a parameter for the typology calculation\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if successful; otherwise, `null` if the GIS model is null or no valid data is found\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double)'></a>

## Create\.Typology\_NonResidential\_YearBuiltData\_1\(GISModelFile, string, double, double\) Method

Creates a typology for non\-residential buildings based on year built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A description to associate with the generated typology\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used for thickness calculations\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during processing\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if the GIS model is valid and processed; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double)'></a>

## Create\.Typology\_NonResidential\_YearBuiltData\_2\(GISModelFile, string, double\) Method

Creates a non\-residential typology based on year built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A description to be associated with the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The thinness ratio used for processing building geometry\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if the model file is valid and contains data; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double)'></a>

## Create\.Typology\_NonResidential\_YearBuiltData\_3\(GISModelFile, string, double\) Method

Creates a non\-residential typology based on year built data \(version 3\)\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_NonResidential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A description for the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_NonResidential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The thinness ratio used during processing\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if successful; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_1(DiGi.GIS.Classes.GISModel,string,double,double)'></a>

## Create\.Typology\_Residential\_1\(GISModel, string, double, double\) Method

Creates a residential typology \(type 1\) based on the provided GIS model and configuration parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_1(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_1(DiGi.GIS.Classes.GISModel,string,double,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the objects used to generate the typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_1(DiGi.GIS.Classes.GISModel,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the created typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_1(DiGi.GIS.Classes.GISModel,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The thinness ratio to be applied during creation\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_1(DiGi.GIS.Classes.GISModel,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value to be applied\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if successful; otherwise, `null` if the provided GIS model is null\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_2(DiGi.GIS.Classes.GISModel,string,double,double)'></a>

## Create\.Typology\_Residential\_2\(GISModel, string, double, double\) Method

Creates a residential typology of type 2 based on the provided GIS model and specified parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_2(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_2(DiGi.GIS.Classes.GISModel,string,double,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the objects used to generate the typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_2(DiGi.GIS.Classes.GISModel,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology object\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_2(DiGi.GIS.Classes.GISModel,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to calculate thinness\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_2(DiGi.GIS.Classes.GISModel,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during creation\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if the operation is successful; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_3(DiGi.GIS.Classes.GISModel,string,double)'></a>

## Create\.Typology\_Residential\_3\(GISModel, string, double\) Method

Creates a residential typology of type 3 based on the specified GIS model and parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_3(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_3(DiGi.GIS.Classes.GISModel,string,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the objects used to generate the typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_3(DiGi.GIS.Classes.GISModel,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the created typology object\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_3(DiGi.GIS.Classes.GISModel,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to determine the thinness of the residential elements\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if successful; otherwise, `null` if the [gISModel](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.Typology_Residential_3(DiGi.GIS.Classes.GISModel,string,double).gISModel 'DiGi\.GIS\.UI\.Create\.Typology\_Residential\_3\(DiGi\.GIS\.Classes\.GISModel, string, double\)\.gISModel') is null\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double)'></a>

## Create\.Typology\_Residential\_4\(GISModel, string, double, double\) Method

Creates a residential typology of type 4 based on the provided GIS model and configuration parameters\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_4(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model containing the spatial data used to generate the typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology object\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to control the thinness of the generated elements\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during the creation process\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if the operation is successful; otherwise, `null` if the [gISModel](DiGi.GIS.UI.md#DiGi.GIS.UI.Create.Typology_Residential_4(DiGi.GIS.Classes.GISModel,string,double,double).gISModel 'DiGi\.GIS\.UI\.Create\.Typology\_Residential\_4\(DiGi\.GIS\.Classes\.GISModel, string, double, double\)\.gISModel') is null\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_5(DiGi.GIS.Classes.GISModel,string,double)'></a>

## Create\.Typology\_Residential\_5\(GISModel, string, double\) Method

Creates a residential typology of type 5 based on the provided GIS model and specifications\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_5(DiGi.GIS.Classes.GISModel? gISModel, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_5(DiGi.GIS.Classes.GISModel,string,double).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

The GIS model used to retrieve building objects for the typology creation\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_5(DiGi.GIS.Classes.GISModel,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description to associate with the created typology object\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_5(DiGi.GIS.Classes.GISModel,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to determine the thinness of the residential typology elements\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if successfully created; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double)'></a>

## Create\.Typology\_Residential\_YearBuiltData\_1\(GISModelFile, string, double, double\) Method

Creates a residential typology based on year\-built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to determine thickness during processing\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_1(DiGi.GIS.Classes.GISModelFile,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during the process\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if successfully created; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double,double)'></a>

## Create\.Typology\_Residential\_YearBuiltData\_2\(GISModelFile, string, double, double\) Method

Creates a residential typology based on year\-built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

A description to be associated with the generated typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used for thickness calculations\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_2(DiGi.GIS.Classes.GISModelFile,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during processing\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if the model file is valid and data is processed; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double)'></a>

## Create\.Typology\_Residential\_YearBuiltData\_3\(GISModelFile, string, double\) Method

Creates a residential typology based on year built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_3(DiGi.GIS.Classes.GISModelFile,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to calculate thickness or thinning of elements\. Defaults to 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') instance if a valid GIS model is provided and processed; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile,string,double,double)'></a>

## Create\.Typology\_Residential\_YearBuiltData\_4\(GISModelFile, string, double, double\) Method

Creates a residential typology based on year\-built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9, double offset=1.0);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile,string,double,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile,string,double,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile,string,double,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to determine thinning of the geometry\. Defaults to 0\.90\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_4(DiGi.GIS.Classes.GISModelFile,string,double,double).offset'></a>

`offset` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The offset value applied during processing\. Defaults to 1\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if a valid GIS model is provided; otherwise, `null`\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_5(DiGi.GIS.Classes.GISModelFile,string,double)'></a>

## Create\.Typology\_Residential\_YearBuiltData\_5\(GISModelFile, string, double\) Method

Creates a residential typology based on year\-built data from the specified GIS model file\.

```csharp
public static DiGi.Typology.Classes.Typology? Typology_Residential_YearBuiltData_5(DiGi.GIS.Classes.GISModelFile? gISModelFile, string? description, double thinnesRatio=0.9);
```
#### Parameters

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_5(DiGi.GIS.Classes.GISModelFile,string,double).gISModelFile'></a>

`gISModelFile` [DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')

The GIS model file containing the source data\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_5(DiGi.GIS.Classes.GISModelFile,string,double).description'></a>

`description` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional description for the resulting typology\.

<a name='DiGi.GIS.UI.Create.Typology_Residential_YearBuiltData_5(DiGi.GIS.Classes.GISModelFile,string,double).thinnesRatio'></a>

`thinnesRatio` [System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')

The ratio used to calculate thickness or geometric properties, with a default value of 0\.90\.

#### Returns
[DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology')  
A [DiGi\.Typology\.Classes\.Typology](https://learn.microsoft.com/en-us/dotnet/api/digi.typology.classes.typology 'DiGi\.Typology\.Classes\.Typology') object if the model file is valid and contains data; otherwise, `null`\.

<a name='DiGi.GIS.UI.Modify'></a>

## Modify Class

```csharp
public static class Modify
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Modify
### Methods

<a name='DiGi.GIS.UI.Modify.Append(thisDiGi.GIS.Classes.Building2DYearBuiltPredictionsFile,DiGi.YOLO.Classes.BoundingBoxResultFile)'></a>

## Modify\.Append\(this Building2DYearBuiltPredictionsFile, BoundingBoxResultFile\) Method

Appends the data from a bounding box result file to the specified building 2D year built predictions file\.

```csharp
public static bool Append(this DiGi.GIS.Classes.Building2DYearBuiltPredictionsFile building2DYearBuiltPredictionsFile, DiGi.YOLO.Classes.BoundingBoxResultFile boundingBoxResultFile);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.Append(thisDiGi.GIS.Classes.Building2DYearBuiltPredictionsFile,DiGi.YOLO.Classes.BoundingBoxResultFile).building2DYearBuiltPredictionsFile'></a>

`building2DYearBuiltPredictionsFile` [DiGi\.GIS\.Classes\.Building2DYearBuiltPredictionsFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2dyearbuiltpredictionsfile 'DiGi\.GIS\.Classes\.Building2DYearBuiltPredictionsFile')

The [DiGi\.GIS\.Classes\.Building2DYearBuiltPredictionsFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2dyearbuiltpredictionsfile 'DiGi\.GIS\.Classes\.Building2DYearBuiltPredictionsFile') instance to which the results will be appended\.

<a name='DiGi.GIS.UI.Modify.Append(thisDiGi.GIS.Classes.Building2DYearBuiltPredictionsFile,DiGi.YOLO.Classes.BoundingBoxResultFile).boundingBoxResultFile'></a>

`boundingBoxResultFile` [DiGi\.YOLO\.Classes\.BoundingBoxResultFile](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.classes.boundingboxresultfile 'DiGi\.YOLO\.Classes\.BoundingBoxResultFile')

The [DiGi\.YOLO\.Classes\.BoundingBoxResultFile](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.classes.boundingboxresultfile 'DiGi\.YOLO\.Classes\.BoundingBoxResultFile') containing the bounding box results to append\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the data was successfully appended; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.AppendBuilding2DYearBuiltPredictionsFile(thisSystem.Windows.Window)'></a>

## Modify\.AppendBuilding2DYearBuiltPredictionsFile\(this Window\) Method

Prompts the user to select a directory containing building 2D year built prediction files and appends them to the current dataset\.

```csharp
public static bool AppendBuilding2DYearBuiltPredictionsFile(this System.Windows.Window owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendBuilding2DYearBuiltPredictionsFile(thisSystem.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') that owns the folder selection dialog\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the operation was completed successfully; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.AppendBuildingModels(thisSystem.Windows.Window)'></a>

## Modify\.AppendBuildingModels\(this Window\) Method

Appends building models associated with the specified window owner\.

```csharp
public static System.Collections.Generic.HashSet<string>? AppendBuildingModels(this System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendBuildingModels(thisSystem.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The window that owns the operation\.

#### Returns
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')  
A [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') containing the identifiers of the appended building models, or `null` if no models were found or created\.

<a name='DiGi.GIS.UI.Modify.AppendComparisonTable(System.Windows.Window)'></a>

## Modify\.AppendComparisonTable\(Window\) Method

Appends a comparison table by prompting the user to select a directory containing GIS model files\.

```csharp
public static void AppendComparisonTable(System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendComparisonTable(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The parent window that owns the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.AppendPredictionTable(System.Windows.Window)'></a>

## Modify\.AppendPredictionTable\(Window\) Method

Appends a prediction table by prompting the user to select the directory containing GIS model files\.

```csharp
public static void AppendPredictionTable(System.Windows.Window owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendPredictionTable(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window that serves as the parent for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.AppendPredictionYearBuilts(System.Windows.Window)'></a>

## Modify\.AppendPredictionYearBuilts\(Window\) Method

Prompts the user to select a tab\-separated values \(TSV\) file containing machine learning output for year built predictions and appends the data\.

```csharp
public static bool AppendPredictionYearBuilts(System.Windows.Window owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendPredictionYearBuilts(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The window that owns the file selection dialog\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the operation was successful; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions)'></a>

## Modify\.AppendVoTTModel\_Building2D\(Window, VoTTConversionOptions\) Method

Appends a 2D building VoTT model by prompting the user to select the directory containing the GIS model files\.

```csharp
public static void AppendVoTTModel_Building2D(System.Windows.Window? owner, DiGi.GIS.UI.Classes.VoTTConversionOptions? voTTConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions).voTTConversionOptions'></a>

`voTTConversionOptions` [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions')

Optional conversion options to be applied during the process\.

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions)'></a>

## Modify\.AppendVoTTModel\_OrtoRange\(Window, VoTTConversionOptions\) Method

Appends the VoTT model for OrtoRange by prompting the user to select the GIS model files directory\.

```csharp
public static void AppendVoTTModel_OrtoRange(System.Windows.Window? owner, DiGi.GIS.UI.Classes.VoTTConversionOptions? voTTConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used to host the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.AppendVoTTModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.VoTTConversionOptions).voTTConversionOptions'></a>

`voTTConversionOptions` [VoTTConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.VoTTConversionOptions 'DiGi\.GIS\.UI\.Classes\.VoTTConversionOptions')

Optional conversion options to be applied during the process\.

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions)'></a>

## Modify\.AppendYOLOModel\_Building2D\(Window, YOLOConversionOptions\) Method

Appends a 2D building YOLO model to the current context using the specified conversion options\.

```csharp
public static bool AppendYOLOModel_Building2D(System.Windows.Window? owner, DiGi.GIS.UI.Classes.YOLOConversionOptions? yOLOConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used for displaying modal dialogs\.

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_Building2D(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions).yOLOConversionOptions'></a>

`yOLOConversionOptions` [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions')

The options for YOLO conversion\. If null, a new instance of [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions') will be initialized\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the model was successfully appended; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions)'></a>

## Modify\.AppendYOLOModel\_OrtoRange\(Window, YOLOConversionOptions\) Method

Appends a YOLO model for OrtoRange data, prompting the user to select the GIS model files directory if it is not specified or does not exist\.

```csharp
public static bool AppendYOLOModel_OrtoRange(System.Windows.Window? owner, DiGi.GIS.UI.Classes.YOLOConversionOptions? yOLOConversionOptions=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used for displaying dialogs\.

<a name='DiGi.GIS.UI.Modify.AppendYOLOModel_OrtoRange(System.Windows.Window,DiGi.GIS.UI.Classes.YOLOConversionOptions).yOLOConversionOptions'></a>

`yOLOConversionOptions` [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions')

The options for YOLO conversion; if null, a new instance will be created\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the model was successfully appended; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.CalculateAdministrativeAreal2DStatisticalUnits(System.Windows.Window,bool,string)'></a>

## Modify\.CalculateAdministrativeAreal2DStatisticalUnits\(Window, bool, string\) Method

Calculates administrative areal 2D statistical units by prompting the user to select a source file via a dialog\.

```csharp
public static void CalculateAdministrativeAreal2DStatisticalUnits(System.Windows.Window? owner, bool overrideExisting=true, string? fileNameSufix=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateAdministrativeAreal2DStatisticalUnits(System.Windows.Window,bool,string).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used for the file selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateAdministrativeAreal2DStatisticalUnits(System.Windows.Window,bool,string).overrideExisting'></a>

`overrideExisting` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Indicates whether existing files should be overwritten if they already exist\.

<a name='DiGi.GIS.UI.Modify.CalculateAdministrativeAreal2DStatisticalUnits(System.Windows.Window,bool,string).fileNameSufix'></a>

`fileNameSufix` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

An optional suffix to append to the generated output file name\.

<a name='DiGi.GIS.UI.Modify.CalculateGISModelFiles(System.Windows.Window)'></a>

## Modify\.CalculateGISModelFiles\(Window\) Method

Prompts the user to select a directory using a folder dialog to calculate GIS model files\.

```csharp
public static void CalculateGISModelFiles(System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateGISModelFiles(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateGISModelFiles(System.Windows.Window,DiGi.Core.Interfaces.IDeterminateWorker)'></a>

## Modify\.CalculateGISModelFiles\(Window, IDeterminateWorker\) Method

Calculates and retrieves a set of GIS model files from a directory selected by the user via a folder dialog\.

```csharp
public static System.Collections.Generic.HashSet<string>? CalculateGISModelFiles(System.Windows.Window? owner, DiGi.Core.Interfaces.IDeterminateWorker? determinateWorker=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateGISModelFiles(System.Windows.Window,DiGi.Core.Interfaces.IDeterminateWorker).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateGISModelFiles(System.Windows.Window,DiGi.Core.Interfaces.IDeterminateWorker).determinateWorker'></a>

`determinateWorker` [DiGi\.Core\.Interfaces\.IDeterminateWorker](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.ideterminateworker 'DiGi\.Core\.Interfaces\.IDeterminateWorker')

An optional worker used to report determinate progress during the calculation process\.

#### Returns
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')  
A [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') containing the paths of the identified GIS model files, or `null` if the user cancels the dialog or no folder is selected\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasBuilding2DOptions,int,bool)'></a>

## Modify\.CalculateOrtoDatas\(Window, OrtoDatasBuilding2DOptions, int, bool\) Method

Calculates ortho data for 2D buildings based on the specified configuration and count\.

```csharp
public static System.Threading.Tasks.Task<bool> CalculateOrtoDatas(System.Windows.Window? owner, DiGi.GIS.Classes.OrtoDatasBuilding2DOptions? ortoDatasBuilding2DOptions, int count, bool overrideExisting=false);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasBuilding2DOptions,int,bool).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used to display the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasBuilding2DOptions,int,bool).ortoDatasBuilding2DOptions'></a>

`ortoDatasBuilding2DOptions` [DiGi\.GIS\.Classes\.OrtoDatasBuilding2DOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatasbuilding2doptions 'DiGi\.GIS\.Classes\.OrtoDatasBuilding2DOptions')

The configuration options for the 2D building ortho data calculation\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasBuilding2DOptions,int,bool).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of items to process during the calculation\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasBuilding2DOptions,int,bool).overrideExisting'></a>

`overrideExisting` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A value indicating whether existing ortho data should be overwritten\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is `true` if the calculation was successfully completed; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasOrtoRangeOptions,int)'></a>

## Modify\.CalculateOrtoDatas\(Window, OrtoDatasOrtoRangeOptions, int\) Method

Asynchronously calculates orthodata based on the specified options and count, prompting the user to select a target directory via a folder dialog\.

```csharp
public static System.Threading.Tasks.Task<bool> CalculateOrtoDatas(System.Windows.Window? owner, DiGi.GIS.Classes.OrtoDatasOrtoRangeOptions? ortoDatasOrtoRangeOptions, int count);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasOrtoRangeOptions,int).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasOrtoRangeOptions,int).ortoDatasOrtoRangeOptions'></a>

`ortoDatasOrtoRangeOptions` [DiGi\.GIS\.Classes\.OrtoDatasOrtoRangeOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatasortorangeoptions 'DiGi\.GIS\.Classes\.OrtoDatasOrtoRangeOptions')

The options used for filtering and retrieving orthodata within a specific range, including scale settings\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatas(System.Windows.Window,DiGi.GIS.Classes.OrtoDatasOrtoRangeOptions,int).count'></a>

`count` [System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

The number of items to process during the calculation\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is `true` if the calculations were completed successfully; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(string,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_)'></a>

## Modify\.CalculateOrtoDatasComparisons\(string, OrtoDatasComparisonOptions, IEnumerable\<string\>, Nullable\<int\>\) Method

Calculates comparisons for orthodata located in the specified directory using the provided options and reference data\.

```csharp
public static System.Collections.Generic.HashSet<string>? CalculateOrtoDatasComparisons(string? directory, DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions? ortoDatasComparisonOptions, System.Collections.Generic.IEnumerable<string>? references=null, System.Nullable<int> count=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(string,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).directory'></a>

`directory` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path to the directory containing the orthodata to be compared\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(string,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).ortoDatasComparisonOptions'></a>

`ortoDatasComparisonOptions` [DiGi\.GIS\.Emgu\.CV\.Classes\.OrtoDatasComparisonOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.emgu.cv.classes.ortodatascomparisonoptions 'DiGi\.GIS\.Emgu\.CV\.Classes\.OrtoDatasComparisonOptions')

The options used to configure the orthodata comparison process\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(string,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of reference strings to use during the comparison\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(string,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).count'></a>

`count` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer specifying the number of processors or a limit for the operation\.

#### Returns
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')  
A [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') containing the results of the orthodata comparisons, or `null` if the directory is invalid or does not exist\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(System.Windows.Window,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_)'></a>

## Modify\.CalculateOrtoDatasComparisons\(Window, OrtoDatasComparisonOptions, IEnumerable\<string\>, Nullable\<int\>\) Method

Calculates comparisons for orthodata based on a user\-selected folder and specified options\.

```csharp
public static System.Collections.Generic.HashSet<string>? CalculateOrtoDatasComparisons(System.Windows.Window owner, DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions? ortoDatasComparisonOptions, System.Collections.Generic.IEnumerable<string>? references=null, System.Nullable<int> count=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(System.Windows.Window,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used to display the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(System.Windows.Window,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).ortoDatasComparisonOptions'></a>

`ortoDatasComparisonOptions` [DiGi\.GIS\.Emgu\.CV\.Classes\.OrtoDatasComparisonOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.emgu.cv.classes.ortodatascomparisonoptions 'DiGi\.GIS\.Emgu\.CV\.Classes\.OrtoDatasComparisonOptions')

The options used to configure the orthodata comparison process\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(System.Windows.Window,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).references'></a>

`references` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

An optional collection of reference strings to be used in the comparison\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoDatasComparisons(System.Windows.Window,DiGi.GIS.Emgu.CV.Classes.OrtoDatasComparisonOptions,System.Collections.Generic.IEnumerable_string_,System.Nullable_int_).count'></a>

`count` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

An optional integer specifying a count limit for the calculation\.

#### Returns
[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')  
A [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') containing the results of the orthodata comparisons, or `null` if the operation was cancelled by the user\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoRanges(System.Windows.Window,DiGi.GIS.Classes.OrtoRangeOptions)'></a>

## Modify\.CalculateOrtoRanges\(Window, OrtoRangeOptions\) Method

Calculates the orthophoto ranges by prompting the user to select a directory and applying the specified range options\.

```csharp
public static bool CalculateOrtoRanges(System.Windows.Window? owner, DiGi.GIS.Classes.OrtoRangeOptions? ortoRangeOptions);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateOrtoRanges(System.Windows.Window,DiGi.GIS.Classes.OrtoRangeOptions).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.CalculateOrtoRanges(System.Windows.Window,DiGi.GIS.Classes.OrtoRangeOptions).ortoRangeOptions'></a>

`ortoRangeOptions` [DiGi\.GIS\.Classes\.OrtoRangeOptions](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortorangeoptions 'DiGi\.GIS\.Classes\.OrtoRangeOptions')

The options specifying the range of the orthophoto, including width and height dimensions\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if a directory was selected and the ranges were successfully calculated; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.CalculateTypologies(System.Windows.Window)'></a>

## Modify\.CalculateTypologies\(Window\) Method

Calculates the typologies based on the provided owner window\.

```csharp
public static bool CalculateTypologies(System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.CalculateTypologies(System.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window used for any modal dialogs during the calculation process\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the typologies were successfully calculated; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.Download3DModels(thisSystem.Windows.Window)'></a>

## Modify\.Download3DModels\(this Window\) Method

Prompts the user to select a ZIP file containing 3D models using an open file dialog\.

```csharp
public static System.Threading.Tasks.Task<System.Collections.Generic.HashSet<string>?> Download3DModels(this System.Windows.Window? owner);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.Download3DModels(thisSystem.Windows.Window).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window that will host the file dialog\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Collections\.Generic\.HashSet&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains a [System\.Collections\.Generic\.HashSet&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1 'System\.Collections\.Generic\.HashSet\`1') of strings if a file was selected; otherwise, [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/keywords/null')\.

<a name='DiGi.GIS.UI.Modify.ResaveOrtoDatasFiles(System.Windows.Window,bool)'></a>

## Modify\.ResaveOrtoDatasFiles\(Window, bool\) Method

Resaves orthodata files from a directory selected by the user via a folder dialog\.

```csharp
public static void ResaveOrtoDatasFiles(System.Windows.Window? owner, bool updateScale);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.ResaveOrtoDatasFiles(System.Windows.Window,bool).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The optional owner window used to display the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.ResaveOrtoDatasFiles(System.Windows.Window,bool).updateScale'></a>

`updateScale` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

A value indicating whether the scale should be updated during the resaving process\.

<a name='DiGi.GIS.UI.Modify.TryDownload(string,string)'></a>

## Modify\.TryDownload\(string, string\) Method

Asynchronously attempts to download a file from the specified URL and save it to the provided local path\.

```csharp
public static System.Threading.Tasks.Task<bool> TryDownload(string url, string path);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.TryDownload(string,string).url'></a>

`url` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The URL of the resource to download\.

<a name='DiGi.GIS.UI.Modify.TryDownload(string,string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The destination file path where the downloaded content will be saved\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result is [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the download was successful; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.UI.Modify.WriteAdministrativeAreal2DsIndexData(System.Windows.Window,string)'></a>

## Modify\.WriteAdministrativeAreal2DsIndexData\(Window, string\) Method

Writes administrative areal 2D index data to a directory selected by the user via a folder dialog\.

```csharp
public static bool WriteAdministrativeAreal2DsIndexData(System.Windows.Window? owner, string? path);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.WriteAdministrativeAreal2DsIndexData(System.Windows.Window,string).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window of the folder selection dialog\.

<a name='DiGi.GIS.UI.Modify.WriteAdministrativeAreal2DsIndexData(System.Windows.Window,string).path'></a>

`path` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The initial path or target path for writing the index data\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the operation was completed successfully; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.WriteImages(System.Windows.Window,bool,DiGi.Core.Classes.Range_int_)'></a>

## Modify\.WriteImages\(Window, bool, Range\<int\>\) Method

Prompts the user to select a GIS model file and writes images based on the specified parameters\.

```csharp
public static bool WriteImages(System.Windows.Window? owner, bool drawGeometry=false, DiGi.Core.Classes.Range<int>? range=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.WriteImages(System.Windows.Window,bool,DiGi.Core.Classes.Range_int_).owner'></a>

`owner` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the file dialog\.

<a name='DiGi.GIS.UI.Modify.WriteImages(System.Windows.Window,bool,DiGi.Core.Classes.Range_int_).drawGeometry'></a>

`drawGeometry` [System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

Indicates whether geometry should be drawn in the output images\.

<a name='DiGi.GIS.UI.Modify.WriteImages(System.Windows.Window,bool,DiGi.Core.Classes.Range_int_).range'></a>

`range` [DiGi\.Core\.Classes\.Range&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')

An optional range of indices to process for image writing\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the images were successfully written; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.WriteImages(thisDiGi.GIS.Classes.Building2D,string,string)'></a>

## Modify\.WriteImages\(this Building2D, string, string\) Method

Writes images for the specified 2D building using source orthodata files and saves them to the designated output directory\.

```csharp
public static bool WriteImages(this DiGi.GIS.Classes.Building2D? building2D, string? directory_OrtoDatasFiles, string? directory_Output);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.WriteImages(thisDiGi.GIS.Classes.Building2D,string,string).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D') instance for which images are to be generated\.

<a name='DiGi.GIS.UI.Modify.WriteImages(thisDiGi.GIS.Classes.Building2D,string,string).directory_OrtoDatasFiles'></a>

`directory_OrtoDatasFiles` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path to the directory containing the orthodata source files\.

<a name='DiGi.GIS.UI.Modify.WriteImages(thisDiGi.GIS.Classes.Building2D,string,string).directory_Output'></a>

`directory_Output` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The path to the directory where the output images will be written\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the images were successfully written; otherwise, `false`\.

<a name='DiGi.GIS.UI.Modify.WriteStatisticalDataCollections(System.Collections.Generic.IEnumerable_DiGi.BDL.Enums.Variable_,DiGi.Core.Classes.Range_int_)'></a>

## Modify\.WriteStatisticalDataCollections\(IEnumerable\<Variable\>, Range\<int\>\) Method

Asynchronously writes the specified statistical data collections to a file selected via an open file dialog\.

```csharp
public static System.Threading.Tasks.Task<bool> WriteStatisticalDataCollections(System.Collections.Generic.IEnumerable<DiGi.BDL.Enums.Variable>? variables, DiGi.Core.Classes.Range<int>? years);
```
#### Parameters

<a name='DiGi.GIS.UI.Modify.WriteStatisticalDataCollections(System.Collections.Generic.IEnumerable_DiGi.BDL.Enums.Variable_,DiGi.Core.Classes.Range_int_).variables'></a>

`variables` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.BDL\.Enums\.Variable](https://learn.microsoft.com/en-us/dotnet/api/digi.bdl.enums.variable 'DiGi\.BDL\.Enums\.Variable')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

The collection of variables to be written to the file\.

<a name='DiGi.GIS.UI.Modify.WriteStatisticalDataCollections(System.Collections.Generic.IEnumerable_DiGi.BDL.Enums.Variable_,DiGi.Core.Classes.Range_int_).years'></a>

`years` [DiGi\.Core\.Classes\.Range&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.range-1 'DiGi\.Core\.Classes\.Range\`1')

The range of years for which the statistical data should be recorded\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the data was successfully written; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.UI.Modify.WriteStatisticalUnit()'></a>

## Modify\.WriteStatisticalUnit\(\) Method

Asynchronously prompts the user to save statistical unit data using a file save dialog\.

```csharp
public static System.Threading.Tasks.Task<bool> WriteStatisticalUnit();
```

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
A task that represents the asynchronous operation\. The task result contains [true](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool') if the file was successfully saved; otherwise, [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs\.microsoft\.com/en\-us/dotnet/csharp/language\-reference/builtin\-types/bool')\.

<a name='DiGi.GIS.UI.Query'></a>

## Query Class

```csharp
public static class Query
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Query
### Methods

<a name='DiGi.GIS.UI.Query.AdministrativeAreal2Ds_TAdministrativeAreal2D_(thisDiGi.GIS.Classes.GISModel,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_)'></a>

## Query\.AdministrativeAreal2Ds\<TAdministrativeAreal2D\>\(this GISModel, IEnumerable\<Building2D\>\) Method

Gets common AdministrativeAreal2Ds for given building2Ds

```csharp
public static System.Collections.Generic.List<TAdministrativeAreal2D>? AdministrativeAreal2Ds<TAdministrativeAreal2D>(this DiGi.GIS.Classes.GISModel? gISModel, System.Collections.Generic.IEnumerable<DiGi.GIS.Classes.Building2D>? building2Ds)
    where TAdministrativeAreal2D : DiGi.GIS.Classes.AdministrativeAreal2D;
```
#### Type parameters

<a name='DiGi.GIS.UI.Query.AdministrativeAreal2Ds_TAdministrativeAreal2D_(thisDiGi.GIS.Classes.GISModel,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_).TAdministrativeAreal2D'></a>

`TAdministrativeAreal2D`

Subtype of AdministrativeAreal2D
#### Parameters

<a name='DiGi.GIS.UI.Query.AdministrativeAreal2Ds_TAdministrativeAreal2D_(thisDiGi.GIS.Classes.GISModel,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_).gISModel'></a>

`gISModel` [DiGi\.GIS\.Classes\.GISModel](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodel 'DiGi\.GIS\.Classes\.GISModel')

GISModel

<a name='DiGi.GIS.UI.Query.AdministrativeAreal2Ds_TAdministrativeAreal2D_(thisDiGi.GIS.Classes.GISModel,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_).building2Ds'></a>

`building2Ds` [System\.Collections\.Generic\.IEnumerable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1 'System\.Collections\.Generic\.IEnumerable\`1')

Buidling2Ds

#### Returns
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[TAdministrativeAreal2D](DiGi.GIS.UI.md#DiGi.GIS.UI.Query.AdministrativeAreal2Ds_TAdministrativeAreal2D_(thisDiGi.GIS.Classes.GISModel,System.Collections.Generic.IEnumerable_DiGi.GIS.Classes.Building2D_).TAdministrativeAreal2D 'DiGi\.GIS\.UI\.Query\.AdministrativeAreal2Ds\<TAdministrativeAreal2D\>\(this DiGi\.GIS\.Classes\.GISModel, System\.Collections\.Generic\.IEnumerable\<DiGi\.GIS\.Classes\.Building2D\>\)\.TAdministrativeAreal2D')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')  
List of common AdministrativeAreal2Ds

<a name='DiGi.GIS.UI.Query.Category(thisDiGi.GIS.UI.Classes.YOLOConversionOptions,System.Random)'></a>

## Query\.Category\(this YOLOConversionOptions, Random\) Method

Determines the YOLO category based on the provided conversion options\.

```csharp
public static System.Nullable<DiGi.YOLO.Enums.Category> Category(this DiGi.GIS.UI.Classes.YOLOConversionOptions? yOLOConversionOptions, System.Random? random=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Query.Category(thisDiGi.GIS.UI.Classes.YOLOConversionOptions,System.Random).yOLOConversionOptions'></a>

`yOLOConversionOptions` [YOLOConversionOptions](DiGi.GIS.UI.Classes.md#DiGi.GIS.UI.Classes.YOLOConversionOptions 'DiGi\.GIS\.UI\.Classes\.YOLOConversionOptions')

The YOLO conversion options used to derive the category\.

<a name='DiGi.GIS.UI.Query.Category(thisDiGi.GIS.UI.Classes.YOLOConversionOptions,System.Random).random'></a>

`random` [System\.Random](https://learn.microsoft.com/en-us/dotnet/api/system.random 'System\.Random')

An optional random number generator for selection logic\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The determined [DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category'), or `null` if it cannot be determined\.

<a name='DiGi.GIS.UI.Query.Category(thisSystem.Collections.Generic.Dictionary_DiGi.YOLO.Enums.Category,double_,System.Random)'></a>

## Query\.Category\(this Dictionary\<Category,double\>, Random\) Method

Selects a category from the provided weight distribution using a weighted random selection process\.

```csharp
public static System.Nullable<DiGi.YOLO.Enums.Category> Category(this System.Collections.Generic.Dictionary<DiGi.YOLO.Enums.Category,double>? weights, System.Random? random=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Query.Category(thisSystem.Collections.Generic.Dictionary_DiGi.YOLO.Enums.Category,double_,System.Random).weights'></a>

`weights` [System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

A dictionary containing categories as keys and their corresponding weights as values\.

<a name='DiGi.GIS.UI.Query.Category(thisSystem.Collections.Generic.Dictionary_DiGi.YOLO.Enums.Category,double_,System.Random).random'></a>

`random` [System\.Random](https://learn.microsoft.com/en-us/dotnet/api/system.random 'System\.Random')

An optional [System\.Random](https://learn.microsoft.com/en-us/dotnet/api/system.random 'System\.Random') instance to use for the selection; if null, a new instance is created\.

#### Returns
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')  
The selected [DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category'), or `null` if the weights dictionary is null or empty\.

<a name='DiGi.GIS.UI.Query.ColumnName(thisDiGi.BDL.Enums.Variable)'></a>

## Query\.ColumnName\(this Variable\) Method

Gets the column name associated with the specified variable\.

```csharp
public static string? ColumnName(this DiGi.BDL.Enums.Variable variable);
```
#### Parameters

<a name='DiGi.GIS.UI.Query.ColumnName(thisDiGi.BDL.Enums.Variable).variable'></a>

`variable` [DiGi\.BDL\.Enums\.Variable](https://learn.microsoft.com/en-us/dotnet/api/digi.bdl.enums.variable 'DiGi\.BDL\.Enums\.Variable')

The [DiGi\.BDL\.Enums\.Variable](https://learn.microsoft.com/en-us/dotnet/api/digi.bdl.enums.variable 'DiGi\.BDL\.Enums\.Variable') for which to retrieve the column name\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The column name as a string, or `null` if no corresponding name is found\.

<a name='DiGi.GIS.UI.Query.GISModelFilePath(System.Windows.Window)'></a>

## Query\.GISModelFilePath\(Window\) Method

Opens a file dialog to select the path of a GIS model file\.

```csharp
public static string? GISModelFilePath(System.Windows.Window window);
```
#### Parameters

<a name='DiGi.GIS.UI.Query.GISModelFilePath(System.Windows.Window).window'></a>

`window` [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window')

The owner window for the file selection dialog\.

#### Returns
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')  
The full path of the selected GIS model file, or `null` if the operation was canceled\.

<a name='DiGi.GIS.UI.Query.Weights()'></a>

## Query\.Weights\(\) Method

Initializes and returns a dictionary containing the default weights for all available YOLO categories\.

```csharp
public static System.Collections.Generic.Dictionary<DiGi.YOLO.Enums.Category,double> Weights();
```

#### Returns
[System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Double](https://learn.microsoft.com/en-us/dotnet/api/system.double 'System\.Double')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')  
A [System\.Collections\.Generic\.Dictionary&lt;&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2') where each [DiGi\.YOLO\.Enums\.Category](https://learn.microsoft.com/en-us/dotnet/api/digi.yolo.enums.category 'DiGi\.YOLO\.Enums\.Category') is mapped to its default weight value\.

<a name='DiGi.GIS.UI.Settings'></a>

## Settings Class

```csharp
public static class Settings
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Settings
### Properties

<a name='DiGi.GIS.UI.Settings.DefaultDirectory'></a>

## Settings\.DefaultDirectory Property

Gets the default directory path based on the location of the currently executing assembly\.

```csharp
public static string? DefaultDirectory { get; }
```

#### Property Value
[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')