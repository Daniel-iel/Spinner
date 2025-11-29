<div align="center">
  <img src="./assets/logo.png?raw=true">
</div>

#

[![Contributors](https://img.shields.io/github/contributors/Daniel-iel/Spinner)](https://www.nuget.org/packages/Spinner/)
[![Activity](https://img.shields.io/github/commit-activity/m/Daniel-iel/Spinner)](https://www.nuget.org/packages/Spinner/)
[![ci](https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml/badge.svg/)
[![documentation](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-documentation.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-documentation.yml/badge.svg/)
[![ci](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-benchmark.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-benchmark.yml/badge.svg/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![Downloads](https://img.shields.io/nuget/dt/Spinner)](https://www.nuget.org/packages/Spinner/)
[![Release](https://img.shields.io/nuget/v/spinner)](https://www.nuget.org/packages/Spinner/)
[![Repo Size](https://img.shields.io/github/repo-size/Daniel-iel/spinner)](https://www.nuget.org/packages/Spinner/)
[![Code Factor](https://www.codefactor.io/repository/github/Daniel-iel/spinner/badge)](https://www.codefactor.io/repository/github/Daniel-iel/spinner)
![Meercode](https://api.meercode.io/badge//?type=ci-score&lastDay=90)
![Snyk](https://img.shields.io/snyk/vulnerabilities/github/Daniel-iel/Spinner)

## Introduction

Spinner is a versatile and efficient tool designed to streamline data interchange between applications. With its intuitive object mapping capabilities, Spinner excels in translating complex information into a format that can be easily understood and processed by a wide range of systems.

Moreover, Spinner boasts a user-friendly interface that empowers developers to effortlessly configure mappings, reducing the time and effort required to integrate disparate systems. Its adaptability extends to various data types, allowing for precise mapping of objects, arrays, and other complex structures.

### Features

* **? Blazing Fast Performance** - Up to 79x faster than v1.x with optimized compiled delegates
* **?? Memory Efficient** - Up to 26x less memory allocation with ThreadStatic StringBuilder
* **?? High-Throughput Ready** - Process millions of records in milliseconds
* **?? Zero-Allocation Scenarios** - Near-zero allocation with Span-based operations
* **?? Seamless Conversions** - Effortlessly transform objects to/from positional strings
* **?? Type-Safe Parsing** - Built-in support for 18+ primitive types (int, decimal, DateTime, etc.)
* **?? Configurable Padding** - Left/Right padding with custom characters
* **?? Custom Interceptors** - Extensible value transformation pipeline

### Performance Highlights

| Operation | v1.x | v2.0 | Improvement |
|-----------|------|------|-------------|
| **ReadFromString** | 2.8 µs | 46 ns | **60x faster** ? |
| **WriteAsString** | 1.2 µs | 40 ns | **31x faster** ? |
| **Memory Usage** | 664-808 B | 32-128 B | **6-20x less** ?? |

*Benchmarks run on .NET 10.0 with BenchmarkDotNet. See [detailed benchmarks](https://spinnerframework.com/docs/performance-benchmarks) for more information.*

## Quick Start

### Installation

```csharp
dotnet add package Spinner
```

### Usage

To accurately represent the final string, instantiate an object. Note that utilizing the WriteProperty attribute is essential for this process.

```csharp
[ObjectMapper(length: 50)]
public struct Nothing
{
  public Nothing(string name, string webSite)
  {
    this.Name = name;
    this.WebSite = webSite;
  }
  
  [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
  public string Name { get; private set; }
  
  [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
  public string WebSite { get; private set; }
}
    
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>();
 var stringResponse = spinner.WriteAsString(nothing);   
 // stringResponse = "              spinner            www.spinner.com.br"
```

### Reading from Strings

```csharp
[ObjectMapper(length: 50)]
public class NothingReader
{
  [ReadProperty(start: 0, length: 20)]        
  public string Name { get; set; }

  [ReadProperty(start: 20, length: 30)]        
  public string WebSite { get; set; }
}

var spinner = new Spinner<NothingReader>();
var obj = spinner.ReadFromString("              spinner            www.spinner.com.br");
// obj.Name = "spinner"
// obj.WebSite = "www.spinner.com.br"
```

## Documentation

Refer to 'Learn: Getting Started' for comprehensive instructions on setting up your project. [here](https://spinnerframework.com/).

## Contributors

[![Daniel-Profile](https://github.com/Daniel-iel.png?size=40)](https://github.com/Daniel-iel)

## Support

We appreciate any donations. Your contributions to the Spinner project will be used exclusively for maintaining the Spinner domain and SSL certificate.

## License

Our code and framework are licensed under the MIT license. Please see the license file for more information. You can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.

# Stats

![Repobeats analytics image](https://repobeats.axiom.co/api/embed/c3f5ed375e6e703c23a90745aaee5bca46ebd0fd.svg)
