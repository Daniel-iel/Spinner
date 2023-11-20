---
sidebar_position: 3
---

# Read String

## Mapping object in string

To configure an object, utilize the `ObjectMapper` and `ReadProperty` properties for mapping the string layout.

```csharp
  [ObjectMapper(length: 50)]
  public class NothingReader
  {
    [ReadProperty(start:1, length: 19 )]        
    public string Name { get; private set; }

    [ReadProperty(start: 20, length: 30)]        
    public string WebSite { get; private set; }
  }
```

## Instantiate

To map your object as a string, instantiate the `Spinner` class, specifying the object type with T

```csharp
  Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();
```

## Read from String

After configuring the object, you need to call the `ReadFromString` method to read a string and convert it to an object.

```csharp
  var obj = spinnerReader.ReadFromString("             spinner            www.spinner.com.br");
```
