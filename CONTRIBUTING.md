# Contributing to Spinner

Thank you for your interest in contributing to Spinner! We welcome contributions from the community and are grateful for your support.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How Can I Contribute?](#how-can-i-contribute)
- [Development Setup](#development-setup)
- [Pull Request Process](#pull-request-process)
- [Coding Standards](#coding-standards)
- [Testing Guidelines](#testing-guidelines)
- [Documentation](#documentation)
- [Community](#community)

## Code of Conduct

This project adheres to a code of conduct that all contributors are expected to follow. Please be respectful and constructive in all interactions.

### Our Standards

- **Be Respectful**: Treat everyone with respect and kindness
- **Be Constructive**: Provide helpful feedback and suggestions
- **Be Collaborative**: Work together to make Spinner better
- **Be Patient**: Remember that everyone has different levels of experience

## How Can I Contribute?

### Reporting Bugs

Before creating bug reports, please check existing issues to avoid duplicates. When creating a bug report, include as many details as possible:

- Use the bug report template
- Use a clear and descriptive title
- Provide step-by-step reproduction steps
- Include code samples
- Describe the expected vs actual behavior
- Include environment details (.NET version, OS, etc.)

### Suggesting Features

Feature suggestions are welcome! Please:

- Use the feature request template
- Clearly describe the problem it solves
- Provide examples of how it would be used
- Consider potential drawbacks or alternatives

### Improving Documentation

Documentation improvements are always appreciated:

- Fix typos or unclear wording
- Add examples or clarifications
- Update outdated information
- Translate documentation (if applicable)

### Contributing Code

1. **Find an Issue**: Look for issues labeled `good first issue` or `help wanted`
2. **Discuss First**: For major changes, open an issue to discuss your approach
3. **Fork & Create Branch**: Create a feature branch from `main`
4. **Write Code**: Follow our coding standards
5. **Add Tests**: Ensure your changes are tested
6. **Update Docs**: Update documentation if needed
7. **Submit PR**: Create a pull request using our template

## Development Setup

### Prerequisites

- .NET SDK 6.0 or higher
- Git
- Your favorite IDE (Visual Studio, Rider, VS Code)

### Getting Started

1. **Fork the repository**

   Click the "Fork" button on GitHub

2. **Clone your fork**

   ```bash
   git clone https://github.com/YOUR-USERNAME/Spinner.git
   cd Spinner
   ```

3. **Add upstream remote**

   ```bash
   git remote add upstream https://github.com/Daniel-iel/Spinner.git
   ```

4. **Create a branch**

   ```bash
   git checkout -b feature/your-feature-name
   ```

5. **Build the project**

   ```bash
   cd src
   dotnet build
   ```

6. **Run tests**

   ```bash
   cd Spinner.Test
   dotnet test
   ```

### Running Benchmarks

For performance-critical changes:

```bash
cd bench/Spinner.Benchmark
dotnet run -c Release
```

### Running Mutation Tests

```bash
cd src/Spinner.Test
dotnet stryker
```

## Pull Request Process

### Before Submitting

- [ ] Create an issue to discuss major changes
- [ ] Fork the repository and create a feature branch
- [ ] Follow the coding standards
- [ ] Write or update tests
- [ ] Update documentation
- [ ] Run all tests locally
- [ ] Run benchmarks (for performance changes)
- [ ] Ensure no new warnings or errors

### PR Title Format

Use [Conventional Commits](https://www.conventionalcommits.org/) format:

```text
<type>: <description>

Types:
- feat: New feature
- fix: Bug fix
- docs: Documentation changes
- perf: Performance improvement
- test: Adding or updating tests
- refactor: Code refactoring
- style: Code style changes (formatting, etc.)
- chore: Build process or tooling changes
```

**Examples:**

```text
feat: Add support for custom date formats
fix: Resolve null reference in ReadFromSpan
docs: Update migration guide with examples
perf: Optimize StringBuilder allocation
test: Add unit tests for padding scenarios
```

### PR Description

Use the provided template and include:

- Clear description of changes
- Type of change (bug fix, feature, etc.)
- Related issues (use "Fixes #123")
- Testing performed
- Checklist completion
- Screenshots (if UI changes)

### Review Process

1. **Automated Checks**: CI/CD will run tests and checks
2. **Code Review**: Maintainers will review your code
3. **Feedback**: Address any requested changes
4. **Approval**: Once approved, your PR will be merged
5. **Cleanup**: Delete your feature branch after merge

## Coding Standards

### C# Style Guide

- **Naming Conventions**:
  - PascalCase for public members, classes, methods
  - camelCase for private fields, parameters
  - Use `_` prefix for private fields: `_myField`

- **Code Organization**:
  - One class per file
  - Organize using statements
  - Group members by type (fields, properties, methods)

- **Performance**:
  - Avoid unnecessary allocations
  - Use `Span<T>` and `ReadOnlySpan<T>` where appropriate
  - Consider ThreadStatic for per-thread resources
  - Profile performance-critical code

- **Thread Safety**:
  - Ensure code is thread-safe where applicable
  - Document thread-safety guarantees
  - Use appropriate synchronization primitives

### Code Quality

- **SOLID Principles**: Follow SOLID design principles
- **DRY**: Don't Repeat Yourself
- **KISS**: Keep It Simple, Stupid
- **Comments**: Comment complex logic, not obvious code
- **Error Handling**: Use appropriate exception types
- **Null Safety**: Handle null appropriately

### Example

```csharp
/// <summary>
/// Converts an object to a positional string.
/// </summary>
/// <param name="obj">Object to serialize.</param>
/// <returns>Positional string representation.</returns>
public string WriteAsString(T obj)
{
    var sb = builder ??= new StringBuilder(ReadObjectMapper?.Length ?? 256);
    sb.Clear();

    WritePositionalString(ref sb, obj);

    return ReadObjectMapper is not null
        ? sb.ToString(0, ReadObjectMapper.Length)
        : sb.ToString();
}
```

## Testing Guidelines

### Test Requirements

- **Unit Tests**: All new code must have unit tests
- **Coverage**: Aim for high code coverage (>80%)
- **Mutation Testing**: Ensure tests catch mutations
- **Edge Cases**: Test boundary conditions and edge cases
- **Error Cases**: Test error handling paths

### Test Structure

Use the Arrange-Act-Assert pattern:

```csharp
[Fact]
public void WriteAsString_ShouldPadLeft_WhenValueIsShorterThanLength()
{
    // Arrange
    var obj = new TestClass { Code = "123" };
    var spinner = new Spinner<TestClass>();

    // Act
    var result = spinner.WriteAsString(obj);

    // Assert
    Assert.Equal("0000000123", result);
}
```

### Test Naming

```text
MethodName_Should_ExpectedBehavior_When_Condition
```

Examples:
- `ReadFromString_Should_ThrowException_When_PropertyNotMapped`
- `WriteAsString_Should_TruncateValue_When_ExceedsLength`
- `ReadFromSpan_Should_ParseCorrectly_When_ValidInput`

## Documentation

### Code Documentation

- Use XML documentation comments for public APIs
- Explain complex algorithms or logic
- Document thread-safety guarantees
- Include usage examples for complex features

### User Documentation

Located in `docs/docs/`:

- Update relevant .md files when changing functionality
- Add examples for new features
- Update migration guide for breaking changes
- Keep performance benchmarks up to date

### README Updates

Update README.md if:

- Adding new major features
- Changing installation process
- Updating supported versions
- Adding new contributors

## Community

### Getting Help

- **Discussions**: [GitHub Discussions](https://github.com/Daniel-iel/Spinner/discussions) for questions
- **Issues**: [GitHub Issues](https://github.com/Daniel-iel/Spinner/issues) for bugs and features
- **Documentation**: [Official Docs](https://spinnerframework.com) for guides

### Staying Informed

- Watch the repository for updates
- Check the [changelog](CHANGELOG.md) for release notes
- Follow discussions for feature proposals

## Recognition

Contributors are recognized in:

- README.md Contributors section
- Release notes
- GitHub contributors page

## Questions?

If you have questions about contributing, feel free to:

- Open a discussion on GitHub
- Comment on an existing issue
- Reach out to maintainers

---

Thank you for contributing to Spinner! Your efforts help make this project better for everyone. 🎉
