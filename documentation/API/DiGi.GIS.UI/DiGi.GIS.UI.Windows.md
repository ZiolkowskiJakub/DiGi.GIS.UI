#### [DiGi\.GIS\.UI](index.md 'index')

## DiGi\.GIS\.UI\.Windows Namespace
### Classes

<a name='DiGi.GIS.UI.Windows.Building2DWindow'></a>

## Building2DWindow Class

Interaction logic for Building2DWindow\.xaml

```csharp
public class Building2DWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') → Building2DWindow

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Windows.Building2DWindow.Building2DWindow()'></a>

## Building2DWindow\(\) Constructor

Initializes a new instance of the [Building2DWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.Building2DWindow 'DiGi\.GIS\.UI\.Windows\.Building2DWindow') class\.

```csharp
public Building2DWindow();
```

<a name='DiGi.GIS.UI.Windows.Building2DWindow.Building2DWindow(DiGi.GIS.Classes.Building2D)'></a>

## Building2DWindow\(Building2D\) Constructor

Initializes a new instance of the [Building2DWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.Building2DWindow 'DiGi\.GIS\.UI\.Windows\.Building2DWindow') class for the specified 2D building\.

```csharp
public Building2DWindow(DiGi.GIS.Classes.Building2D building2D);
```
#### Parameters

<a name='DiGi.GIS.UI.Windows.Building2DWindow.Building2DWindow(DiGi.GIS.Classes.Building2D).building2D'></a>

`building2D` [DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')

The [Building2D](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.Building2DWindow.Building2D 'DiGi\.GIS\.UI\.Windows\.Building2DWindow\.Building2D') entity to be associated with this window\.
### Properties

<a name='DiGi.GIS.UI.Windows.Building2DWindow.Building2D'></a>

## Building2DWindow\.Building2D Property

Gets or sets the [Building2D](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.Building2DWindow.Building2D 'DiGi\.GIS\.UI\.Windows\.Building2DWindow\.Building2D') entity associated with this window\.

```csharp
public DiGi.GIS.Classes.Building2D? Building2D { get; set; }
```

#### Property Value
[DiGi\.GIS\.Classes\.Building2D](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.building2d 'DiGi\.GIS\.Classes\.Building2D')
### Methods

<a name='DiGi.GIS.UI.Windows.Building2DWindow.InitializeComponent()'></a>

## Building2DWindow\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Windows.GISWindow'></a>

## GISWindow Class

Interaction logic for GISWindow\.xaml

```csharp
public class GISWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') → GISWindow

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Windows.GISWindow.GISWindow()'></a>

## GISWindow\(\) Constructor

Initializes a new instance of the [GISWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.GISWindow 'DiGi\.GIS\.UI\.Windows\.GISWindow') class\.

```csharp
public GISWindow();
```
### Methods

<a name='DiGi.GIS.UI.Windows.GISWindow.InitializeComponent()'></a>

## GISWindow\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Windows.OrtoDatasWindow'></a>

## OrtoDatasWindow Class

Interaction logic for OrtoDatasWindow\.xaml

```csharp
public class OrtoDatasWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') → OrtoDatasWindow

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Windows.OrtoDatasWindow.OrtoDatasWindow()'></a>

## OrtoDatasWindow\(\) Constructor

Initializes a new instance of the [OrtoDatasWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.OrtoDatasWindow 'DiGi\.GIS\.UI\.Windows\.OrtoDatasWindow') class\.

```csharp
public OrtoDatasWindow();
```
### Methods

<a name='DiGi.GIS.UI.Windows.OrtoDatasWindow.InitializeComponent()'></a>

## OrtoDatasWindow\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Windows.TypologyWindow'></a>

## TypologyWindow Class

Interaction logic for TypologyWindow\.xaml

```csharp
public class TypologyWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') → TypologyWindow

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Windows.TypologyWindow.TypologyWindow()'></a>

## TypologyWindow\(\) Constructor

Initializes a new instance of the [TypologyWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.TypologyWindow 'DiGi\.GIS\.UI\.Windows\.TypologyWindow') class\.

```csharp
public TypologyWindow();
```
### Methods

<a name='DiGi.GIS.UI.Windows.TypologyWindow.InitializeComponent()'></a>

## TypologyWindow\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')

<a name='DiGi.GIS.UI.Windows.YearBuiltsWindow'></a>

## YearBuiltsWindow Class

Interaction logic for YearBuiltsWindow\.xaml

```csharp
public class YearBuiltsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → [System\.Windows\.Threading\.DispatcherObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.threading.dispatcherobject 'System\.Windows\.Threading\.DispatcherObject') → [System\.Windows\.DependencyObject](https://learn.microsoft.com/en-us/dotnet/api/system.windows.dependencyobject 'System\.Windows\.DependencyObject') → [System\.Windows\.Media\.Visual](https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.visual 'System\.Windows\.Media\.Visual') → [System\.Windows\.UIElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.uielement 'System\.Windows\.UIElement') → [System\.ComponentModel\.ISupportInitialize](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.isupportinitialize 'System\.ComponentModel\.ISupportInitialize') → [System\.Windows\.FrameworkElement](https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkelement 'System\.Windows\.FrameworkElement') → [System\.Windows\.Controls\.Control](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.control 'System\.Windows\.Controls\.Control') → [System\.Windows\.Controls\.ContentControl](https://learn.microsoft.com/en-us/dotnet/api/system.windows.controls.contentcontrol 'System\.Windows\.Controls\.ContentControl') → [System\.Windows\.Window](https://learn.microsoft.com/en-us/dotnet/api/system.windows.window 'System\.Windows\.Window') → YearBuiltsWindow

Implements [System\.Windows\.Markup\.IComponentConnector](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector 'System\.Windows\.Markup\.IComponentConnector')
### Constructors

<a name='DiGi.GIS.UI.Windows.YearBuiltsWindow.YearBuiltsWindow()'></a>

## YearBuiltsWindow\(\) Constructor

Initializes a new instance of the [YearBuiltsWindow](DiGi.GIS.UI.Windows.md#DiGi.GIS.UI.Windows.YearBuiltsWindow 'DiGi\.GIS\.UI\.Windows\.YearBuiltsWindow') class\.

```csharp
public YearBuiltsWindow();
```
### Properties

<a name='DiGi.GIS.UI.Windows.YearBuiltsWindow.GISModelFile'></a>

## YearBuiltsWindow\.GISModelFile Property

Gets or sets the GIS model file associated with the Year Builts window\.

```csharp
public DiGi.GIS.Classes.GISModelFile? GISModelFile { get; set; }
```

#### Property Value
[DiGi\.GIS\.Classes\.GISModelFile](https://learn.microsoft.com/en-us/dotnet/api/digi.gis.classes.gismodelfile 'DiGi\.GIS\.Classes\.GISModelFile')
### Methods

<a name='DiGi.GIS.UI.Windows.YearBuiltsWindow.InitializeComponent()'></a>

## YearBuiltsWindow\.InitializeComponent\(\) Method

InitializeComponent

```csharp
public void InitializeComponent();
```

Implements [InitializeComponent\(\)](https://learn.microsoft.com/en-us/dotnet/api/system.windows.markup.icomponentconnector.initializecomponent 'System\.Windows\.Markup\.IComponentConnector\.InitializeComponent')