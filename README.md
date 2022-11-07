<div align="center">
  <img src="/assets/logo.png?raw=true">
</div>

#

 <a href="https://github.com/Daniel-iel/Spinner/graphs/contributors" alt="Contributors">
    <img src="https://img.shields.io/github/contributors/Daniel-iel/Spinner" />
</a>
<a href="https://github.com/Daniel-iel/Spinner/commits/main" alt="Total Commits">
    <img src="https://img.shields.io/github/commit-activity/m/Daniel-iel/Spinner/main" />
</a>  
<a href="https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml">
    <img src="https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml/badge.svg" alt="build status">
</a>
<a href="https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml">
    <img src="https://github.com/Daniel-iel/Spinner/actions/workflows/ci-documentation.yml/badge.svg" alt="build status">
</a>
<a href="https://github.com/Daniel-iel/Spinner/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/Daniel-iel/Spinner" alt="License"/>
</a>
<a href="https://www.nuget.org/packages/Spinner">
    <img src="https://img.shields.io/nuget/dt/Spinner" alt="Nuget"/>
</a>  
<a href="#">
  <img alt="views" title="Views" src="https://visitor-badge-reloaded.herokuapp.com/badge?page_id=https://github.com/Daniel-iel/Spinner&logo=github"/>
</a>
<a href="#">
  <img alt="Release" title="Release" src="https://img.shields.io/nuget/v/spinner"/>
</a>
<a href="#">
  <img alt="Repo Size" title="Repo Size" src="https://img.shields.io/github/repo-size/Daniel-iel/spinner"/>
</a>
<a href="https://www.codefactor.io/repository/github/Daniel-iel/spinner">
  <img alt="" title="" src="https://www.codefactor.io/repository/github/Daniel-iel/spinner/badge"/>  
</a>
<a href="https://codecov.io/gh/Daniel-iel/Spinner">
  <img alt="Coverity Scan Build Status" src="https://codecov.io/gh/Daniel-iel/Spinner/branch/main/graph/badge.svg?token=0DO0Z5CA6N/">
</a>
<a href="https://scan.coverity.com/projects/spinner">
  <img alt="Coverity Scan Build Status"
       src="https://img.shields.io/coverity/scan/24116.svg"/>
</a>
<a href="">
  <img alt="" src="https://api.meercode.io/badge/Daniel-iel/Spinner?type=ci-score&lastDay=31"/>
</a>

<br />

### Introduction

Spinner is a simple object mapper, it’s useful to communicate to any system that uses a positional string as communication.

### Spinner provides features

* Fast write.
* Convert object to a mapped string.
* Convert string to a mapped object.

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

<a href="https://github.com/Daniel-iel">
  <img src="https://github.com/Daniel-iel.png?size=40" width="40" style="border-radius: 100%" />
</a>

## Support

All donation are welcome, you can donate to the Spinner project, we gonna use to maintain spinner domain and ssl certificate only.

## License

Our code and framework are licensed under the MIT license. Please see the license file for more information. You can do whatever you want as long as you include the original copyright and license notice in any copy of the software/source.

# Stats

<img src="https://repobeats.axiom.co/api/embed/c3f5ed375e6e703c23a90745aaee5bca46ebd0fd.svg" alt="Repobeats analytics image" />
