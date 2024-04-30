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
[![Visitors](https://visitor-badge-reloaded.herokuapp.com/badge?page_id=https://github.com/Daniel-iel/Spinner&logo=github)
[![Release](https://img.shields.io/nuget/v/spinner)](https://www.nuget.org/packages/Spinner/)
[![Repo Size](https://img.shields.io/github/repo-size/Daniel-iel/spinner)](https://www.nuget.org/packages/Spinner/)
[![Code Factor](https://www.codefactor.io/repository/github/Daniel-iel/spinner/badge)](https://www.codefactor.io/repository/github/Daniel-iel/spinner)
[![coverity](https://img.shields.io/coverity/scan/24116.svg)](https://scan.coverity.com/projects/spinner)
[![Code Factor](https://api.meercode.io/badge/Daniel-iel/Spinner?type=ci-score&lastDay=31)](https://scan.coverity.com/projects/spinner)
[![Snyk](https://img.shields.io/snyk/vulnerabilities/github/Daniel-iel/Spinner)](https://scan.coverity.com/projects/spinner)

## Introduction

Spinner is a simple object mapper, it’s useful to communicate to any system that uses a positional string as communication.

### Spinner provides features

* Fast write.
* Convert object to a mapped string.
* Convert string to a mapped object.
* Convert string property into any type using interceptor.

## Quick Start

### Installation

```csharp
dotnet add package Spinner
```

### Usage

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

See Learn: Getting Started for setting up your project [here](https://spinnerframework.com/).

## Contributors

[![Daniel-Profile](https://github.com/Daniel-iel.png?size=40)](https://github.com/Daniel-iel)

## Support

All donation are welcome, you can donate to the Spinner project, we gonna use to maintain spinner domain and ssl certificate only.

## License

Our code and framework are licensed under the MIT license. Please see the license file for more information. You can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.

# Stats

[![Repobeats analytics image](https://repobeats.axiom.co/api/embed/c3f5ed375e6e703c23a90745aaee5bca46ebd0fd.svg)]
