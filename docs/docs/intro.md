---
sidebar_position: 1
---

# Tutorial Intro

Let's discover **Spinner in less than 5 minutes**.

## Getting Started

Get started by **configuring you project**.

## Installing Spinner package

To install spinner in your project using **Nuget Package**:

```shell
dotnet add package Spinner
```

## Mapping your model

```csharp
[ObjectMapper(lenght: 50)]
public struct Nothing
{
  public Nothing(string name, string site)
  {
    this.Name = name;
    this.Site = site;
  }
  
  [WriteProperty(lenght: 20, order: 1, paddingChar: ' ')]
  public string Name { get; private set; }
  
  [WriteProperty(lenght: 30, order: 2, paddingChar: ' ')]
  public string Site { get; private set; }
}
```

obs: `length` is the maximum length of the property and should not more than ObjectMapper `length`.

## Using spinner

Run **WriteAsString** to get mapped object as string or call **WriteAsSpan** to get the result as span:

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var stringResponse = spinner.WriteAsString();   
 //stringresponse = "              spinner            www.spinner.com.br"
```

```csharp
 var nothing = new Nothing("spinner", "www.spinner.com.br");
 var spinner = new Spinner<Nothing>(nothing);
 var spanResponse = spinner.WriteAsSpan();   
 //spanResponse = "              spinner            www.spinner.com.br"
```
