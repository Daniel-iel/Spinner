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

* Swift Writing Speed.
* Effortlessly Transforms Objects into Mapped Strings.
* Seamlessly Converts Mapped Strings back into Objects.

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
 var spinner = new Spinner<Nothing>(nothing);
 var stringResponse = spinner.WriteAsString();   
 // stringResponse = "              spinner            www.spinner.com.br"
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
