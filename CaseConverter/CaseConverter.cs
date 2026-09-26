// Copyright (c) 2023-2026 ktsu-dev contributors

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ktsu.CaseConverter.Test")]

namespace ktsu.CaseConverter;

using System.Globalization;
using System.Text;

/// <summary>
/// Provides extension methods for converting strings between different cases.
/// </summary>
public static partial class CaseConverter
{
	/// <summary>
	/// Returns the number of UTF-16 code units making up the code point at <paramref name="index"/>.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="index">The index of the first code unit of the code point.</param>
	/// <returns>2 for a surrogate pair, otherwise 1.</returns>
	private static int CodePointLength(string input, int index) => char.IsSurrogatePair(input, index) ? 2 : 1;

	/// <summary>
	/// Replaces every code point that is not a Unicode letter or an ASCII digit with a space.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <returns>A new string with each non-alphanumeric code point replaced by a space.</returns>
	/// <remarks>
	/// This walks by code point rather than by UTF-16 code unit. The regex this replaces
	/// (<c>[^\p{L}0-9]</c>) matched per code unit, and a surrogate code unit is categorised as
	/// <see cref="UnicodeCategory.Surrogate"/> rather than as a letter — so each half of a
	/// surrogate pair matched and letters outside the Basic Multilingual Plane were silently
	/// deleted instead of preserved.
	/// </remarks>
	private static string ReplaceNonAlphaNumericWithSpace(string input)
	{
		StringBuilder builder = new(input.Length);

		for (int i = 0; i < input.Length;)
		{
			int length = CodePointLength(input, i);

			if (char.IsLetter(input, i) || input[i] is >= '0' and <= '9')
			{
				builder.Append(input, i, length);
			}
			else
			{
				builder.Append(' ');
			}

			i += length;
		}

		return builder.ToString();
	}

	/// <summary>
	/// Inserts a space at each case change, such as transitions from lower to upper or from a
	/// letter to a non-letter.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <returns>A new string with a space inserted at each word boundary.</returns>
	/// <remarks>
	/// This walks by code point, for the same reason
	/// <see cref="ReplaceNonAlphaNumericWithSpace"/> does. The regex this replaces tested
	/// <c>\p{L}</c> and <c>\p{Lu}</c> per UTF-16 code unit, so a letter outside the Basic
	/// Multilingual Plane read as a non-letter and had a spurious word boundary inserted
	/// before it.
	/// </remarks>
	private static string SplitOnCaseChange(string input)
	{
		StringBuilder builder = new(input.Length);
		int previousStart = -1;

		for (int i = 0; i < input.Length;)
		{
			int length = CodePointLength(input, i);
			int nextStart = i + length;

			if (previousStart >= 0 && IsWordBoundary(input, previousStart, i, nextStart))
			{
				builder.Append(' ');
			}

			builder.Append(input, i, length);
			previousStart = i;
			i = nextStart;
		}

		return builder.ToString();
	}

	/// <summary>
	/// Determines whether a word boundary falls immediately before the code point at
	/// <paramref name="start"/>.
	/// </summary>
	/// <param name="input">The string being split.</param>
	/// <param name="previousStart">The index of the preceding code point.</param>
	/// <param name="start">The index of the code point to test.</param>
	/// <param name="nextStart">The index of the following code point, which may be past the end.</param>
	/// <returns><c>true</c> if a space belongs before <paramref name="start"/>; otherwise, <c>false</c>.</returns>
	private static bool IsWordBoundary(string input, int previousStart, int start, int nextStart)
	{
		bool previousIsUpper = char.IsUpper(input, previousStart);
		bool currentIsUpper = char.IsUpper(input, start);

		// The tail of an acronym run that begins a new word: "XMLDoc" breaks before the "D".
		if (previousIsUpper && currentIsUpper && nextStart < input.Length && char.IsLower(input, nextStart))
		{
			return true;
		}

		// The start of a capitalised word: "fooBar" breaks before the "B".
		if (!previousIsUpper && currentIsUpper)
		{
			return true;
		}

		// A letter followed by a non-letter: "abc123" breaks before the "1".
		return char.IsLetter(input, previousStart) && !char.IsLetter(input, start);
	}

	/// <summary>
	/// Returns a copy of this string with the first character converted to lowercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to lowercase.</returns>
	public static string ToLowercaseFirstChar(this string input)
	{
		Ensure.NotNull(input);
#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
		return CollapseSpaces(input.Length > 0 ? char.ToLowerInvariant(input[0]) + input.Substring(1) : input).Trim();
#pragma warning restore IDE0057
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
		Ensure.NotNull(input);
#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
		return CollapseSpaces(input.Length > 0 ? char.ToUpperInvariant(input[0]) + input.Substring(1) : input).Trim();
#pragma warning restore IDE0057
#else
		return CollapseSpaces(input.Length > 0 ? char.ToUpperInvariant(input[0]) + input[1..] : input).Trim();
#endif
	}

