// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

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
}
