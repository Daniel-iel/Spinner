---
sidebar_position: 1
---

# Getting Started

## Prerequisites

Spinner is compatible with the following versions of .NET: `.NET 7`, `.NET 6`, `.NET 5`.

:::info

dotnet version 3.1 support was discontinued in spinner version 2.

:::
   
## Installation

Spinner is distributed as a NuGet package. See the **[spinner](https://www.nuget.org/packages/Spinner/)** for more information.

```shell
dotnet add package Spinner
```

## Import Spinner

After installing the package, you'll need to import the Spinner class into your code.

```csharp
using Spinner;
```

## Configuring an Object

For a comprehensive understanding of how to configure your object, refer to the **[example](/docs/mapping-object-in-string)**

## Using Spinner

Execute **WriteAsString** to obtain the mapped object as a string, or use **WriteAsSpan** to retrieve the result as a span.

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