	/// <summary>
	/// Lowercases every space separated word whose letters are all uppercase, leaving the rest alone.
	/// </summary>
	/// <param name="input">The string to process, already split into space separated words.</param>
	/// <returns>A new string with each all-caps word lowercased.</returns>
	/// <remarks>
	/// <see cref="TextInfo.ToTitleCase(string)"/> preserves a word that is all caps, on the assumption
	/// that it is an acronym. Lowering such a word first is what normalizes it instead, and deciding
	/// this per word rather than for the whole string is what keeps a word's result independent of its
	/// neighbours.
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Lowercasing is the point: TextInfo.ToTitleCase then capitalizes the first letter of each word.")]
	private static string LowercaseAllCapsWords(string input)
	{
		string[] words = input.Split(' ');
		StringBuilder builder = new(input.Length);

		for (int i = 0; i < words.Length; i++)
		{
			if (i > 0)
			{
				builder.Append(' ');
			}

			string word = words[i];
			builder.Append(IsAllCaps(word) ? word.ToLowerInvariant() : word);
		}

		return builder.ToString();
	}

	/// <summary>
	/// Returns a copy of this string converted to Title Case. Example: "the quick brown fox" becomes "The Quick Brown Fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in Title Case.</returns>
	/// <remarks>
	/// An all-caps word is normalized rather than preserved as an acronym, so <c>"HTTP"</c> becomes
	/// <c>"Http"</c> and <c>"parse HTTP header"</c> becomes <c>"Parse Http Header"</c>. The decision is
	/// made per word, so a word converts the same way whatever else is in the string.
	/// </remarks>
	public static string ToTitleCase(this string input)
	{
		Ensure.NotNull(input);

		string output = input;
		output = SplitOnCaseChange(output);
		output = CollapseSpaces(output).Trim();

		// TextInfo.ToTitleCase preserves words that are all caps assuming they are acronyms, so lowercase
		// them first. This is done per word rather than only when the whole string is all caps, because
		// otherwise the same word converts two different ways depending on its neighbours.
		output = LowercaseAllCapsWords(output);

		return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(output);
	}

	/// <summary>
	/// Determines whether all alphabetic characters in the specified string are uppercase.
	/// </summary>
	/// <param name="output">The string to check.</param>
	/// <returns><c>true</c> if all alphabetic characters are uppercase; otherwise, <c>false</c>.</returns>
	public static bool IsAllCaps(this string output)
	{
		Ensure.NotNull(output);

		for (int i = 0; i < output.Length;)
		{
			int length = CodePointLength(output, i);

			if (char.IsLetter(output, i) && !char.IsUpper(output, i))
			{
				return false;
			}

			i += length;
		}

		// A string with no letters at all is vacuously all caps.
		return true;
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
	/// <remarks>
	/// An all-caps word is normalized rather than preserved as an acronym, so <c>"MAX_SIZE"</c> and
	/// <c>"set MAX_SIZE"</c> become <c>"MaxSize"</c> and <c>"SetMaxSize"</c>. The decision is made per
	/// word by <see cref="ToTitleCase(string)"/>, so a word converts the same way whatever else is in
	/// the string.
	/// </remarks>
	public static string ToPascalCase(this string input)
	{
		Ensure.NotNull(input);

		string output = input;
		output = ReplaceNonAlphaNumericWithSpace(output);
		output = SplitOnCaseChange(output);
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
	/// <remarks>
	/// An all-caps word is normalized rather than preserved as an acronym, so <c>"URL"</c> and
	/// <c>"my URL handler"</c> become <c>"url"</c> and <c>"myUrlHandler"</c>. The decision is made per
	/// word by <see cref="ToTitleCase(string)"/>, so a word converts the same way whatever else is in
	/// the string.
	/// </remarks>
	public static string ToCamelCase(this string input)
	{
		Ensure.NotNull(input);

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
		Ensure.NotNull(input);
		return input.ToMacroCase().ToLowerInvariant();
	}

	/// <summary>
	/// Returns a copy of this string converted to kebab-case. Example: "the quick brown fox" becomes "the-quick-brown-fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in kebab-case.</returns>
	public static string ToKebabCase(this string input)
	{
		Ensure.NotNull(input);

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
		Ensure.NotNull(input);

		string output = input.Trim();
		output = ReplaceNonAlphaNumericWithSpace(output);
		output = SplitOnCaseChange(output).ToUpperInvariant();
		output = CollapseSpaces(output).Trim();
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
