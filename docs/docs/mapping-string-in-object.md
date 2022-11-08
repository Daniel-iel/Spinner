---
sidebar_position: 3
---

# Read String

## Mapping object in string

To configure an object, you should use ` ObjectMapper ` and ` ReadProperty ` properties to map string layout.

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

To map you object as string, you need instantiate ``` Spinner ``` passing the object type in T.

```csharp
  Spinner<NothingReader> spinnerReader = new Spinner<NothingReader>();
```

## Read from String

After configured the object, you need call the ` ReadFromString ` method to read string and convert it to object.

```csharp
  var obj = spinnerReader.ReadFromString("             spinner            www.spinner.com.br");
```
