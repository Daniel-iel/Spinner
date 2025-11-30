---
sidebar_position: 4
---

# Interceptors

Interceptors allow you to transform values during the reading process. This is useful when you need to parse custom formats, apply business logic, or transform data before it's assigned to a property.

## What are Interceptors?

When reading positional strings, Spinner automatically converts values to common types like `int`, `decimal`, `DateTime`, etc. However, sometimes you need custom transformation logic. That's where interceptors come in.

## Creating an Interceptor

To create an interceptor, implement the `IInterceptor<T>` interface where `T` is the return type:

```csharp
using Spinner.Interceptors;

public class WebSiteInterceptor : IInterceptor<string>
{
    public string Parse(string value)
    {
        // Add "https://" prefix if not present
        if (!value.StartsWith("http://") && !value.StartsWith("https://"))
        {
            return $"https://{value.Trim()}";
        }
        return value.Trim();
    }
}
```

## Using Interceptors

Apply the interceptor to a property using the `type` parameter in `ReadProperty`:

```csharp
[ObjectMapper(length: 50)]
public class WebSite
{
    [ReadProperty(start: 0, length: 20)]
    public string Name { get; set; }
    
    [ReadProperty(start: 20, length: 30, type: typeof(WebSiteInterceptor))]
    public string Url { get; set; }
}

// Usage
var spinner = new Spinner<WebSite>();
var data = "MyWebSite           example.com                   ";
var result = spinner.ReadFromString(data);

// result.Name = "MyWebSite"
// result.Url = "https://example.com"
```

## Common Use Cases

### Currency Formatting

```csharp
public class CurrencyInterceptor : IInterceptor<decimal>
{
    public decimal Parse(string value)
    {
        // Remove currency symbols and parse
        var trimmed = value.Trim().Replace("$", "").Replace(",", "");
        return decimal.Parse(trimmed);
    }
}

[ReadProperty(start: 0, length: 15, type: typeof(CurrencyInterceptor))]
public decimal Price { get; set; }
```

### Custom Date Formats

```csharp
public class CustomDateInterceptor : IInterceptor<DateTime>
{
    public DateTime Parse(string value)
    {
        // Parse custom format: YYYYMMDD
        return DateTime.ParseExact(value.Trim(), "yyyyMMdd", null);
    }
}

[ReadProperty(start: 0, length: 8, type: typeof(CustomDateInterceptor))]
public DateTime Date { get; set; }
```

### Boolean Mapping

```csharp
public class YesNoInterceptor : IInterceptor<bool>
{
    public bool Parse(string value)
    {
        var normalized = value.Trim().ToUpper();
        return normalized == "Y" || normalized == "YES" || normalized == "1";
    }
}

[ReadProperty(start: 0, length: 3, type: typeof(YesNoInterceptor))]
public bool IsActive { get; set; }
```

## Performance Considerations

- Interceptors are cached per type for optimal performance
- The `Parse` method is called once per property during deserialization
- Keep interceptor logic simple and fast for best results

:::tip Best Practice

Only use interceptors when you need custom transformation logic. For standard types (`int`, `decimal`, `DateTime`, etc.), Spinner's built-in converters are faster and don't require interceptors.

:::

## When NOT to Use Interceptors

You don't need interceptors for these scenarios (they're handled automatically):

- Converting strings to `int`, `long`, `decimal`, etc.
- Parsing standard `DateTime` formats
- Converting to `bool` from "true"/"false"
- Any primitive type conversion

:::info

For a complete list of automatically supported types, see the **[Advanced Features](/docs/advanced-features#supported-data-types)** page.

:::
