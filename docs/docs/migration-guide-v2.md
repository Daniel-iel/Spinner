---
sidebar_position: 6
---

# Migration Guide: v1.x to v2.0

This guide will help you migrate from Spinner v1.x to v2.0. Version 2.0 introduces significant architectural changes that improve performance and simplify the API.

## Breaking Changes Overview

### 1. Class Type Change: `ref struct` ? `sealed class`

**v1.x:**

```csharp
public ref struct Spinner<T> where T : new()
```

**v2.0:**

```csharp
public sealed class Spinner<T> where T : new()
```

**Impact:** `Spinner<T>` is no longer a `ref struct`, which means:

- ? Can now be used as a field in classes
- ? Can be passed to async methods
- ? Can be stored in collections
- ? Better compatibility with modern C# features

### 2. Constructor Changes

**v1.x:**

```csharp
// Required object instance in constructor for writing
var obj = new MyClass("data", "value");
var spinner = new Spinner<MyClass>(obj);
var result = spinner.WriteAsString();

// Default constructor for reading
var spinner = new Spinner<MyClass>();
var obj = spinner.ReadFromString(data);
```

**v2.0:**

```csharp
// Single constructor, object passed to methods
var obj = new MyClass("data", "value");
var spinner = new Spinner<MyClass>();
var result = spinner.WriteAsString(obj);  // Object passed here

// Same instance for reading
var spinner = new Spinner<MyClass>();
var obj = spinner.ReadFromString(data);  // Returns object
```

**Migration Steps:**

1. Remove object from constructor
2. Pass object to `WriteAsString()` or `WriteAsSpan()` methods
3. Capture return value from `ReadFromString()` or `ReadFromSpan()` methods

### 3. Method Signature Changes

#### WriteAsString()

**v1.x:**

```csharp
public string WriteAsString()
```

**v2.0:**

```csharp
public string WriteAsString(T obj)
```

#### WriteAsSpan()

**v1.x:**

```csharp
public ReadOnlySpan<char> WriteAsSpan()
```

**v2.0:**
```csharp
public ReadOnlySpan<char> WriteAsSpan(T obj)
```

### 4. Removed Properties

The following properties have been removed in v2.0:

**v1.x:**

```csharp
public readonly IImmutableList<PropertyInfo> GetWriteProperties
public readonly IImmutableList<PropertyInfo> GetReadProperties
```

**v2.0:**

```csharp
// ? These properties no longer exist
// Use reflection directly if needed:
typeof(T).GetProperties().Where(p => 
    p.GetCustomAttribute<WritePropertyAttribute>() != null)
```

**Reason:** These properties were rarely used and added unnecessary overhead. The metadata is still available via reflection if needed.

### 5. Internal Implementation Changes

#### StringBuilder Management

**v1.x:**

```csharp
private readonly PooledStringBuilder _sb = PooledStringBuilder.GetInstance();
```

**v2.0:**

```csharp
[ThreadStatic]
private static StringBuilder builder;
```

**Impact:**

- Better thread-safety with `ThreadStatic`
- Reduced allocations through reuse
- No need for external pooling library

#### Property Caching

**v1.x:**

```csharp
private static readonly PropertyInfo[] WriteProperties
private static readonly PropertyInfo[] ReadProperties
```

**v2.0:**

```csharp
private static readonly (PropertyInfo PropertyInfo, WritePropertyAttribute Attribute, Func<T, string> Getter)[] WriteProperties;
private static readonly (PropertyInfo PropertyInfo, ReadPropertyAttribute Attribute, Action<T, string> Setter)[] ReadProperties;
```

**Impact:**

- ? Significantly faster property access via compiled delegates
- ?? No more reflection overhead during serialization/deserialization
- ?? Attributes cached to avoid repeated lookups

## Migration Examples

### Example 1: Writing Object to String

**v1.x:**
```csharp
[ObjectMapper(length: 50)]
public class Person
{
    [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
    public string Name { get; set; }
    
    [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
    public string Email { get; set; }
}

// Old way
var person = new Person { Name = "John", Email = "john@example.com" };
var spinner = new Spinner<Person>(person);
var result = spinner.WriteAsString();
```

**v2.0:**
```csharp
[ObjectMapper(length: 50)]
public class Person
{
    [WriteProperty(length: 20, order: 1, paddingChar: ' ')]
    public string Name { get; set; }
    
    [WriteProperty(length: 30, order: 2, paddingChar: ' ')]
    public string Email { get; set; }
}

// New way
var person = new Person { Name = "John", Email = "john@example.com" };
var spinner = new Spinner<Person>();
var result = spinner.WriteAsString(person);  // ? Pass object here
```

### Example 2: Reading String to Object

**v1.x:**
```csharp
var spinner = new Spinner<Person>();
var person = spinner.ReadFromString("John                john@example.com              ");
// person is now populated
```

**v2.0:**
```csharp
var spinner = new Spinner<Person>();
var person = spinner.ReadFromString("John                john@example.com              ");
// ? Same behavior, but cleaner API
```

### Example 3: Reusing Spinner Instance

