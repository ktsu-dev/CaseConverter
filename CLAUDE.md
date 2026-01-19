# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

CaseConverter is a lightweight .NET library providing extension methods for converting strings between various case styles (PascalCase, camelCase, snake_case, kebab-case, MACRO_CASE, Title Case).

The library is published as a NuGet package (`ktsu.CaseConverter`) and targets multiple .NET versions: net10.0, net9.0, net8.0, net7.0, net6.0, net5.0, netstandard2.0, and netstandard2.1.

## Development Commands

### Building and Testing

```bash
# Build the solution
dotnet build CaseConverter.sln

# Build a specific target framework
dotnet build CaseConverter/CaseConverter.csproj --framework net10.0

# Run all tests
dotnet test CaseConverter.Test/CaseConverter.Test.csproj

# Run tests with coverage (CI uses this)
dotnet test CaseConverter.Test/CaseConverter.Test.csproj --collect:"XPlat Code Coverage" --results-directory ./coverage
```

### Project Requirements

- .NET SDK 10.0.100 or later (specified in global.json)
- Uses `ktsu.Sdk` (v2.2.1) and `MSTest.Sdk` (v4.0.2) as MSBuild SDKs
- Central package management enabled via Directory.Packages.props

## Code Architecture

### Core Implementation

The entire library is contained in a single file: `CaseConverter/CaseConverter.cs`

**Key architectural patterns:**

1. **Conditional Compilation for Multi-Targeting**: Uses extensive preprocessor directives to support 8 different target frameworks
   - NET7_0_OR_GREATER: Uses `[GeneratedRegex]` for performance
   - NET6_0_OR_GREATER: Uses `ArgumentNullException.ThrowIfNull()`
   - NETSTANDARD2_0: Uses backward-compatible APIs (Substring instead of ranges, Contains without StringComparison)

2. **Regex-Based String Processing**: Three core regex patterns drive all conversions
   - `NonAlphaNumericRegex`: Removes all non-alphanumeric characters
   - `NonAlphaRegex`: Removes all non-alphabetic characters
   - `SplitOnCaseChangeRegex`: Detects case transitions (camelCase → camel Case)

3. **Conversion Chain Pattern**: Methods build on each other
   - ToPascalCase() calls ToTitleCase()
   - ToCamelCase() calls ToPascalCase() then ToLowercaseFirstChar()
   - ToSnakeCase() calls ToMacroCase() then ToLowerInvariant()
   - ToKebabCase() calls ToSnakeCase() then replaces underscores with hyphens

4. **Null Safety**: All public methods validate input with ArgumentNullException (using appropriate API for target framework)

### Test Structure

Tests are in `CaseConverter.Test/CaseConverterTests.cs` using MSTest framework. Test coverage includes:
- Basic conversion functionality for each case style
- Edge cases (empty strings, single characters, multiple spaces)
- Null handling validation
- The public `IsAllCaps()` helper method

## Code Style Requirements

### File Headers
All C# files must include this copyright header:
```csharp
// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.
```

### Formatting (from .editorconfig)
- C# files: Use **tabs** for indentation (not spaces)
- Tab width: 4
- End of line: CRLF (Windows style)
- Charset: UTF-8
- All .NET analyzer diagnostics are treated as **errors** (not warnings)
- No `this.` qualifiers
- Always use accessibility modifiers (public, private, etc.)
- Readonly fields required where applicable

## CI/CD Pipeline

The GitHub Actions workflow (`.github/workflows/dotnet.yml`) runs on Windows and includes:
1. Build and test across all target frameworks
2. Code coverage collection (OpenCover format)
3. SonarQube analysis (if SONAR_TOKEN is configured)
4. Custom PowerShell build pipeline (`scripts/PSBuild.psm1`)
5. Automated NuGet package publishing on releases
6. Winget manifest generation and updates

**Important**: The CI pipeline uses a custom `Invoke-CIPipeline` function from the PSBuild module that handles versioning, changelog management, package creation, and release orchestration.
