---
sidebar_position: 5
---

# Advanced Features

## GetObjectMapper Property

The `GetObjectMapper` property allows you to retrieve the `ObjectMapperAttribute` configuration of your mapped type at runtime. This is useful for inspecting the configuration or validating the mapping setup.

```csharp
[ObjectMapper(length: 50)]
public class Person
{
    [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
    public string Name { get; set; }
    
    [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
    public string Email { get; set; }
}

var spinner = new Spinner<Person>();
var config = spinner.GetObjectMapper;

if (config != null)
{
    Console.WriteLine($"Total length configured: {config.Length}");
}
```

## ReadFromSpan Method

For high-performance scenarios, you can use `ReadFromSpan` to parse positional strings directly from a `ReadOnlySpan<char>`, avoiding string allocations.

```csharp
[ObjectMapper(length: 50)]
public class Person
{
    [ReadProperty(start: 0, length: 20)]
    public string Name { get; set; }
    
    [ReadProperty(start: 20, length: 30)]
    public string Email { get; set; }
}

var spinner = new Spinner<Person>();

// Using ReadOnlySpan<char> for better performance
ReadOnlySpan<char> data = "John Doe            john.doe@example.com      ".AsSpan();
var person = spinner.ReadFromSpan(data);
```

:::tip Performance Benefit

`ReadFromSpan` is more efficient than `ReadFromString` because it avoids creating intermediate string objects. Use it when processing large volumes of data or in performance-critical scenarios.

:::

## Supported Data Types

Spinner automatically handles conversion for the following primitive types when reading from strings:

- **Numeric Types**: `int`, `long`, `short`, `ushort`, `uint`, `ulong`, `byte`, `sbyte`, `nint`, `nuint`
- **Floating Point**: `float`, `double`, `decimal`
- **Date/Time**: `DateTime`, `TimeSpan`
- **Other**: `string`, `char`, `bool`

```csharp
[ObjectMapper(length: 100)]
public class DataRecord
{
    [ReadProperty(start: 0, length: 10)]
    public int Id { get; set; }
    
    [ReadProperty(start: 10, length: 20)]
    public decimal Amount { get; set; }
    
    [ReadProperty(start: 30, length: 20)]
    public DateTime Date { get; set; }
    
    [ReadProperty(start: 50, length: 10)]
    public bool IsActive { get; set; }
}
```

## Padding Configuration

The `WritePropertyAttribute` supports two types of padding: **Left** and **Right**. By default, padding is applied to the left side.

### Left Padding (Default)

```csharp
[WriteProperty(length: 10, order: 1, paddingChar: '0', padding: PaddingType.Left)]
public string Code { get; set; }

// Example: Code = "123" => Output: "0000000123"
```

### Right Padding

```csharp
[WriteProperty(length: 20, order: 1, paddingChar: ' ', padding: PaddingType.Right)]
public string Name { get; set; }

// Example: Name = "John" => Output: "John                "
```

### Complete Example

```csharp
using Spinner.Enums;

[ObjectMapper(length: 30)]
public class Product
{
    [WriteProperty(length: 10, order: 1, paddingChar: '0', padding: PaddingType.Left)]
    public string Code { get; set; }
    
    [WriteProperty(length: 20, order: 2, paddingChar: ' ', padding: PaddingType.Right)]
    public string Name { get; set; }
}

var product = new Product { Code = "123", Name = "Widget" };
var spinner = new Spinner<Product>();
var result = spinner.WriteAsString(product);
// Output: "0000000123Widget              "
```

## Automatic Trimming

When reading from strings, Spinner automatically trims whitespace from the extracted values. This ensures clean data without manual processing.

```csharp
[ReadProperty(start: 0, length: 20)]
public string Name { get; set; }

// Input: "  John Doe         "
// Result: Name = "John Doe" (whitespace trimmed)
```

## String Truncation

If a property value exceeds the configured length during writing, it will be automatically truncated to fit the specified size.

```csharp
[WriteProperty(length: 10, order: 1, paddingChar: ' ')]
public string Name { get; set; }

var obj = new MyClass { Name = "VeryLongName" };
var spinner = new Spinner<MyClass>();
var result = spinner.WriteAsString(obj);
// Output: "VeryLongNa" (truncated to 10 characters)
```

:::warning Important

Always ensure your property values fit within the configured length to avoid data loss. Consider validating data before serialization.

:::

## Performance Optimizations

Spinner uses several optimizations for high-performance scenarios:

1. **ThreadStatic StringBuilder**: Reuses StringBuilder instances per thread to minimize allocations
2. **Span-based Processing**: Uses `ReadOnlySpan<char>` for efficient string parsing
3. **Compiled Delegates**: Creates compiled delegates for property getters/setters instead of reflection
4. **Static Caching**: Caches type metadata during static initialization

### Best Practices

- Use `WriteAsSpan` and `ReadFromSpan` for high-throughput scenarios
- Configure `ObjectMapper.Length` to match your exact needs
- Reuse `Spinner<T>` instances when possible (they are thread-safe for reading)
