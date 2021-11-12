<p class="text-align: center;">
  <img src="/Assets/svgexport-6.png?raw=true">
</p>

# Spinner
         
 <a href="https://github.com/SpinnerAlloc/Spinner/graphs/contributors" alt="Contributors">
    <img src="https://img.shields.io/github/contributors/SpinnerAlloc/Spinner" />
</a>
<a href="https://github.com/SpinnerAlloc/Spinner/commits/main" alt="Total Commits">
    <img src="https://img.shields.io/github/commit-activity/m/SpinnerAlloc/Spinner/main" />
</a>  
<a href="https://github.com/SpinnerAlloc/Spinner/actions/workflows/ci.yml">
    <img src="https://github.com/SpinnerAlloc/Spinner/actions/workflows/ci.yml/badge.svg" alt="build status">
</a>             
<a href="https://lgtm.com/projects/g/SpinnerAlloc/Spinner/alerts/">
    <img src="https://img.shields.io/lgtm/alerts/g/badges/shields" alt="Total alerts"/>
</a>  
<a href="https://github.com/SpinnerAlloc/Spinner/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/SpinnerAlloc/Spinner" alt="License"/>
</a>
<a href="https://www.nuget.org/packages/Spinner">
    <img src="https://img.shields.io/nuget/dt/Spinner" alt="Nuget"/>
</a>  

### 🕮 Introduction 
Spinner is a simple object mapper, it’s useful to communicate to any system that uses a positional string as communication, for example,  integrations with the mainframe.

### ♨️Spinner provides features:
* Fast write.
* Convert object to a mapped string.
* Convert string to a mapped object(commig soon).

## Quick Start

### Installation

```csharp
dotnet add package Spinner
```

### Usage

```csharp
[ContextProperty(lenght: 50)]
public struct Nothing
{
  public Nothing(string name, string adress)
  {
    this.Name = name;
    this.Adress = adress;
  }
  
  [WriteProperty(lenght: 20, order: 1, paddingChar: ' ')]
  public string Name { get; private set; }
  
  [WriteProperty(lenght: 30, order: 2, paddingChar: ' ')]
  public string Adress { get; private set; }
}
    
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var stringResponse = spinner.WriteAsString();   
 // stringresponse = "              spinner            www.spinner.com.br   "
```

## Documentation
See Learn: Getting Started for setting up your project [here](https://spinneralloc.github.io/Spinner/).

## Contributors
<a href="https://github.com/Daniel-iel"><img src="https://github.com/Daniel-iel.png?size=40" width="40" class="border-radius: 100%" /></a>

## License
Our code and framework are licensed under the MIT licence. Please see the licence file for more information. You can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.



















