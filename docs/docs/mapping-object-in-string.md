---
sidebar_position: 2
---

# Write Object

## Mapping object in string

To configure an object, utilize the `ObjectMapper` and `WriteProperty` properties for mapping the string layout.

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
```

:::info

The sum `length` of all mapped property should not be more than ObjectMapper `length`.

:::

## Padding Configuration

The `WriteProperty` attribute supports configuring padding direction using the `PaddingType` enum.

### Left Padding (Default)

```csharp
using Spinner.Enums;

[WriteProperty(length: 10, order: 1, paddingChar: '0', padding: PaddingType.Left)]
public string Code { get; set; }

// Example: Code = "123" => Output: "0000000123"
```

### Right Padding

```csharp
using Spinner.Enums;

[WriteProperty(length: 20, order: 1, paddingChar: ' ', padding: PaddingType.Right)]
public string Name { get; set; }

// Example: Name = "John" => Output: "John                "
```

:::tip

If the `padding` parameter is not specified, `PaddingType.Left` is used by default.

:::

## Instantiate

To map your object as a string, instantiate the `Spinner` class by providing the object type as 'T'.

```csharp
  Nothing nothing = new Nothing("spinner", "www.spinner.com.br");
  Spinner<Nothing> spinner = new Spinner<Nothing>();  
```

## Write an Object

Once the object is configured, you can call the `WriteAsString` method to write the object in string format.

```csharp
  string stringResponse = spinner.WriteAsString(nothing);
```

For better performance in high-throughput scenarios, use `WriteAsSpan`:

```csharp
  ReadOnlySpan<char> spanResponse = spinner.WriteAsSpan(nothing);
