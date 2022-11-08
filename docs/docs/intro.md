---
sidebar_position: 1
---

# Get started

## Prerequisites

Spinner is only compatible with **dotnet core 3.0, 3.1** and  **dotnet 5.0, 6.0.**

## Installation

Spinner is distributed as a NuGet package. See the **[spinner](https://www.nuget.org/packages/Spinner/)** for more information.

```shell
dotnet add package Spinner
```

## Import Spinner

After install the package, you need import Spinner class into your class.

```csharp
using Spinner;
```

## Configuring an Object

To have a good knowledge to how configure your object, you can see the **[example](/docs/mapping-object-in-string)**

## Using Spinner

Run **WriteAsString** to get mapped object as string or call **WriteAsSpan** to get the result as span:

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var stringResponse = spinner.WriteAsString();   
 //output: "              spinner            www.spinner.com.br"
```

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var spanResponse = spinner.WriteAsSpan();   
 //output: "              spinner            www.spinner.com.br"
```
