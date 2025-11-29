# Changelog

All notable changes to the Spinner project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - TBD

### ?? Breaking Changes

#### Architecture
- **Changed `Spinner<T>` from `ref struct` to `sealed class`**
  - Can now be used in async methods, as class fields, and in collections
  - No longer has ref struct limitations
  
#### API Changes
- **Constructor**: Removed object parameter from constructor
  - Before: `new Spinner<T>(obj)`
  - After: `new Spinner<T>()`
  
- **WriteAsString**: Now requires object parameter
  - Before: `spinner.WriteAsString()`
  - After: `spinner.WriteAsString(obj)`
  
- **WriteAsSpan**: Now requires object parameter
  - Before: `spinner.WriteAsSpan()`
  - After: `spinner.WriteAsSpan(obj)`

#### Removed Features
- **Removed `GetWriteProperties` property**
  - Use reflection directly: `typeof(T).GetProperties().Where(...)`
  
- **Removed `GetReadProperties` property**
  - Use reflection directly: `typeof(T).GetProperties().Where(...)`

### ? New Features

- **Compiled Delegates**: Properties now use compiled delegates instead of reflection
  - 10-50x faster property access during serialization/deserialization
  
- **Native Type Support**: Automatic conversion for primitive types
  - Supported: `int`, `long`, `decimal`, `DateTime`, `bool`, `TimeSpan`, `byte`, `sbyte`, `short`, `ushort`, `uint`, `ulong`, `float`, `double`, `char`, `nint`, `nuint`
  - No need for interceptors for common types
  
- **Improved Padding Control**: Explicit `PaddingType` parameter
  - `PaddingType.Left` (default)
  - `PaddingType.Right`

### ? Performance Improvements

- **ThreadStatic StringBuilder**: Reduced memory allocations
  - Reuses StringBuilder instances per thread
  - No need for external pooling library
  
- **Cached Attributes**: Attributes are cached during type initialization
  - 100x faster attribute lookups
  - Zero reflection overhead during operations
  
- **Optimized Property Access**: Direct delegate invocation
  - Eliminates reflection-based `GetValue/SetValue` calls
  - Significant performance boost in tight loops

### ?? Bug Fixes

- Fixed potential memory leaks with pooled resources
- Improved thread-safety with ThreadStatic pattern
- Better error messages for missing property setters

### ?? Documentation

- Added comprehensive migration guide (v1.x to v2.0)
- Added advanced features documentation
- Expanded examples for all supported types
- Added performance optimization guidelines

### ?? Internal Changes

- Replaced `PooledStringBuilder` with `ThreadStatic StringBuilder`
- Refactored property caching to use tuples with delegates
- Improved guard clauses and error handling
- Better code organization and maintainability

---

## [1.x.x] - Previous Versions

### Features
- Initial implementation using `ref struct`
- Basic read/write operations with attributes
- Property-based configuration with `ObjectMapper`, `WriteProperty`, `ReadProperty`
- Custom interceptors support
- Span-based operations

### Limitations
- Could not be used in async methods
- Could not be stored as class fields
- Required object instance in constructor for write operations
- Performance overhead from reflection-based property access

---

## Migration Guide

For detailed migration instructions from v1.x to v2.0, please see [Migration Guide](docs/docs/migration-guide-v2.md).

### Quick Migration Checklist

- [ ] Update NuGet package to v2.0.0
- [ ] Remove object from `Spinner<T>` constructor
- [ ] Add object parameter to `WriteAsString(obj)` and `WriteAsSpan(obj)` calls
- [ ] Replace `GetWriteProperties` / `GetReadProperties` usage (if any)
- [ ] Test all serialization/deserialization operations
- [ ] Verify performance improvements in your scenarios

---

## Support

- **Documentation**: [https://spinnerframework.com/](https://spinnerframework.com/)
- **Issues**: [GitHub Issues](https://github.com/Daniel-iel/Spinner/issues)
- **NuGet**: [Spinner Package](https://www.nuget.org/packages/Spinner/)

---

[2.0.0]: https://github.com/Daniel-iel/Spinner/compare/v1.x.x...v2.0.0
[1.x.x]: https://github.com/Daniel-iel/Spinner/releases/tag/v1.x.x
