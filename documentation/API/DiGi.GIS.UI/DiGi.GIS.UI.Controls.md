#### [DiGi\.GIS\.UI](index.md 'index')

## DiGi\.GIS\.UI\.Controls Namespace
### Classes

<a name='DiGi.GIS.UI.Controls.Building2DControl'></a>

## Building2DControl Class

Interaction logic for Building2DControl\.xaml

```csharp
public class Building2DControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Controls\.UserControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol 'System\.Windows\.Controls\.UserControl') → Building2DControl

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Controls.Building2DControl.Building2DControl()'></a>

## Building2DControl\(\) Constructor

Initializes a new instance of the [Building2DControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.Building2DControl 'DiGi\.GIS\.UI\.Controls\.Building2DControl') class\.

```csharp
public Building2DControl();
```
### Properties

<a name='DiGi.GIS.UI.Controls.Building2DControl.Building2D'></a>

## Building2DControl\.Building2D Property

Gets or sets the [Building2D](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.Building2DControl.Building2D 'DiGi\.GIS\.UI\.Controls\.Building2DControl\.Building2D') entity associated with this control\.

```csharp
public DiGi.GIS.Classes.Building2D? Building2D { get; set; }
```

#### Property Value
[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')
### Methods

<a name='DiGi.GIS.UI.Controls.Building2DControl.InitializeComponent()'></a>

## Building2DControl\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Controls.OrtoDataControl'></a>

## OrtoDataControl Class

Interaction logic for OrtoDataControl\.xaml

```csharp
public class OrtoDataControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Controls\.UserControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol 'System\.Windows\.Controls\.UserControl') → OrtoDataControl

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.OrtoDataControl()'></a>

## OrtoDataControl\(\) Constructor

Initializes a new instance of the [OrtoDataControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDataControl 'DiGi\.GIS\.UI\.Controls\.OrtoDataControl') class\.

```csharp
public OrtoDataControl();
```
### Properties

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.Active'></a>

## OrtoDataControl\.Active Property

Gets or sets a value indicating whether the control is currently active\.

```csharp
public bool Active { get; set; }
```

#### Property Value
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.BitmapImage'></a>

## OrtoDataControl\.BitmapImage Property

Gets or sets the bitmap image associated with the orthophoto data\.

```csharp
public System.Windows.Media.Imaging.BitmapImage? BitmapImage { get; set; }
```

#### Property Value
[System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage')

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.PredictedYear'></a>

## OrtoDataControl\.PredictedYear Property

Gets or sets the predicted year of the orthophoto data\.

```csharp
public System.Nullable<short> PredictedYear { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.Year'></a>

## OrtoDataControl\.Year Property

Gets or sets the year associated with the orthophoto data\.

```csharp
public short Year { get; set; }
```

#### Property Value
[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')
### Methods

<a name='DiGi.GIS.UI.Controls.OrtoDataControl.InitializeComponent()'></a>

## OrtoDataControl\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl'></a>

## OrtoDatasControl Class

Interaction logic for OrtoDatasControl\.xaml

```csharp
public class OrtoDatasControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Controls\.UserControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol 'System\.Windows\.Controls\.UserControl') → OrtoDatasControl

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.OrtoDatasControl()'></a>

## OrtoDatasControl\(\) Constructor

Initializes a new instance of the [OrtoDatasControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDatasControl 'DiGi\.GIS\.UI\.Controls\.OrtoDatasControl') class\.

```csharp
public OrtoDatasControl();
```

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.OrtoDatasControl(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_)'></a>

## OrtoDatasControl\(OrtoDatas, Nullable\<short\>\) Constructor

Initializes a new instance of the [OrtoDatasControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDatasControl 'DiGi\.GIS\.UI\.Controls\.OrtoDatasControl') class with the specified orthophoto data and optional year\.

```csharp
public OrtoDatasControl(DiGi.GIS.Classes.OrtoDatas? ortoDatas, System.Nullable<short> year=null);
```
#### Parameters

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.OrtoDatasControl(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_).ortoDatas'></a>

