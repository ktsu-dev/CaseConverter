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
		Assert.AreEqual("The Quick Brown FOX", result);
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
		Assert.AreEqual("The Quick Brown FOX", result);
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
