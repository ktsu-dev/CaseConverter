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
	[GeneratedRegex("[^A-Za-z0-9]", RegexOptions.Compiled)]
	private static partial Regex NonAlphaNumericRegex();

	/// <summary>
	/// Gets a <see cref="Regex"/> that matches all non-alphabetic characters.
	/// </summary>
	/// <returns>The compiled <see cref="Regex"/> instance.</returns>
	[GeneratedRegex("[^A-Za-z]", RegexOptions.Compiled)]
	private static partial Regex NonAlphaRegex();

	/// <summary>
	/// Gets a <see cref="Regex"/> that splits on case changes, such as transitions from
	/// lower to upper or upper to lower within a string.
	/// </summary>
	/// <returns>The compiled <see cref="Regex"/> instance.</returns>
	[GeneratedRegex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.Compiled)]
	private static partial Regex SplitOnCaseChangeRegex();

	/// <summary>
	/// Returns a copy of this string with the first character converted to lowercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to lowercase.</returns>
	public static string ToLowercaseFirstChar(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);
		return CollapseSpaces(input.Length > 0 ? char.ToLowerInvariant(input[0]) + input[1..] : input).Trim();
	}

	/// <summary>
	/// Returns a copy of this string with the first character converted to uppercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to uppercase.</returns>
	public static string ToUppercaseFirstChar(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);
		return CollapseSpaces(input.Length > 0 ? char.ToUpperInvariant(input[0]) + input[1..] : input).Trim();
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
		while (output.Contains("  ", StringComparison.Ordinal))
		{
			output = output.Replace("  ", " ", StringComparison.Ordinal);
		}

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to PascalCase. Example: "the quick brown fox" becomes "TheQuickBrownFox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in PascalCase.</returns>
	public static string ToPascalCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		string output = input;
		output = NonAlphaNumericRegex().Replace(output, " ");
		output = SplitOnCaseChangeRegex().Replace(output, " ");
		output = output.ToTitleCase();
		output = output.Replace(" ", string.Empty, StringComparison.Ordinal);

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to camelCase. Example: "the quick brown fox" becomes "theQuickBrownFox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in camelCase.</returns>
	public static string ToCamelCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

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
		ArgumentNullException.ThrowIfNull(input);
		return input.ToMacroCase().ToLowerInvariant();
	}

	/// <summary>
	/// Returns a copy of this string converted to kebab-case. Example: "the quick brown fox" becomes "the-quick-brown-fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in kebab-case.</returns>
	public static string ToKebabCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		string output = input.ToSnakeCase();
		output = output.Replace("_", "-", StringComparison.Ordinal);

		return output;
	}

	/// <summary>
	/// Returns a copy of this string converted to MACRO_CASE. Example: "the quick brown fox" becomes "THE_QUICK_BROWN_FOX".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in MACRO_CASE.</returns>
	public static string ToMacroCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		string output = input.Trim();
		output = NonAlphaNumericRegex().Replace(output, " ");
		output = SplitOnCaseChangeRegex().Replace(output, " ").ToUpperInvariant();
		output = output.Replace(" ", "_", StringComparison.Ordinal);

		while (output.Contains("__", StringComparison.Ordinal))
		{
			output = output.Replace("__", "_", StringComparison.Ordinal);
		}

		return output;
	}
}
