# ktsu.CaseConverter

> A library with extension methods to convert strings between common casings used in code.

[![License](https://img.shields.io/github/license/ktsu-dev/CaseConverter)](https://github.com/ktsu-dev/CaseConverter/blob/main/LICENSE.md)
[![NuGet](https://img.shields.io/nuget/v/ktsu.CaseConverter.svg)](https://www.nuget.org/packages/ktsu.CaseConverter/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/ktsu.CaseConverter.svg)](https://www.nuget.org/packages/ktsu.CaseConverter/)
[![Build Status](https://github.com/ktsu-dev/CaseConverter/workflows/build/badge.svg)](https://github.com/ktsu-dev/CaseConverter/actions)
[![GitHub Stars](https://img.shields.io/github/stars/ktsu-dev/CaseConverter?style=social)](https://github.com/ktsu-dev/CaseConverter/stargazers)

## Introduction

CaseConverter is a lightweight .NET library that provides a set of extension methods for converting strings between various case styles commonly used in programming. Whether you need to transform identifiers between different naming conventions or format text for display, CaseConverter makes these operations simple and efficient.

## Features

- **ToTitleCase**: Converts text to Title Case (Each Word Capitalized)
- **ToPascalCase**: Converts text to PascalCase (CapitalizedWords)
- **ToCamelCase**: Converts text to camelCase (firstWordLowerCaseRestCapitalized)
- **ToSnakeCase**: Converts text to snake_case (lowercase_with_underscores)
- **ToKebabCase**: Converts text to kebab-case (lowercase-with-hyphens)
- **ToMacroCase**: Converts text to MACRO_CASE (UPPERCASE_WITH_UNDERSCORES)
- **ToUppercaseFirstChar**: Converts only the first character to uppercase (Uppercasefirstchar)
- **ToLowercaseFirstChar**: Converts only the first character to lowercase (lowercaseFirstChar)

## Installation

### Package Manager Console

```powershell
Install-Package ktsu.CaseConverter
```

### .NET CLI

```bash
dotnet add package ktsu.CaseConverter
```

### Package Reference

```xml
<PackageReference Include="ktsu.CaseConverter" Version="x.y.z" />
```

## Usage Examples

### Basic Example

```csharp
using ktsu.CaseConverter;

string original = "This is a test string";

// Convert to different cases
string pascalCase = original.ToPascalCase(); // "ThisIsATestString"
string camelCase = original.ToCamelCase();   // "thisIsATestString"
string snakeCase = original.ToSnakeCase();   // "this_is_a_test_string"
string kebabCase = original.ToKebabCase();   // "this-is-a-test-string"
string macroCase = original.ToMacroCase();   // "THIS_IS_A_TEST_STRING"

Console.WriteLine(pascalCase);
Console.WriteLine(camelCase);
Console.WriteLine(snakeCase);
Console.WriteLine(kebabCase);
Console.WriteLine(macroCase);
```

### Advanced Usage

```csharp
// Working with code identifiers
string variableName = "customer_order_details";
string className = variableName.ToPascalCase(); // "CustomerOrderDetails"
string propertyName = variableName.ToCamelCase(); // "customerOrderDetails"
string constantName = variableName.ToMacroCase(); // "CUSTOMER_ORDER_DETAILS"

// Handling acronyms and special cases
string withAcronym = "API_response_URL";
string pascalWithAcronym = withAcronym.ToPascalCase(); // "ApiResponseUrl"

// First character transformations
string sentence = "lorem ipsum dolor sit amet";
string capitalized = sentence.ToUppercaseFirstChar(); // "Lorem ipsum dolor sit amet"
```

## API Reference

### Extension Methods

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `ToTitleCase()` | None | `string` | Converts string to Title Case |
| `ToPascalCase()` | None | `string` | Converts string to PascalCase |
| `ToCamelCase()` | None | `string` | Converts string to camelCase |
| `ToSnakeCase()` | None | `string` | Converts string to snake_case |
| `ToKebabCase()` | None | `string` | Converts string to kebab-case |
| `ToMacroCase()` | None | `string` | Converts string to MACRO_CASE |
| `ToUppercaseFirstChar()` | None | `string` | Capitalizes only the first character |
| `ToLowercaseFirstChar()` | None | `string` | Makes only the first character lowercase |

## Contributing

Contributions are welcome! Here's how you can help:

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

Please make sure to update tests as appropriate and adhere to the existing coding style.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
