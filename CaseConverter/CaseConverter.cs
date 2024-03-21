namespace ktsu.io.CaseConverter;

using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Provides extension methods for converting strings between different cases.
/// </summary>
public static partial class CaseConverter
{
	[GeneratedRegex("[^A-Za-z0-9]", RegexOptions.Compiled)]
	private static partial Regex NonAlphaNumericRegex();

	[GeneratedRegex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.Compiled)]
	private static partial Regex SplitOnCaseChangeRegex();

	/// <summary>
	/// Returns a copy of this string with the first character converted to lowercase.
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToLowercaseFirstChar(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		return input.Length > 0 ? char.ToLowerInvariant(input[0]) + input[1..] : input;
	}

	/// <summary>
	/// Returns a copy of this string with the first character converted to uppercase.
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToUppercaseFirstChar(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		return input.Length > 0 ? char.ToUpperInvariant(input[0]) + input[1..] : input;
	}

	/// <summary>
	/// Returns a copy of this string converted to Title Case. Example: "the quick brown fox" becomes "The Quick Brown Fox".
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToTitleCase(this string input) => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);

	/// <summary>
	/// Returns a copy of this string converted to PascalCase. Example: "the quick brown fox" becomes "TheQuickBrownFox".
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
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
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToCamelCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		string output = input.ToPascalCase();
		return output.ToLowercaseFirstChar();
	}

	/// <summary>
	/// Returns a copy of this string converted to snake_case. Example: "the quick brown fox" becomes "the_quick_brown_fox".
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "We actually want lowercase here as snake case is lowercase")]
	public static string ToSnakeCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		return input.ToMacroCase().ToLowerInvariant();
	}

	/// <summary>
	/// Returns a copy of this string converted to kebab-case. Example: "the quick brown fox" becomes "the-quick-brown-fox".
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
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
	/// <param name="input"></param>
	/// <returns></returns>
	public static string ToMacroCase(this string input)
	{
		ArgumentNullException.ThrowIfNull(input);

		string output = input;
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
