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
	/// Replaces every code point that is not a Unicode letter or an ASCII digit with a space,
	/// except that an apostrophe between two letters is dropped.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <returns>A new string with each non-alphanumeric code point replaced by a space.</returns>
	/// <remarks>
	/// This walks by code point rather than by UTF-16 code unit. The regex this replaces
	/// (<c>[^\p{L}0-9]</c>) matched per code unit, and a surrogate code unit is categorised as
	/// <see cref="UnicodeCategory.Surrogate"/> rather than as a letter — so each half of a
	/// surrogate pair matched and letters outside the Basic Multilingual Plane were silently
	/// deleted instead of preserved.
	/// <para>
	/// An apostrophe (<c>'</c> or U+2019) between two letters is part of the word, as in
	/// <c>"don't"</c> or <c>"o'neil"</c>, so it is dropped rather than turned into a separator. This
	/// keeps <c>"don't stop"</c> as two words, matching <see cref="ToTitleCase(string)"/>. An
	/// apostrophe anywhere else, such as a leading or trailing quote, still separates words.
	/// </para>
	/// <para>
	/// When the apostrophe follows a capital and every letter after it is lowercase, as in
	/// <c>"CEO's"</c>, those letters are uppercased. Dropping the apostrophe alone would leave
	/// <c>"CEOs"</c>, which <see cref="SplitOnCaseChange(string)"/> reads as an acronym followed by a
	/// capitalised word and splits into <c>"CE Os"</c>. <c>"CEOS"</c> is one all-caps word, which every
	/// converter then normalizes the same way as <c>"CEO"</c>.
	/// </para>
	/// </remarks>
	private static string ReplaceNonAlphaNumericWithSpace(string input)
	{
		StringBuilder builder = new(input.Length);
		int previousStart = -1;
		bool uppercaseSuffix = false;

		for (int i = 0; i < input.Length;)
		{
			int length = CodePointLength(input, i);
			int nextStart = i + length;

			if (char.IsLetter(input, i))
			{
#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
				string letter = input.Substring(i, length);
#pragma warning restore IDE0057
#else
				string letter = input[i..nextStart];
#endif
				builder.Append(uppercaseSuffix ? letter.ToUpperInvariant() : letter);
			}
			else if (IsApostropheWithinWord(input, previousStart, i, nextStart))
			{
				uppercaseSuffix = char.IsUpper(input, previousStart) && AreLettersFromIndexLowercase(input, nextStart);
			}
			else
			{
				builder.Append(input[i] is >= '0' and <= '9' ? input[i] : ' ');
				uppercaseSuffix = false;
			}

			previousStart = i;
			i = nextStart;
		}

		return builder.ToString();
	}

	/// <summary>
	/// Determines whether every letter in the run of letters starting at <paramref name="start"/> is lowercase.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="start">The index of the first letter of the run.</param>
	/// <returns><c>true</c> if no letter in the run is uppercase or titlecase; otherwise, <c>false</c>.</returns>
	private static bool AreLettersFromIndexLowercase(string input, int start)
	{
		for (int i = start; i < input.Length && char.IsLetter(input, i); i += CodePointLength(input, i))
		{
			if (!char.IsLower(input, i))
			{
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Determines whether the code point at <paramref name="start"/> is an apostrophe with a letter on
	/// each side of it.
	/// </summary>
	/// <param name="input">The string being processed.</param>
	/// <param name="previousStart">The index of the preceding code point, or -1 if there is none.</param>
	/// <param name="start">The index of the code point to test.</param>
	/// <param name="nextStart">The index of the following code point, which may be past the end.</param>
	/// <returns><c>true</c> if the code point is an in-word apostrophe; otherwise, <c>false</c>.</returns>
	private static bool IsApostropheWithinWord(string input, int previousStart, int start, int nextStart) =>
		input[start] is '\'' or '\u2019'
		&& previousStart >= 0
		&& nextStart < input.Length
		&& char.IsLetter(input, previousStart)
		&& char.IsLetter(input, nextStart);

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
	private static string SplitOnCaseChange(string input) => SplitOnCaseChange(input, breakBeforeAnyNonLetter: true);

	/// <summary>
	/// Inserts a space at each case change, and before whatever follows a letter as
	/// <paramref name="breakBeforeAnyNonLetter"/> selects.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <param name="breakBeforeAnyNonLetter">
	/// <c>true</c> to break between a letter and any non-letter that follows it; <c>false</c> to break
	/// only between a letter and a digit, leaving punctuation attached to the word before it.
	/// </param>
	/// <returns>A new string with a space inserted at each word boundary.</returns>
	private static string SplitOnCaseChange(string input, bool breakBeforeAnyNonLetter)
	{
		StringBuilder builder = new(input.Length);
		int previousStart = -1;

		for (int i = 0; i < input.Length;)
		{
			int length = CodePointLength(input, i);
			int nextStart = i + length;

			if (previousStart >= 0 && IsWordBoundary(input, previousStart, i, nextStart, breakBeforeAnyNonLetter))
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
	/// <param name="breakBeforeAnyNonLetter">
	/// <c>true</c> to break between a letter and any non-letter; <c>false</c> to break only between a letter and a digit.
	/// </param>
	/// <returns><c>true</c> if a space belongs before <paramref name="start"/>; otherwise, <c>false</c>.</returns>
	private static bool IsWordBoundary(string input, int previousStart, int start, int nextStart, bool breakBeforeAnyNonLetter)
	{
		bool previousIsLetter = char.IsLetter(input, previousStart);
		bool previousIsUpper = char.IsUpper(input, previousStart);
		bool currentIsUpper = char.IsUpper(input, start);

		// A lowercase letter with no uppercase form, such as "ß" or "ﬁ", survives uppercasing, so it
		// takes the case of the nearest letter that has one: "STRAßE" is a single all-caps word.
		int casedNextStart = SkipLowercaseWithNoUppercase(input, nextStart);

		// The tail of an acronym run that begins a new word: "XMLDoc" breaks before the "D".
		if (previousIsUpper && currentIsUpper && casedNextStart < input.Length && char.IsLower(input, casedNextStart))
		{
			return true;
		}

		// The start of a capitalised word: "fooBar" breaks before the "B". Only a letter or digit can
		// end the word before it, so "(Hello" and "don'T" do not split away from their punctuation.
		if (EndsWordThatIsNotUppercase(input, previousStart) && currentIsUpper && (previousIsLetter || char.IsDigit(input, previousStart)))
		{
			return true;
		}

		// A letter followed by a non-letter: "abc123" breaks before the "1", and, when asked to,
		// "abc_def" breaks before the "_".
		if (!previousIsLetter || char.IsLetter(input, start))
		{
			return false;
		}

		return breakBeforeAnyNonLetter || char.IsDigit(input, start);
	}

	/// <summary>
	/// Determines whether the code point at <paramref name="index"/> is a lowercase letter that
	/// uppercasing leaves unchanged, such as <c>"ß"</c>, <c>"ﬁ"</c> or <c>"ŉ"</c>.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="index">The index of the first code unit of the code point.</param>
	/// <returns><c>true</c> if the code point is lowercase and has no uppercase form; otherwise, <c>false</c>.</returns>
	/// <remarks>
	/// Such a letter is still present, and still lowercase, in the output of
	/// <see cref="ToMacroCase(string)"/>, so it must not count as lowercase when the words of an
	/// all-caps string are found again.
	/// </remarks>
	private static bool IsLowercaseWithNoUppercase(string input, int index)
	{
		if (!char.IsLower(input, index))
		{
			return false;
		}

#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
		string codePoint = input.Substring(index, CodePointLength(input, index));
#pragma warning restore IDE0057
#else
		string codePoint = input[index..(index + CodePointLength(input, index))];
#endif
		return string.Equals(codePoint.ToUpperInvariant(), codePoint, StringComparison.Ordinal);
	}

	/// <summary>
	/// Returns the index of the first code point at or after <paramref name="index"/> that is not a
	/// lowercase letter with no uppercase form.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="index">The index to start from, which may be past the end.</param>
	/// <returns>The index found, or the length of <paramref name="input"/> if there is none.</returns>
	private static int SkipLowercaseWithNoUppercase(string input, int index)
	{
		while (index < input.Length && IsLowercaseWithNoUppercase(input, index))
		{
			index += CodePointLength(input, index);
		}

		return index;
	}

	/// <summary>
	/// Determines whether the code point at <paramref name="index"/> ends a word that is not uppercase,
	/// so that a capital after it starts a new word.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="index">The index of the first code unit of the code point before the capital.</param>
	/// <returns><c>true</c> if a capital after the code point starts a new word; otherwise, <c>false</c>.</returns>
	/// <remarks>
	/// A lowercase letter with no uppercase form takes the case of the nearest letter before it in the
	/// same word, so <c>"STRAßE"</c> does not break before the <c>"E"</c> while <c>"großFoo"</c> still
	/// breaks before the <c>"F"</c>. With no such letter, as in <c>"ﬁLE"</c>, it does not end a word.
	/// </remarks>
	private static bool EndsWordThatIsNotUppercase(string input, int index)
	{
		if (!IsLowercaseWithNoUppercase(input, index))
		{
			return !char.IsUpper(input, index);
		}

		for (int i = index; i > 0;)
		{
			i -= i >= 2 && char.IsSurrogatePair(input, i - 2) ? 2 : 1;

			if (!IsLowercaseWithNoUppercase(input, i))
			{
				return char.IsLetter(input, i) && !char.IsUpper(input, i);
			}
		}

		return false;
	}

	/// <summary>
	/// Returns a copy of this string, trimmed and with runs of spaces collapsed to one, with the first character converted to lowercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to lowercase.</returns>
	/// <remarks>
	/// The string is trimmed before the first character is chosen, so leading whitespace does not
	/// take the place of the first letter.
	/// The first character is the first code point, so a letter outside the Basic Multilingual
	/// Plane is case-mapped as a whole rather than through its high surrogate alone, which would
	/// leave it unchanged.
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Lowercasing the first character is the point of this method.")]
	public static string ToLowercaseFirstChar(this string input)
	{
		Ensure.NotNull(input);
		return MapFirstCodePoint(CollapseSpaces(input).Trim(), static first => first.ToLowerInvariant());
	}

	/// <summary>
	/// Returns a copy of this string, trimmed and with runs of spaces collapsed to one, with the first character converted to uppercase.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string with the first character converted to uppercase.</returns>
	/// <remarks>
	/// The string is trimmed before the first character is chosen, so leading whitespace does not
	/// take the place of the first letter.
	/// The first character is the first code point, for the same reason as in
	/// <see cref="ToLowercaseFirstChar(string)"/>.
	/// </remarks>
	public static string ToUppercaseFirstChar(this string input)
	{
		Ensure.NotNull(input);
		return MapFirstCodePoint(CollapseSpaces(input).Trim(), static first => first.ToUpperInvariant());
	}

	/// <summary>
	/// Applies <paramref name="map"/> to the first code point of <paramref name="input"/>, leaving the rest unchanged.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <param name="map">The mapping to apply to the first code point, given as a string of one or two UTF-16 code units.</param>
	/// <returns>A new string with the first code point mapped, or <paramref name="input"/> if it is empty.</returns>
	private static string MapFirstCodePoint(string input, Func<string, string> map)
	{
		if (input.Length == 0)
		{
			return input;
		}

		int length = CodePointLength(input, 0);
#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
		return map(input.Substring(0, length)) + input.Substring(length);
#pragma warning restore IDE0057
#else
		return map(input[..length]) + input[length..];
#endif
	}

	/// <summary>
	/// Lowercases every word whose letters are all uppercase, leaving the rest alone.
	/// </summary>
	/// <param name="input">The string to process.</param>
	/// <returns>A new string with each all-caps word lowercased.</returns>
	/// <remarks>
	/// <see cref="TextInfo.ToTitleCase(string)"/> preserves a word that is all caps, on the assumption
	/// that it is an acronym. Lowering such a word first is what normalizes it instead, and deciding
	/// this per word rather than for the whole string is what keeps a word's result independent of its
	/// neighbours.
	/// <para>
	/// A word here is a maximal run of letters and apostrophes, which is what
	/// <see cref="TextInfo.ToTitleCase(string)"/> treats as a word. Splitting only on spaces would miss
	/// the words it finds either side of a <c>'-'</c>, a <c>'.'</c> or a tab, so <c>"foo-BAR"</c> would
	/// keep <c>"BAR"</c> as an acronym while <c>"FOO-BAR"</c> normalized it.
	/// </para>
	/// </remarks>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "Lowercasing is the point: TextInfo.ToTitleCase then capitalizes the first letter of each word.")]
	private static string LowercaseAllCapsWords(string input)
	{
		StringBuilder builder = new(input.Length);
		int i = 0;

		while (i < input.Length)
		{
			if (!IsWordCharacter(input, i))
			{
				builder.Append(input[i]);
				i++;
				continue;
			}

			int wordStart = i;

			while (i < input.Length && IsWordCharacter(input, i))
			{
				i += CodePointLength(input, i);
			}

#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
			string word = input.Substring(wordStart, i - wordStart);
#pragma warning restore IDE0057
#else
			string word = input[wordStart..i];
#endif
			builder.Append(IsAllCaps(StemBeforeApostrophe(word)) ? word.ToLowerInvariant() : word);
		}

		return builder.ToString();
	}

	/// <summary>
	/// Returns the part of <paramref name="word"/> before its first in-word apostrophe, or the whole
	/// word if it has none.
	/// </summary>
	/// <param name="word">A word as <see cref="LowercaseAllCapsWords"/> finds it.</param>
	/// <returns>The letters that decide whether <paramref name="word"/> is all caps.</returns>
	/// <remarks>
	/// A possessive or contraction suffix is conventionally lowercase even on an acronym, so
	/// <c>"CEO's"</c> is all caps in the sense that matters. Judging it by its stem normalizes it the
	/// same way as <c>"CEO"</c> rather than preserving it as an acronym.
	/// </remarks>
	private static string StemBeforeApostrophe(string word)
	{
		int previousStart = -1;

		for (int i = 0; i < word.Length;)
		{
			int nextStart = i + CodePointLength(word, i);

			if (IsApostropheWithinWord(word, previousStart, i, nextStart))
			{
#if NETSTANDARD2_0
#pragma warning disable IDE0057 // Substring cannot be simplified in netstandard2.0
				return word.Substring(0, i);
#pragma warning restore IDE0057
#else
				return word[..i];
#endif
			}

			previousStart = i;
			i = nextStart;
		}

		return word;
	}

	/// <summary>
	/// Determines whether the code point at <paramref name="index"/> belongs to a word, as
	/// <see cref="LowercaseAllCapsWords"/> defines one: a letter or an apostrophe.
	/// </summary>
	/// <param name="input">The string to inspect.</param>
	/// <param name="index">The index of the first code unit of the code point.</param>
	/// <returns><c>true</c> if the code point is part of a word; otherwise, <c>false</c>.</returns>
	private static bool IsWordCharacter(string input, int index) =>
		char.IsLetter(input, index) || input[index] is '\'' or '\u2019';

	/// <summary>
	/// Returns a copy of this string converted to Title Case. Example: "the quick brown fox" becomes "The Quick Brown Fox".
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in Title Case.</returns>
	/// <remarks>
	/// An all-caps word is normalized rather than preserved as an acronym, so <c>"HTTP"</c> becomes
	/// <c>"Http"</c> and <c>"parse HTTP header"</c> becomes <c>"Parse Http Header"</c>. The decision is
	/// made per word, so a word converts the same way whatever else is in the string.
	/// <para>
	/// Punctuation stays attached to the word it follows, so <c>"hello, world"</c> becomes
	/// <c>"Hello, World"</c> and <c>"don't stop"</c> becomes <c>"Don't Stop"</c>. An underscore
	/// separates words, so <c>"foo_bar"</c> becomes <c>"Foo Bar"</c>.
	/// </para>
	/// </remarks>
	public static string ToTitleCase(this string input)
	{
		Ensure.NotNull(input);

		string output = input.Replace('_', ' ');
		output = SplitOnCaseChange(output, breakBeforeAnyNonLetter: false);
		output = CollapseSpaces(output).Trim();

		// TextInfo.ToTitleCase preserves words that are all caps assuming they are acronyms, so lowercase
		// them first. This is done per word rather than only when the whole string is all caps, because
		// otherwise the same word converts two different ways depending on its neighbours.
		output = LowercaseAllCapsWords(output);

		return TitleCaseKeepingTypographicApostrophes(output);
	}

	/// <summary>
	/// Applies <see cref="TextInfo.ToTitleCase(string)"/>, treating an in-word U+2019 the same as an ASCII apostrophe.
	/// </summary>
	/// <param name="input">The string to convert.</param>
	/// <returns>A new string in Title Case.</returns>
	/// <remarks>
	/// <see cref="TextInfo.ToTitleCase(string)"/> keeps the letter after an ASCII <c>'</c> lowercase, but
	/// treats U+2019 as a word separator, so <c>"don’t"</c> would become <c>"Don’T"</c>. Each in-word
	/// U+2019 is swapped for <c>'</c> before the call and restored at the same index afterwards, which is
	/// safe because <see cref="TextInfo.ToTitleCase(string)"/> does not change the string's length.
	/// </remarks>
	private static string TitleCaseKeepingTypographicApostrophes(string input)
	{
		char[] characters = input.ToCharArray();
		List<int> typographicApostrophes = [];
		int previousStart = -1;

		for (int i = 0; i < input.Length;)
		{
			int nextStart = i + CodePointLength(input, i);

			if (input[i] == '’' && IsApostropheWithinWord(input, previousStart, i, nextStart))
			{
				characters[i] = '\'';
				typographicApostrophes.Add(i);
			}

			previousStart = i;
			i = nextStart;
		}

		if (typographicApostrophes.Count == 0)
		{
			return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);
		}

		char[] output = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(new string(characters)).ToCharArray();

		foreach (int index in typographicApostrophes)
		{
			output[index] = '’';
		}

		return new string(output);
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

			// A lowercase letter with no uppercase form, such as "ß", is left as it is by uppercasing,
			// so it does not stop "STRAßE" from being all caps.
			if (char.IsLetter(output, i) && !char.IsUpper(output, i) && !IsLowercaseWithNoUppercase(output, i))
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