**v1.x:**
```csharp
// Had to create new instance for each object
var person1 = new Person { Name = "John" };
var spinner1 = new Spinner<Person>(person1);
var result1 = spinner1.WriteAsString();

var person2 = new Person { Name = "Jane" };
var spinner2 = new Spinner<Person>(person2);  // New instance required
var result2 = spinner2.WriteAsString();
```

**v2.0:**
```csharp
// ? Can reuse same instance
var spinner = new Spinner<Person>();

var person1 = new Person { Name = "John" };
var result1 = spinner.WriteAsString(person1);

var person2 = new Person { Name = "Jane" };
var result2 = spinner.WriteAsString(person2);  // Same instance!
```

## Performance Improvements

Version 2.0 brings significant performance improvements:

| Feature | v1.x | v2.0 | Improvement |
|---------|------|------|-------------|
| Property Access | Reflection | Compiled Delegates | ~10-50x faster |
| StringBuilder | Pooled | ThreadStatic | Lower memory pressure |
| Attribute Lookup | Per operation | Cached on startup | ~100x faster |
| Type Conversion | Reflection | Direct parsing | ~5-10x faster |

:::tip Benchmark Results

For detailed benchmark comparisons showing **up to 79x faster** performance and **26x less** memory allocation, see the **[Performance Benchmarks](/docs/performance-benchmarks)** page.

:::

## New Features in v2.0

### 1. Compiled Delegates for Type Conversion

v2.0 automatically handles primitive type conversions without reflection:

```csharp
[ReadProperty(start: 0, length: 10)]
public int Age { get; set; }  // Automatically parsed from string to int

[ReadProperty(start: 10, length: 15)]
public decimal Salary { get; set; }  // Automatically parsed to decimal

[ReadProperty(start: 25, length: 20)]
public DateTime BirthDate { get; set; }  // Automatically parsed to DateTime
```

Supported types: `int`, `long`, `decimal`, `DateTime`, `bool`, `TimeSpan`, `byte`, `sbyte`, `short`, `ushort`, `uint`, `ulong`, `float`, `double`, `char`, `nint`, `nuint`

### 2. Improved Padding Control

```csharp
using Spinner.Enums;

[WriteProperty(length: 10, order: 1, paddingChar: '0', padding: PaddingType.Left)]
public string Code { get; set; }

[WriteProperty(length: 20, order: 2, paddingChar: ' ', padding: PaddingType.Right)]
public string Name { get; set; }
```

## Troubleshooting

### Compilation Error: Cannot convert from 'MyClass' to 'Spinner\<MyClass\>'

**Cause:** You're still using the v1.x constructor pattern.

**Fix:**

```csharp
// ? Old way
var spinner = new Spinner<MyClass>(obj);

// ? New way
var spinner = new Spinner<MyClass>();
var result = spinner.WriteAsString(obj);
```

### Compilation Error: "Spinner\<T>\ does not contain a definition for 'GetWriteProperties'"

**Cause:** The properties were removed in v2.0.

**Fix:**

```csharp
// ? Old way
var props = spinner.GetWriteProperties;

// ? New way - Use reflection directly if needed
var props = typeof(MyClass).GetProperties()
    .Where(p => p.GetCustomAttribute<WritePropertyAttribute>() != null)
    .ToList();
```

### Runtime Error: "Cannot use ref struct in async method"

**Cause:** This error should no longer occur in v2.0.

**Fix:** v2.0 uses a `sealed class` instead of `ref struct`, so this is no longer an issue. Your async code should work without changes.

## Recommended Upgrade Path

1. **Update NuGet Package**

   ```bash
   dotnet add package Spinner --version 2.0.0
   ```

2. **Find and Replace Constructor Calls**
   - Search: `new Spinner<(\w+)>\((\w+)\)`
   - Replace: `new Spinner<$1>()`
   - Then update method calls to pass the object

3. **Update Method Calls**
   - Change `spinner.WriteAsString()` to `spinner.WriteAsString(obj)`
   - Change `spinner.WriteAsSpan()` to `spinner.WriteAsSpan(obj)`

4. **Remove Property Dependencies**
   - Replace `GetWriteProperties` and `GetReadProperties` usage with direct reflection if needed

5. **Test Thoroughly**
   - Run your unit tests
   - Verify serialization/deserialization still produces expected results

## Benefits of Upgrading

? **Better Performance**: 10-100x faster in most scenarios  
? **Lower Memory Usage**: ThreadStatic StringBuilder reduces allocations  
? **Cleaner API**: More intuitive method signatures  
? **Better Compatibility**: Works with async/await, collections, and class fields  
? **Type Safety**: Compiled delegates catch type errors at startup  
? **Future-Proof**: Foundation for upcoming features  

## Support

If you encounter issues during migration:

- Check the [GitHub Issues](https://github.com/Daniel-iel/Spinner/issues)
- Review the [Documentation](https://spinnerframework.com/)
- Create a new issue with migration details

## Version History

- **v2.0.0** - Major architectural rewrite with breaking changes
- **v1.x.x** - Original ref struct implementation
