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
    [ReadProperty(start:0, length: 19)]        
    public string Name { get; set; }

    [ReadProperty(start: 20, length: 30)]        
    public string WebSite { get; set; }
  }
```

:::info

Position indexing starts at 0. Make sure your `start` and `length` values correctly match your string layout.

:::

:::info ObjectMapper Length

While `ObjectMapper` helps document the expected string length, it's not strictly enforced during reading. The `start` and `length` parameters in `ReadProperty` determine what portion of the string is extracted.

:::

## Supported Data Types

Spinner automatically converts string values to the following types without requiring interceptors:

- **Numeric**: `int`, `long`, `short`, `ushort`, `uint`, `ulong`, `byte`, `sbyte`
- **Native Integers**: `nint`, `nuint`
- **Floating Point**: `float`, `double`, `decimal`
- **Date/Time**: `DateTime`, `TimeSpan`
- **Other**: `string`, `char`, `bool`

```csharp
[ObjectMapper(length: 100)]
public class DataRecord
{
    [ReadProperty(start: 0, length: 10)]
    public int Id { get; set; }
    
    [ReadProperty(start: 10, length: 15)]
    public decimal Price { get; set; }
    
    [ReadProperty(start: 25, length: 20)]
    public DateTime Date { get; set; }
    
    [ReadProperty(start: 45, length: 5)]
    public bool IsActive { get; set; }
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
  var obj = spinnerReader.ReadFromString("spinner            www.spinner.com.br");
```

## Read from Span (High Performance)

For better performance, especially when processing large volumes of data, use `ReadFromSpan` with `ReadOnlySpan<char>`:

```csharp
  ReadOnlySpan<char> data = "spinner            www.spinner.com.br".AsSpan();
  var obj = spinnerReader.ReadFromSpan(data);
```

:::tip Performance

`ReadFromSpan` avoids string allocations and is recommended for performance-critical scenarios.

:::
