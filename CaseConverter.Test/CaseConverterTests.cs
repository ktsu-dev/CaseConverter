// Copyright (c) 2023-2026 ktsu-dev contributors

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]

namespace ktsu.CaseConverter.Test;

[TestClass]
public class CaseConverterTests
{
	[TestMethod]
	public void IsAllCapsShouldReturnTrueWhenStringIsAllCaps()
	{
		string input = "HELLO WORLD";
		bool result = input.IsAllCaps();
		Assert.IsTrue(result, "All uppercase string should be detected as all caps.");
	}

	[TestMethod]
	public void IsAllCapsShouldReturnFalseWhenStringContainsLowercase()
	{
		string input = "Hello WORLD";
		bool result = input.IsAllCaps();
		Assert.IsFalse(result, "String containing lowercase characters should not be detected as all caps.");
	}

	[TestMethod]
	public void IsAllCapsShouldReturnTrueWhenStringHasNoAlphabeticChars()
	{
		string input = "1234!?";
		bool result = input.IsAllCaps();
		Assert.IsTrue(result, "No alpha characters should be considered 'all caps'.");
	}

	[TestMethod]
	public void ToPascalCaseShouldThrowArgumentNullExceptionWhenInputIsNull()
	{
		string? input = null;
		Assert.ThrowsExactly<ArgumentNullException>(() => _ = input!.ToPascalCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldHandleMultipleSpaces()
	{
		string input = "  the   quick   brown   FOX  ";
		string result = input.ToTitleCase();

		// "FOX" is normalized rather than kept as an acronym, the same as it would be on its own.
		Assert.AreEqual("The Quick Brown Fox", result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldReturnEmptyWhenInputIsEmpty()
	{
		string input = string.Empty;
		string result = input.ToLowercaseFirstChar();
		Assert.AreEqual(string.Empty, result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldHandleSingleCharacter()
	{
		string input = "A";
		string result = input.ToLowercaseFirstChar();
		Assert.AreEqual("a", result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldConvertFirstCharToLowercase()
	{
		string input = "Hello";
		string result = input.ToLowercaseFirstChar();
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void ToUppercaseFirstCharShouldConvertFirstCharToUppercase()
	{
		string input = "hello";
		string result = input.ToUppercaseFirstChar();
		Assert.AreEqual("Hello", result);
	}

	[TestMethod]
	public void ToTitleCaseShouldConvertToTitleCase()
	{
		string input = "the quick Brown FOX";
		string result = input.ToTitleCase();

		// "FOX" is normalized rather than kept as an acronym, the same as it would be on its own.
		Assert.AreEqual("The Quick Brown Fox", result);
	}

	// An all-caps word used to be normalized only when the entire string was all caps, because
	// ToTitleCase tested IsAllCaps against the whole string. The same token therefore converted two
	// different ways depending on its neighbours: "MAX_SIZE" gave "MaxSize" but "set MAX_SIZE" gave
	// "SetMAXSIZE". Each pair below measures a word alone and beside a lowercase one, so a regression
	// to whole-string reasoning fails the second assertion of the pair while the first still passes.

	[TestMethod]
	public void ToTitleCaseShouldNormalizeAnAllCapsWordIndependentlyOfItsNeighbours()
	{
		Assert.AreEqual("Http", "HTTP".ToTitleCase());
		Assert.AreEqual("Parse Http Header", "parse HTTP header".ToTitleCase());
	}

	// ToTitleCase used to split before every non-letter, so punctuation became a word of its own and
	// TextInfo.ToTitleCase capitalized the letter after it.

	[TestMethod]
	public void ToTitleCaseShouldKeepACommaWithTheWordBeforeIt()
	{
		Assert.AreEqual("Hello, World", "hello, world".ToTitleCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldKeepAnApostropheInsideItsWord()
	{
		Assert.AreEqual("Don't Stop", "don't stop".ToTitleCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldTreatAnUnderscoreAsAWordSeparator()
	{
		Assert.AreEqual("Foo Bar", "foo_bar".ToTitleCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldKeepOtherPunctuationInPlace()
	{
		Assert.AreEqual("Part 1: Setup", "part 1: setup".ToTitleCase());
		Assert.AreEqual("What's New?", "what's new?".ToTitleCase());
		Assert.AreEqual("(Hello) World", "(Hello) world".ToTitleCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldStillSplitOnCaseChangesAndDigits()
	{
		Assert.AreEqual("Foo Bar", "fooBar".ToTitleCase());
		Assert.AreEqual("Xml Doc", "XMLDoc".ToTitleCase());
		Assert.AreEqual("Abc 123", "abc123".ToTitleCase());
	}

	[TestMethod]
	public void ToPascalCaseShouldNormalizeAnAllCapsWordIndependentlyOfItsNeighbours()
	{
		Assert.AreEqual("MaxSize", "MAX_SIZE".ToPascalCase());
		Assert.AreEqual("SetMaxSize", "set MAX_SIZE".ToPascalCase());
	}

	[TestMethod]
	public void ToCamelCaseShouldNormalizeAnAllCapsWordIndependentlyOfItsNeighbours()
	{
		Assert.AreEqual("url", "URL".ToCamelCase());
		Assert.AreEqual("myUrlHandler", "my URL handler".ToCamelCase());
	}

	[TestMethod]
	public void ToPascalCaseShouldMatchTheAcronymExampleInTheReadme()
	{
		// README.md has documented this result since before the neighbour dependence was found, while
		// the library actually produced "APIResponseURL". Pinning it keeps the two from drifting again.
		Assert.AreEqual("ApiResponseUrl", "API_response_URL".ToPascalCase());
	}

	[TestMethod]
	public void ToPascalCaseShouldAgreeWithToMacroCaseOnWordBoundaries()
	{
		// The macro and snake paths never had the neighbour dependence, so they are the reference the
		// title-case-derived converters are brought back into agreement with.
		Assert.AreEqual("SET_MAX_SIZE", "set MAX_SIZE".ToMacroCase());
		Assert.AreEqual("SetMaxSize", "set MAX_SIZE".ToPascalCase());
	}

	[TestMethod]
	public void ToPascalCaseShouldConvertToPascalCase()
	{
		string input = "the quick brown fox";
		string result = input.ToPascalCase();
		Assert.AreEqual("TheQuickBrownFox", result);
	}

	[TestMethod]
	public void ToCamelCaseShouldConvertToCamelCase()
	{
		string input = "THE QUICK BROWN FOX";
		string result = input.ToCamelCase();
		Assert.AreEqual("theQuickBrownFox", result);
	}

	[TestMethod]
	public void ToSnakeCaseShouldConvertToSnakeCase()
	{
		string input = "TheQuick BrownFox";
		string result = input.ToSnakeCase();
		Assert.AreEqual("the_quick_brown_fox", result);
	}

	[TestMethod]
	public void ToKebabCaseShouldConvertToKebabCase()
	{
		string input = "the quick brown fox";
		string result = input.ToKebabCase();
		Assert.AreEqual("the-quick-brown-fox", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldConvertToMacroCase()
	{
		string input = "the quickBrown Fox";
		string result = input.ToMacroCase();
		Assert.AreEqual("THE_QUICK_BROWN_FOX", result);
	}

	[TestMethod]
	public void ToPascalCaseShouldCollapseSpacesAndBeTrimmed()
	{
		string input = "  the   quick  brown   fox  ";
		string result = input.ToPascalCase();
		Assert.AreEqual("TheQuickBrownFox", result);
	}

	[TestMethod]
	public void ToCamelCaseShouldCollapseSpacesAndBeTrimmed()
	{
		string input = "  THE   QUICK  BROWN   FOX  ";
		string result = input.ToCamelCase();
		Assert.AreEqual("theQuickBrownFox", result);
	}

	[TestMethod]
	public void ToSnakeCaseShouldCollapseSpacesAndBeTrimmed()
	{
		string input = "  the   quick  brown   fox  ";
		string result = input.ToSnakeCase();
		Assert.AreEqual("the_quick_brown_fox", result);
	}

	[TestMethod]
	public void ToKebabCaseShouldCollapseSpacesAndBeTrimmed()
	{
		string input = "  the   quick  brown   fox  ";
		string result = input.ToKebabCase();
		Assert.AreEqual("the-quick-brown-fox", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldCollapseSpacesAndBeTrimmed()
	{
		string input = "  the   quick  brown   fox  ";
		string result = input.ToMacroCase();
		Assert.AreEqual("THE_QUICK_BROWN_FOX", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldCollapseSpacesAndBeTrimmedExtendedUnicode()
	{
		string input = "  den   raske  høye  brune   reven  ";
		string result = input.ToMacroCase();
		Assert.AreEqual("DEN_RASKE_HØYE_BRUNE_REVEN", result);
	}

	[TestMethod]
	public void ToSnakeCaseShouldNotIncludeLeadingOrTrailingSeparators()
	{
		string input = "_privateField-";
		string result = input.ToSnakeCase();
		Assert.AreEqual("private_field", result);
	}

	[TestMethod]
	public void ToKebabCaseShouldNotIncludeLeadingOrTrailingSeparators()
	{
		string input = "_privateField-";
		string result = input.ToKebabCase();
		Assert.AreEqual("private-field", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldNotIncludeLeadingOrTrailingSeparators()
	{
		string input = "_privateField-";
		string result = input.ToMacroCase();
		Assert.AreEqual("PRIVATE_FIELD", result);
	}

	// U+10400 DESERET CAPITAL LONG I and U+10428 DESERET SMALL LONG I are letters outside the
	// Basic Multilingual Plane, so each is a surrogate pair in UTF-16. They are a cased pair,
	// which lets these tests check case mapping as well as preservation.
	private const string DeseretCapitalLongI = "\U00010400";
	private const string DeseretSmallLongI = "\U00010428";

	[TestMethod]
	public void ToSnakeCaseShouldPreserveLettersOutsideTheBasicMultilingualPlane()
	{
		string input = $"{DeseretCapitalLongI}abc";
		string result = input.ToSnakeCase();
		Assert.AreEqual($"{DeseretSmallLongI}abc", result, "A letter outside the BMP must be lowercased, not deleted.");
	}

	[TestMethod]
	public void ToPascalCaseShouldPreserveLettersOutsideTheBasicMultilingualPlane()
	{
		string input = $"set {DeseretCapitalLongI}abc";
		string result = input.ToPascalCase();
		Assert.AreEqual($"Set{DeseretCapitalLongI}abc", result, "A letter outside the BMP must survive the conversion.");
	}

	[TestMethod]
	public void ToMacroCaseShouldTreatAnAstralUppercaseLetterAsAWordBoundary()
	{
		string input = $"abc{DeseretCapitalLongI}def";
		string result = input.ToMacroCase();

		// "abcXdef" breaks before the "X"; an uppercase letter outside the BMP must behave the same.
		Assert.AreEqual($"ABC_{DeseretCapitalLongI}DEF", result);
		Assert.AreEqual("ABC_XDEF", "abcXdef".ToMacroCase(), "The BMP analogue this case is matched against.");
	}

	[TestMethod]
	public void IsAllCapsShouldReturnFalseForAnAstralLowercaseLetter()
	{
		string input = $"HELLO {DeseretSmallLongI}";
		bool result = input.IsAllCaps();
		Assert.IsFalse(result, "A lowercase letter outside the BMP must count as lowercase, not be skipped.");
	}

	[TestMethod]
	public void ToSnakeCaseShouldStillDropAstralCharactersThatAreNotLetters()
	{
		// U+1F600 GRINNING FACE is a surrogate pair but not a letter, so it is a separator like
		// any other non-alphanumeric. This pins the boundary of the surrogate-pair handling.
		string input = "emoji \U0001F600 here";
		string result = input.ToSnakeCase();
		Assert.AreEqual("emoji_here", result);
	}
}
