// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.CaseConverter;

using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Provides extension methods for converting strings between different cases.
/// </summary>
public static partial class CaseConverter
{
	/// <summary>
	/// Gets a <see cref="Regex"/> that matches all non-alphanumeric characters.
	/// </summary>
	/// <returns>The compiled <see cref="Regex"/> instance.</returns>
#if NET7_0_OR_GREATER
	[GeneratedRegex(@"[^\p{L}0-9]", RegexOptions.Compiled)]
	private static partial Regex NonAlphaNumericRegex();
#else
	private static Regex NonAlphaNumericRegex() => NonAlphaNumericRegexInstance;
	private static readonly Regex NonAlphaNumericRegexInstance = new(@"[^\p{L}0-9]", RegexOptions.Compiled);
#endif

	/// <summary>
	/// Gets a <see cref="Regex"/> that matches all non-alphabetic characters.
	/// </summary>
	/// <returns>The compiled <see cref="Regex"/> instance.</returns>
#if NET7_0_OR_GREATER
	[GeneratedRegex(@"[^\p{L}]", RegexOptions.Compiled)]
	private static partial Regex NonAlphaRegex();
#else
	private static Regex NonAlphaRegex() => NonAlphaRegexInstance;
	private static readonly Regex NonAlphaRegexInstance = new(@"[^\p{L}]", RegexOptions.Compiled);
#endif

	/// <summary>
	/// Gets a <see cref="Regex"/> that splits on case changes, such as transitions from
	/// lower to upper or upper to lower within a string.
	/// </summary>
	/// <returns>The compiled <see cref="Regex"/> instance.</returns>
#if NET7_0_OR_GREATER
	[GeneratedRegex(@"(?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])|(?<=[^\p{Lu}])(?=[\p{Lu}])|(?<=[\p{L}])(?=[^\p{L}])", RegexOptions.Compiled)]
	private static partial Regex SplitOnCaseChangeRegex();
#else
	private static Regex SplitOnCaseChangeRegex() => SplitOnCaseChangeRegexInstance;
	private static readonly Regex SplitOnCaseChangeRegexInstance = new(@"(?<=[\p{Lu}])(?=[\p{Lu}][\p{Ll}])|(?<=[^\p{Lu}])(?=[\p{Lu}])|(?<=[\p{L}])(?=[^\p{L}])", RegexOptions.Compiled);
#endif

	/// <summary>
	/// Returns a copy of this string with the first character converted to lowercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to lowercase.</returns>
	public static string ToLowercaseFirstChar(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif
#if NETSTANDARD2_0
		return CollapseSpaces(input.Length > 0 ? char.ToLowerInvariant(input[0]) + input.Substring(1) : input).Trim();
#else
		return CollapseSpaces(input.Length > 0 ? char.ToLowerInvariant(input[0]) + input[1..] : input).Trim();
#endif
	}

	/// <summary>
	/// Returns a copy of this string with the first character converted to uppercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to uppercase.</returns>
	public static string ToUppercaseFirstChar(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif
#if NETSTANDARD2_0
		return CollapseSpaces(input.Length > 0 ? char.ToUpperInvariant(input[0]) + input.Substring(1) : input).Trim();
#else
		return CollapseSpaces(input.Length > 0 ? char.ToUpperInvariant(input[0]) + input[1..] : input).Trim();
#endif
	}

	/// <summary>
	/// Returns a copy of this string converted to Title Case. Example: "the quick brown fox" becomes "The Quick Brown Fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in Title Case.</returns>
	public static string ToTitleCase(this string input)
	{
		string output = input;
		output = SplitOnCaseChangeRegex().Replace(output, " ");
		output = CollapseSpaces(output).Trim();

		// If the input is all caps, we want to convert it to lowercase before converting to title case,
		// as TextInfo.ToTitleCase preserves words that are all caps assuming they are acronyms.
		if (IsAllCaps(output))
		{
			output = output.ToLowerInvariant();
		}

		return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(output);
	}

	/// <summary>
	/// Determines whether all alphabetic characters in the specified string are uppercase.
	/// </summary>
	/// <param name="output">The string to check.</param>
	/// <returns><c>true</c> if all alphabetic characters are uppercase; otherwise, <c>false</c>.</returns>
	public static bool IsAllCaps(this string output)
	{
		string alphaChars = NonAlphaRegex().Replace(output, string.Empty);
		return alphaChars.All(char.IsUpper);
	}

	/// <summary>
	/// Collapses multiple spaces into a single space within the specified string.
	/// </summary>
	/// <param name="output">The string to process.</param>
	/// <returns>A new string with collapsed spaces.</returns>
	private static string CollapseSpaces(string output)
	{
#if NETSTANDARD2_0
		while (output.Contains("  "))
		{
			output = output.Replace("  ", " ");
		}
#else
		while (output.Contains("  ", StringComparison.Ordinal))
		{
			output = output.Replace("  ", " ", StringComparison.Ordinal);
		}
#endif

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to PascalCase. Example: "the quick brown fox" becomes "TheQuickBrownFox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in PascalCase.</returns>
	public static string ToPascalCase(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif

		string output = input;
		output = NonAlphaNumericRegex().Replace(output, " ");
		output = SplitOnCaseChangeRegex().Replace(output, " ");
		output = output.ToTitleCase();
#if NETSTANDARD2_0
		output = output.Replace(" ", string.Empty);
#else
		output = output.Replace(" ", string.Empty, StringComparison.Ordinal);
#endif

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to camelCase. Example: "the quick brown fox" becomes "theQuickBrownFox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in camelCase.</returns>
	public static string ToCamelCase(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif

		string output = input.ToPascalCase();
		return output.ToLowercaseFirstChar();
	}

	/// <summary>
	/// Returns a copy of this string converted to snake_case. Example: "the quick brown fox" becomes "the_quick_brown_fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in snake_case.</returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "We actually want lowercase here as snake case is lowercase")]
	public static string ToSnakeCase(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif
		return input.ToMacroCase().ToLowerInvariant();
	}

	/// <summary>
	/// Returns a copy of this string converted to kebab-case. Example: "the quick brown fox" becomes "the-quick-brown-fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in kebab-case.</returns>
	public static string ToKebabCase(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif

		string output = input.ToSnakeCase();
#if NETSTANDARD2_0
		output = output.Replace("_", "-");
#else
		output = output.Replace("_", "-", StringComparison.Ordinal);
#endif

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to MACRO_CASE. Example: "the quick brown fox" becomes "THE_QUICK_BROWN_FOX".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in MACRO_CASE.</returns>
	public static string ToMacroCase(this string input)
	{
#if NET6_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(input);
#else
		if (input is null)
		{
			throw new ArgumentNullException(nameof(input));
		}
#endif

		string output = input.Trim();
		output = NonAlphaNumericRegex().Replace(output, " ");
		output = SplitOnCaseChangeRegex().Replace(output, " ").ToUpperInvariant();
#if NETSTANDARD2_0
		output = output.Replace(" ", "_");

		while (output.Contains("__"))
		{
			output = output.Replace("__", "_");
		}
#else
		output = output.Replace(" ", "_", StringComparison.Ordinal);

		while (output.Contains("__", StringComparison.Ordinal))
		{
			output = output.Replace("__", "_", StringComparison.Ordinal);
		}
#endif

		return output;
	}
}
