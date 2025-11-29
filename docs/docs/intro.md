---
sidebar_position: 1
---

# Getting Started

Spinner is a high-performance library for converting objects to/from positional (fixed-width) strings. With version 2.0, Spinner delivers **up to 79x faster** performance and **26x less** memory allocation compared to v1.x.

## Prerequisites

Spinner is compatible with the following versions of .NET: `.NET 7`, `.NET 6`, `.NET 5`.

:::info

dotnet version 3.1 support was discontinued in spinner version 2.

:::

:::tip Performance

Version 2.0 introduces major performance improvements:
- **60x faster** read operations
- **31x faster** write operations  
- **Up to 26x less** memory allocation

See the **[Performance Benchmarks](/docs/performance-benchmarks)** page for detailed results.

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

### Writing Objects to Strings

Execute **WriteAsString** to obtain the mapped object as a string, or use **WriteAsSpan** to retrieve the result as a span.

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>();
 var stringResponse = spinner.WriteAsString(nothing);   
 //output: "              spinner            www.spinner.com.br"
```

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>();
 var spanResponse = spinner.WriteAsSpan(nothing);   
 //output: "              spinner            www.spinner.com.br"
```

### Reading Strings to Objects

Use **ReadFromString** to parse a positional string into an object, or **ReadFromSpan** for better performance with span-based processing.

```csharp
 var spinner = new Spinner<NothingReader>();
 var obj = spinner.ReadFromString("              spinner            www.spinner.com.br");
 //obj.Name = "spinner"
 //obj.WebSite = "www.spinner.com.br"
```

```csharp
 var spinner = new Spinner<NothingReader>();
 ReadOnlySpan<char> data = "              spinner            www.spinner.com.br".AsSpan();
 var obj = spinner.ReadFromSpan(data);
 //obj.Name = "spinner"
 //obj.WebSite = "www.spinner.com.br"
```

## Next Steps

- Learn about **[Writing Objects](/docs/mapping-object-in-string)**
- Learn about **[Reading Strings](/docs/mapping-string-in-object)**
- Explore **[Interceptors](/docs/mapping-string-into-type)** for custom value processing
- Check out **[Advanced Features](/docs/advanced-features)** for performance optimizations and additional capabilities