`ortoDatas` [DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

The orthophoto data to be associated with the control\.

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.OrtoDatasControl(DiGi.GIS.Classes.OrtoDatas,System.Nullable_short_).year'></a>

`year` [System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

The optional year associated with the orthophoto data\.
### Properties

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.ImageSize'></a>

## OrtoDatasControl\.ImageSize Property

Gets or sets the dimensions of the image\.

```csharp
public DiGi.Core.Classes.Size? ImageSize { get; set; }
```

#### Property Value
[DiGi\.Core\.Classes\.Size](https://learn.microsoft.com/en-us/dotnet/api/digi.core.classes.size 'DiGi\.Core\.Classes\.Size')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Margin'></a>

## OrtoDatasControl\.Margin Property

Gets or sets the margin value for the control\.

```csharp
public int Margin { get; set; }
```

#### Property Value
[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.OrtoDatas'></a>

## OrtoDatasControl\.OrtoDatas Property

Gets or sets the orthophoto data associated with this control\.

```csharp
public DiGi.GIS.Classes.OrtoDatas? OrtoDatas { get; set; }
```

#### Property Value
[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Year'></a>

## OrtoDatasControl\.Year Property

Gets or sets the year associated with the orthophoto data\.

```csharp
public System.Nullable<short> Year { get; set; }
```

#### Property Value
[System\.Nullable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1 'System\.Nullable\`1')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Years'></a>

## OrtoDatasControl\.Years Property

Gets the list of years associated with the orthodata\.

```csharp
public System.Collections.Generic.List<short>? Years { get; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')
### Methods

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.InitializeComponent()'></a>

## OrtoDatasControl\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Update(System.Windows.Media.Imaging.BitmapImage,short)'></a>

## OrtoDatasControl\.Update\(BitmapImage, short\) Method

Updates the ortho data using the specified bitmap image and year\.

```csharp
public bool Update(System.Windows.Media.Imaging.BitmapImage? bitmapImage, short year);
```
#### Parameters

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Update(System.Windows.Media.Imaging.BitmapImage,short).bitmapImage'></a>

`bitmapImage` [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage')

The [System\.Windows\.Media\.Imaging\.BitmapImage](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapimage 'System\.Windows\.Media\.Imaging\.BitmapImage') to update, or `null`\.

<a name='DiGi.GIS.UI.Controls.OrtoDatasControl.Update(System.Windows.Media.Imaging.BitmapImage,short).year'></a>

`year` [System\.Int16](https://learn.microsoft.com/en-us/dotnet/api/system.int16 'System\.Int16')

The year associated with the ortho data\.

#### Returns
[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')  
`true` if the update was successful; otherwise, `false`\.

<a name='DiGi.GIS.UI.Controls.OrtoDatasListControl'></a>

## OrtoDatasListControl Class

Interaction logic for OrtoDatasListControl\.xaml

```csharp
public class OrtoDatasListControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Controls\.UserControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol 'System\.Windows\.Controls\.UserControl') → OrtoDatasListControl

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Controls.OrtoDatasListControl.OrtoDatasListControl()'></a>

## OrtoDatasListControl\(\) Constructor

Initializes a new instance of the [OrtoDatasListControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.OrtoDatasListControl 'DiGi\.GIS\.UI\.Controls\.OrtoDatasListControl') class\.

```csharp
public OrtoDatasListControl();
```
### Properties

<a name='DiGi.GIS.UI.Controls.OrtoDatasListControl.OrtoDatasList'></a>

## OrtoDatasListControl\.OrtoDatasList Property

Gets or sets the list of orthophoto data available in the control\.

```csharp
public System.Collections.Generic.List<DiGi.GIS.Classes.OrtoDatas>? OrtoDatasList { get; set; }
```

#### Property Value
[System\.Collections\.Generic\.List&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1 'System\.Collections\.Generic\.List\`1')

<a name='DiGi.GIS.UI.Controls.OrtoDatasListControl.SelectedOrtoDatas'></a>

## OrtoDatasListControl\.SelectedOrtoDatas Property

Gets the currently selected orthophoto data from the list control\.

```csharp
public DiGi.GIS.Classes.OrtoDatas? SelectedOrtoDatas { get; }
```

#### Property Value
[DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas')  
The [DiGi\.GIS\.Classes\.OrtoDatas](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.ortodatas 'DiGi\.GIS\.Classes\.OrtoDatas') object that is currently selected, or `null` if no item is selected\.
### Methods

<a name='DiGi.GIS.UI.Controls.OrtoDatasListControl.InitializeComponent()'></a>

## OrtoDatasListControl\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Controls.YearBuiltsControl'></a>

## YearBuiltsControl Class

Interaction logic for YearBuiltsControl\.xaml

```csharp
public class YearBuiltsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Controls\.UserControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.usercontrol 'System\.Windows\.Controls\.UserControl') → YearBuiltsControl

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Controls.YearBuiltsControl.YearBuiltsControl()'></a>

## YearBuiltsControl\(\) Constructor

Initializes a new instance of the [YearBuiltsControl](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.YearBuiltsControl 'DiGi\.GIS\.UI\.Controls\.YearBuiltsControl') class\.

```csharp
public YearBuiltsControl();
```
### Properties

<a name='DiGi.GIS.UI.Controls.YearBuiltsControl.Building2D'></a>

## YearBuiltsControl\.Building2D Property

Gets the currently selected [Building2D](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.YearBuiltsControl.Building2D 'DiGi\.GIS\.UI\.Controls\.YearBuiltsControl\.Building2D') entity from the control's list\.

```csharp
public DiGi.GIS.Classes.Building2D? Building2D { get; }
```

#### Property Value
[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')  
The [Building2D](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.YearBuiltsControl.Building2D 'DiGi\.GIS\.UI\.Controls\.YearBuiltsControl\.Building2D') associated with the selected item, or `null` if no valid building is selected\.

<a name='DiGi.GIS.UI.Controls.YearBuiltsControl.GISModelFile'></a>

## YearBuiltsControl\.GISModelFile Property

Gets or sets the [GISModelFile](DiGi.GIS.UI.Controls.md#DiGi.GIS.UI.Controls.YearBuiltsControl.GISModelFile 'DiGi\.GIS\.UI\.Controls\.YearBuiltsControl\.GISModelFile') associated with this control, which stores GIS model data with external reference support\.

```csharp
public DiGi.GIS.Classes.GISModelFile? GISModelFile { get; set; }
```

#### Property Value
[DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')
### Methods

<a name='DiGi.GIS.UI.Controls.YearBuiltsControl.InitializeComponent()'></a>

## YearBuiltsControl\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')