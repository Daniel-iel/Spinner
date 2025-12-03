<div align="center">
  <img src="./assets/logo.png?raw=true">
</div>

<div align="center">

[![Contributors](https://img.shields.io/github/contributors/Daniel-iel/Spinner)](https://github.com/Daniel-iel/Spinner/graphs/contributors)
[![Activity](https://img.shields.io/github/commit-activity/m/Daniel-iel/Spinner)](https://github.com/Daniel-iel/Spinner/graphs/commit-activity)
[![CI](https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci.yml)
[![Documentation](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-documentation.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-documentation.yml)
[![Benchmarks](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-benchmark.yml/badge.svg)](https://github.com/Daniel-iel/Spinner/actions/workflows/ci-benchmark.yml)

[![NuGet](https://img.shields.io/nuget/v/spinner)](https://www.nuget.org/packages/Spinner/)
[![Downloads](https://img.shields.io/nuget/dt/Spinner)](https://www.nuget.org/packages/Spinner/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.md)
[![Code Factor](https://www.codefactor.io/repository/github/Daniel-iel/spinner/badge)](https://www.codefactor.io/repository/github/Daniel-iel/spinner)
[![Known Vulnerabilities](https://snyk.io/test/github/daniel-iel/spinner/badge.svg)](https://snyk.io/test/github/daniel-iel/spinner)
[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-support-yellow?style=flat&logo=buy-me-a-coffee)](https://buymeacoffee.com/danieliel)

[Documentation](https://spinnerframework.com) | [Getting Started](https://spinnerframework.com/docs/intro) | [API Reference](https://spinnerframework.com/docs/advanced-features)

</div>

---

## 🎯 Introduction

**Spinner** is a high-performance .NET library for converting objects to and from positional (fixed-width) strings. Designed for scenarios where you need to work with legacy file formats, mainframe data, or any system that uses fixed-position text records.

### Why Spinner?

* **⚡ Blazing Fast** - Up to **79x faster** than previous versions
* **💾 Memory Efficient** - Up to **26x less** memory allocation
* **🎯 Type-Safe** - Compile-time validation with attributes
* **🔧 Easy to Use** - Simple, intuitive API
* **🧪 Battle-Tested** - High test coverage with mutation testing
* **📦 Zero Dependencies** - Lightweight and self-contained

### Features

* **? Blazing Fast Performance** - Up to 79x faster than v1.x with optimized compiled delegates
* **?? Memory Efficient** - Up to 26x less memory allocation with ThreadStatic StringBuilder
* **?? High-Throughput Ready** - Process millions of records in milliseconds
* **?? Zero-Allocation Scenarios** - Near-zero allocation with Span-based operations
* **?? Seamless Conversions** - Effortlessly transform objects to/from positional strings
* **?? Type-Safe Parsing** - Built-in support for 18+ primitive types (int, decimal, DateTime, etc.)
* **?? Configurable Padding** - Left/Right padding with custom characters
* **?? Custom Interceptors** - Extensible value transformation pipeline

### Performance Highlights

| Operation | v1.x | v2.0 | Improvement |
|-----------|------|------|-------------|
| **ReadFromString** | 2.8 �s | 46 ns | **60x faster** ? |
| **WriteAsString** | 1.2 �s | 40 ns | **31x faster** ? |
| **Memory Usage** | 664-808 B | 32-128 B | **6-20x less** ?? |

*Benchmarks run on .NET 10.0 with BenchmarkDotNet. See [detailed benchmarks](https://spinnerframework.com/docs/performance-benchmarks) for more information.*

## 📋 Use Cases

Spinner is perfect for:

* **Legacy System Integration** - Parse and generate mainframe-style fixed-width files
* **EDI Processing** - Handle Electronic Data Interchange formats
* **Banking & Finance** - Process CNAB, FEBRABAN, and other financial formats
* **Government Systems** - Work with standardized fixed-position government data
* **Data Migration** - Convert between legacy and modern data formats
* **Report Generation** - Create fixed-width reports for legacy systems

## 🚀 Quick Start

### Installation

Install via NuGet Package Manager:

```bash
dotnet add package Spinner
```

Or via Package Manager Console:

```powershell
Install-Package Spinner
```

### Basic Usage - Writing Objects

Transform objects into positional strings:

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
 var spinner = new Spinner<Nothing>();
 var stringResponse = spinner.WriteAsString(nothing);   
 // Output: "              spinner            www.spinner.com.br"
```

### Basic Usage - Reading Strings

Parse positional strings into objects:

```csharp
[ObjectMapper(length: 50)]
public class NothingReader
{
  [ReadProperty(start: 0, length: 20)]        
  public string Name { get; set; }

  [ReadProperty(start: 20, length: 30)]        
  public string WebSite { get; set; }
}

var spinner = new Spinner<NothingReader>();
var obj = spinner.ReadFromString("              spinner            www.spinner.com.br");
// obj.Name = "spinner"
// obj.WebSite = "www.spinner.com.br"
```

### Advanced Features

```csharp
// Use Span for better performance
ReadOnlySpan<char> data = input.AsSpan();
var obj = spinner.ReadFromSpan(data);

// Custom padding
[WriteProperty(length: 10, order: 1, paddingChar: '0', padding: PaddingType.Left)]
public string Code { get; set; }  // "123" → "0000000123"

// Automatic type conversion
[ReadProperty(start: 0, length: 10)]
public int Age { get; set; }  // Automatically parses string to int

[ReadProperty(start: 10, length: 20)]
public DateTime BirthDate { get; set; }  // Automatically parses to DateTime
```

For more examples, see the [full documentation](https://spinnerframework.com/docs/intro).

## 📚 Documentation

For comprehensive documentation, visit our official website: [spinnerframework.com](https://spinnerframework.com/)

### Quick Links

* **[Getting Started](https://spinnerframework.com/docs/intro)** - Installation and basic usage
* **[Writing Objects](https://spinnerframework.com/docs/mapping-object-in-string)** - Convert objects to positional strings
* **[Reading Strings](https://spinnerframework.com/docs/mapping-string-in-object)** - Parse positional strings to objects
* **[Interceptors](https://spinnerframework.com/docs/mapping-string-into-type)** - Custom value transformations
* **[Advanced Features](https://spinnerframework.com/docs/advanced-features)** - Thread-safety, padding, and optimizations
* **[Migration Guide](https://spinnerframework.com/docs/migration-guide-v2)** - Upgrading from v1.x to v2.0
* **[Performance Benchmarks](https://spinnerframework.com/docs/performance-benchmarks)** - Detailed performance analysis

## Contributing

We welcome contributions from the community! Here's how you can help:

### Ways to Contribute

* 🐛 **Report Bugs** - Found an issue? [Open a bug report](https://github.com/Daniel-iel/Spinner/issues/new?labels=bug)
* ✨ **Suggest Features** - Have an idea? [Request a feature](https://github.com/Daniel-iel/Spinner/issues/new?labels=enhancement)
* 📖 **Improve Documentation** - Help make our docs better
* 💻 **Submit Code** - Fix bugs or implement new features
* ⭐ **Star the Repo** - Show your support!

### Development Setup

1. **Fork and Clone**

   ```bash
   git clone https://github.com/YOUR-USERNAME/Spinner.git
   cd Spinner
   ```

2. **Build the Project**

   ```bash
   cd src
   dotnet build
   ```

3. **Run Tests**

   ```bash
   cd Spinner.Test
   dotnet test
   ```

4. **Run Benchmarks** (optional)

   ```bash
   cd bench/Spinner.Benchmark
   dotnet run -c Release
   ```

### Pull Request Guidelines

When submitting a PR, please follow these guidelines:

#### Before Creating a PR

* ✅ **Create an Issue First** - Discuss your proposal before starting work
* ✅ **Fork the Repository** - Work on your own fork
* ✅ **Create a Feature Branch** - Use descriptive names (e.g., `feature/add-new-padding-mode` or `fix/null-reference-bug`)
* ✅ **Follow Code Style** - Match the existing code style and conventions
* ✅ **Write Tests** - Add or update tests for your changes
* ✅ **Update Documentation** - If you change functionality, update the docs

#### PR Requirements

* 📝 **Clear Description** - Explain what changes you made and why
* 🔗 **Link Related Issues** - Reference issues using `Fixes #123` or `Closes #456`
* ✅ **All Tests Pass** - Ensure `dotnet test` passes
* 📊 **No Performance Regression** - Run benchmarks for performance-critical changes
* 📚 **Documentation Updated** - Update README.md or docs/ if needed
* 🔍 **Code Review Ready** - Address all review comments promptly

#### PR Title Format

Use conventional commits format:

```text
feat: Add support for custom date formats
fix: Resolve null reference in ReadFromSpan
docs: Update migration guide with examples
perf: Optimize StringBuilder allocation
test: Add unit tests for padding scenarios
refactor: Simplify property cache initialization
```

#### Example PR Description

```markdown
## Description
This PR adds support for custom date format parsing using interceptors.

## Changes
- Added `DateFormatInterceptor` example to documentation
- Updated `mapping-string-into-type.md` with date parsing examples
- Added unit tests for date format validation

## Related Issues
Fixes #42

## Testing
- [x] All existing tests pass
- [x] Added new tests for date format parsing
- [x] Manually tested with multiple date formats

## Checklist
- [x] Code follows project style guidelines
- [x] Tests added/updated
- [x] Documentation updated
- [x] No breaking changes (or documented if breaking)
```

### Code Standards

* **C# Version**: Target .NET 5+
* **Code Style**: Follow standard C# conventions
* **Naming**: Use PascalCase for public members, camelCase for private
* **Performance**: Consider memory allocations and performance impact
* **Thread Safety**: Ensure code is thread-safe where applicable
* **Tests**: Aim for high code coverage with meaningful tests

### Running Mutation Tests

We use Stryker.NET for mutation testing:

```bash
cd src/Spinner.Test
dotnet stryker
```

### Project Structure

```text
Spinner/
├── src/
│   ├── Spinner/           # Main library
│   └── Spinner.Test/      # Unit tests
├── bench/
│   └── Spinner.Benchmark/ # Performance benchmarks
├── docs/                  # Documentation (Docusaurus)
└── README.md
```

## Community

* 💬 **Discussions** - [GitHub Discussions](https://github.com/Daniel-iel/Spinner/discussions)
* 🐛 **Issues** - [GitHub Issues](https://github.com/Daniel-iel/Spinner/issues)
* 📧 **Contact** - For security issues, contact the maintainers privately

## Versioning

Spinner follows [Semantic Versioning](https://semver.org/):

* **Major version** (X.0.0) - Breaking changes
* **Minor version** (0.X.0) - New features, backward compatible
* **Patch version** (0.0.X) - Bug fixes, backward compatible

### Supported .NET Versions

* .NET 10.0
* .NET 09.0
* .NET 08.0

## Roadmap

Planned features for future releases:

* [ ] Source Generators for compile-time validation
* [ ] Support for nested objects
* [ ] Custom encoding support (EBCDIC, ASCII variants)
* [ ] Async read/write operations for streams
* [ ] Record type support
* [ ] Enhanced error messages with detailed diagnostics

See our [GitHub Issues](https://github.com/Daniel-iel/Spinner/issues?q=is%3Aissue+is%3Aopen+label%3Aenhancement) for the full list of planned features and vote on what you'd like to see next!

## Contributors

A huge thank you to all our contributors! 🙏

[![Daniel-Profile](https://github.com/Daniel-iel.png?size=40)](https://github.com/Daniel-iel)

Want to see your name here? Check out our [Contributing Guide](#contributing) and submit a PR!

## Acknowledgments

* **BenchmarkDotNet** - For excellent benchmarking tools
* **Stryker.NET** - For mutation testing capabilities
* **All contributors** - For making this project better

## Support

If you find Spinner useful, consider:

* ⭐ **Star this repository** - Help others discover the project
* 📢 **Share with colleagues** - Spread the word
* 💰 **Sponsor the project** - Help maintain infrastructure (domain, SSL, CI/CD)
* 🐛 **Report issues** - Help us improve quality
* 💻 **Contribute code** - Make Spinner even better

Your support helps keep Spinner maintained and improved. All donations are used exclusively for project infrastructure (domain, SSL certificates, and CI/CD resources).

## License

Spinner is licensed under the **[MIT License](LICENSE.md)**.

```text
Copyright (c) 2025 Daniel-iel

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
```

**TL;DR** - You can do whatever you want with this code as long as you include the original copyright and license notice.

---

<div align="center">

## 📊 Project Stats

![Repobeats analytics image](https://repobeats.axiom.co/api/embed/c3f5ed375e6e703c23a90745aaee5bca46ebd0fd.svg)

---

**Made with ❤️ by the Spinner team**

[⬆ Back to top](#)

</div>

