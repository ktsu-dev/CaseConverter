// Copyright (c) ktsu.dev
// All rights reserved.
// Licensed under the MIT license.

namespace ktsu.CaseConverter.Test;

[TestClass]
public class CaseConverterTests
{
	[TestMethod]
	public void IsAllCapsShouldReturnTrueWhenStringIsAllCaps()
	{
		var input = "HELLO WORLD";
		var result = input.IsAllCaps();
		Assert.IsTrue(result);
	}

	[TestMethod]
	public void IsAllCapsShouldReturnFalseWhenStringContainsLowercase()
	{
		var input = "Hello WORLD";
		var result = input.IsAllCaps();
		Assert.IsFalse(result);
	}

	[TestMethod]
	public void IsAllCapsShouldReturnTrueWhenStringHasNoAlphabeticChars()
	{
		var input = "1234!?";
		var result = input.IsAllCaps();
		Assert.IsTrue(result, "No alpha characters should be considered 'all caps'.");
	}

	[TestMethod]
	public void ToPascalCaseShouldThrowArgumentNullExceptionWhenInputIsNull()
	{
		string? input = null;
		Assert.ThrowsException<ArgumentNullException>(() => _ = input!.ToPascalCase());
	}

	[TestMethod]
	public void ToTitleCaseShouldHandleMultipleSpaces()
	{
		var input = "  the   quick   brown   FOX  ";
		var result = input.ToTitleCase();
		Assert.AreEqual("The Quick Brown FOX", result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldReturnEmptyWhenInputIsEmpty()
	{
		var input = string.Empty;
		var result = input.ToLowercaseFirstChar();
		Assert.AreEqual(string.Empty, result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldHandleSingleCharacter()
	{
		var input = "A";
		var result = input.ToLowercaseFirstChar();
		Assert.AreEqual("a", result);
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldConvertFirstCharToLowercase()
	{
		var input = "Hello";
		var result = input.ToLowercaseFirstChar();
		Assert.AreEqual("hello", result);
	}

	[TestMethod]
	public void ToUppercaseFirstCharShouldConvertFirstCharToUppercase()
	{
		var input = "hello";
		var result = input.ToUppercaseFirstChar();
		Assert.AreEqual("Hello", result);
	}

	[TestMethod]
	public void ToTitleCaseShouldConvertToTitleCase()
	{
		var input = "the quick Brown FOX";
		var result = input.ToTitleCase();
		Assert.AreEqual("The Quick Brown FOX", result);
	}

	[TestMethod]
	public void ToPascalCaseShouldConvertToPascalCase()
	{
		var input = "the quick brown fox";
		var result = input.ToPascalCase();
		Assert.AreEqual("TheQuickBrownFox", result);
	}

	[TestMethod]
	public void ToCamelCaseShouldConvertToCamelCase()
	{
		var input = "THE QUICK BROWN FOX";
		var result = input.ToCamelCase();
		Assert.AreEqual("theQuickBrownFox", result);
	}

	[TestMethod]
	public void ToSnakeCaseShouldConvertToSnakeCase()
	{
		var input = "TheQuick BrownFox";
		var result = input.ToSnakeCase();
		Assert.AreEqual("the_quick_brown_fox", result);
	}

	[TestMethod]
	public void ToKebabCaseShouldConvertToKebabCase()
	{
		var input = "the quick brown fox";
		var result = input.ToKebabCase();
		Assert.AreEqual("the-quick-brown-fox", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldConvertToMacroCase()
	{
		var input = "the quickBrown Fox";
		var result = input.ToMacroCase();
		Assert.AreEqual("THE_QUICK_BROWN_FOX", result);
	}

	[TestMethod]
	public void ToPascalCaseShouldCollapseSpacesAndBeTrimmed()
	{
		var input = "  the   quick  brown   fox  ";
		var result = input.ToPascalCase();
		Assert.AreEqual("TheQuickBrownFox", result);
	}

	[TestMethod]
	public void ToCamelCaseShouldCollapseSpacesAndBeTrimmed()
	{
		var input = "  THE   QUICK  BROWN   FOX  ";
		var result = input.ToCamelCase();
		Assert.AreEqual("theQuickBrownFox", result);
	}

	[TestMethod]
	public void ToSnakeCaseShouldCollapseSpacesAndBeTrimmed()
	{
		var input = "  the   quick  brown   fox  ";
		var result = input.ToSnakeCase();
		Assert.AreEqual("the_quick_brown_fox", result);
	}

	[TestMethod]
	public void ToKebabCaseShouldCollapseSpacesAndBeTrimmed()
	{
		var input = "  the   quick  brown   fox  ";
		var result = input.ToKebabCase();
		Assert.AreEqual("the-quick-brown-fox", result);
	}

	[TestMethod]
	public void ToMacroCaseShouldCollapseSpacesAndBeTrimmed()
	{
		var input = "  the   quick  brown   fox  ";
		var result = input.ToMacroCase();
		Assert.AreEqual("THE_QUICK_BROWN_FOX", result);
	}
}
