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

	[TestMethod]
	[DataRow("foo-BAR", "Foo-Bar")]
	[DataRow("FOO-BAR", "Foo-Bar")]
	[DataRow("API-key", "Api-Key")]
	[DataRow("foo.BAR", "Foo.Bar")]
	[DataRow("HELLO\tworld", "Hello\tWorld")]
	[DataRow("HELLO world", "Hello World")]
	[DataRow("DON'T stop", "Don't Stop")]
	public void ToTitleCaseShouldNormalizeAllCapsWordsJoinedByPunctuationOrTabs(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToTitleCase());
	}

	// The lowercase "s" of a possessive made "CEO's" count as mixed case, so it was preserved as an
	// acronym while "CEO" on its own was normalized.

	[TestMethod]
	[DataRow("the CEO office", "The Ceo Office")]
	[DataRow("the CEO's office", "The Ceo's Office")]
	[DataRow("NASA's mission", "Nasa's Mission")]
	[DataRow("O'Neil", "O'neil")]
	public void ToTitleCaseShouldNormalizeAPossessiveAllCapsWordLikeTheWordAlone(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToTitleCase());
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

	// TextInfo.ToTitleCase only keeps the letter after an ASCII apostrophe lowercase; it treated the
	// typographic apostrophe U+2019 as a separator and gave "Don’T Stop".

	[TestMethod]
	[DataRow("don’t stop", "Don’t Stop")]
	[DataRow("it’s fine", "It’s Fine")]
	[DataRow("’quoted’ word", "’Quoted’ Word")]
	public void ToTitleCaseShouldKeepATypographicApostropheInsideItsWord(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToTitleCase());
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

	// A letter after a digit does not start a word: snake_case keeps "1st" whole, so Title, Pascal and
	// Camel case must too, or converting through one case into another changes the words.

	[TestMethod]
	[DataRow("1st place", "1st Place", "1stPlace", "1stPlace", "1st_place")]
	[DataRow("abc123def", "Abc 123def", "Abc123def", "abc123def", "abc_123def")]
	[DataRow("md5hash", "Md 5hash", "Md5hash", "md5hash", "md_5hash")]
	[DataRow("3d model", "3d Model", "3dModel", "3dModel", "3d_model")]
	public void ALetterAfterADigitShouldNotStartANewWord(string input, string title, string pascal, string camel, string snake)
	{
		Assert.AreEqual(title, input.ToTitleCase());
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToPascalCase().ToSnakeCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToCamelCase().ToSnakeCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToTitleCase().ToSnakeCase());
	}

	// A plural acronym ends in a lowercase "s", which the acronym-tail rule ("XMLDoc" -> "XML Doc") used
	// to read as the start of a capitalised word, splitting "APIs" into "AP Is".

	[TestMethod]
	[DataRow("APIs", "Apis", "apis", "apis", "apis", "APIS", "Apis")]
	[DataRow("URLs", "Urls", "urls", "urls", "urls", "URLS", "Urls")]
	[DataRow("getIDs", "GetIds", "getIds", "get_ids", "get-ids", "GET_IDS", "Get Ids")]
	[DataRow("the URLs list", "TheUrlsList", "theUrlsList", "the_urls_list", "the-urls-list", "THE_URLS_LIST", "The Urls List")]
	[DataRow("PDFsAndDOCs", "PdfsAndDocs", "pdfsAndDocs", "pdfs_and_docs", "pdfs-and-docs", "PDFS_AND_DOCS", "Pdfs And Docs")]
	[DataRow("GUIDs_2", "Guids2", "guids2", "guids_2", "guids-2", "GUIDS_2", "Guids 2")]
	public void APluralAcronymShouldStayOneWord(string input, string pascal, string camel, string snake, string kebab, string macro, string title)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(kebab, input.ToKebabCase());
		Assert.AreEqual(macro, input.ToMacroCase());
		Assert.AreEqual(title, input.ToTitleCase());
	}

	[TestMethod]
	[DataRow("HTTPServer", "http_server")]
	[DataRow("XMLDoc", "xml_doc")]
	[DataRow("HTMLParser", "html_parser")]
	[DataRow("ABCdef", "ab_cdef")]
	[DataRow("IOStream", "io_stream")]
	[DataRow("URLsList", "urls_list")]
	public void AnAcronymFollowedByAWordShouldStillSplit(string input, string snake)
	{
		Assert.AreEqual(snake, input.ToSnakeCase());
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
	public void ToCamelCaseShouldLowercaseAnAstralFirstLetter()
	{
		string input = $"{DeseretCapitalLongI}abc";
		string result = input.ToCamelCase();
		Assert.AreEqual($"{DeseretSmallLongI}abc", result, "A first letter outside the BMP must be lowercased like any other.");
	}

	[TestMethod]
	public void ToCamelCaseShouldLowercaseAnAstralAllCapsFirstWord()
	{
		string input = $"{DeseretCapitalLongI}{DeseretCapitalLongI} foo";
		string result = input.ToCamelCase();
		Assert.AreEqual($"{DeseretSmallLongI}{DeseretSmallLongI}Foo", result);
		Assert.AreEqual("xxFoo", "XX foo".ToCamelCase(), "The BMP analogue this case is matched against.");
	}

	[TestMethod]
	public void ToLowercaseFirstCharShouldLowercaseAnAstralFirstLetter()
	{
		string input = $"{DeseretCapitalLongI}abc";
		string result = input.ToLowercaseFirstChar();
		Assert.AreEqual($"{DeseretSmallLongI}abc", result);
	}

	[TestMethod]
	public void ToUppercaseFirstCharShouldUppercaseAnAstralFirstLetter()
	{
		string input = $"{DeseretSmallLongI}abc";
		string result = input.ToUppercaseFirstChar();
		Assert.AreEqual($"{DeseretCapitalLongI}abc", result);
	}

	[TestMethod]
	[DataRow(" hello", "Hello")]
	[DataRow("\thello", "Hello")]
	[DataRow("  hello  ", "Hello")]
	public void ToUppercaseFirstCharShouldSkipLeadingWhitespace(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToUppercaseFirstChar());
	}

	[TestMethod]
	[DataRow(" Hello", "hello")]
	[DataRow("\tHello", "hello")]
	[DataRow("  Hello  ", "hello")]
	public void ToLowercaseFirstCharShouldSkipLeadingWhitespace(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToLowercaseFirstChar());
	}

	[TestMethod]
	public void FirstCharHelpersShouldCollapseRunsOfSpaces()
	{
		// Documented behaviour: both helpers collapse runs of spaces to one.
		Assert.AreEqual("A b", "a  b".ToUppercaseFirstChar());
		Assert.AreEqual("a b", "A  b".ToLowercaseFirstChar());
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

	[TestMethod]
	[DataRow("don't stop", "DontStop", "dontStop", "dont_stop", "dont-stop", "DONT_STOP")]
	[DataRow("don\u2019t stop", "DontStop", "dontStop", "dont_stop", "dont-stop", "DONT_STOP")]
	[DataRow("o'neil", "Oneil", "oneil", "oneil", "oneil", "ONEIL")]
	[DataRow("o\u2019neil", "Oneil", "oneil", "oneil", "oneil", "ONEIL")]
	[DataRow("O'Neil", "Oneil", "oneil", "oneil", "oneil", "ONEIL")]
	[DataRow("O\u2019Neil", "Oneil", "oneil", "oneil", "oneil", "ONEIL")]
	[DataRow("O'NEIL", "Oneil", "oneil", "oneil", "oneil", "ONEIL")]
	[DataRow("D'Angelo", "Dangelo", "dangelo", "dangelo", "dangelo", "DANGELO")]
	[DataRow("d'angelo", "Dangelo", "dangelo", "dangelo", "dangelo", "DANGELO")]
	[DataRow("Don'T stop", "DontStop", "dontStop", "dont_stop", "dont-stop", "DONT_STOP")]
	[DataRow("O'Neil's car", "OneilsCar", "oneilsCar", "oneils_car", "oneils-car", "ONEILS_CAR")]
	[DataRow("McDonald's menu", "McDonaldsMenu", "mcDonaldsMenu", "mc_donalds_menu", "mc-donalds-menu", "MC_DONALDS_MENU")]
	[DataRow("DON'T stop", "DontStop", "dontStop", "dont_stop", "dont-stop", "DONT_STOP")]
	[DataRow("API's", "Apis", "apis", "apis", "apis", "APIS")]
	[DataRow("CEO's office", "CeosOffice", "ceosOffice", "ceos_office", "ceos-office", "CEOS_OFFICE")]
	[DataRow("the CEO's office", "TheCeosOffice", "theCeosOffice", "the_ceos_office", "the-ceos-office", "THE_CEOS_OFFICE")]
	[DataRow("NASA's mission", "NasasMission", "nasasMission", "nasas_mission", "nasas-mission", "NASAS_MISSION")]
	[DataRow("CEO\u2019s office", "CeosOffice", "ceosOffice", "ceos_office", "ceos-office", "CEOS_OFFICE")]
	public void ApostropheWithinWordShouldNotSplitIt(string input, string pascal, string camel, string snake, string kebab, string macro)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(kebab, input.ToKebabCase());
		Assert.AreEqual(macro, input.ToMacroCase());
	}

	[TestMethod]
	[DataRow("'quoted' word", "quoted_word")]
	[DataRow("\u2019quoted\u2019 word", "quoted_word")]
	[DataRow("rock 'n' roll", "rock_n_roll")]
	[DataRow("80's music", "80_s_music")]
	public void ApostropheNotBetweenLettersShouldStillSeparateWords(string input, string expected)
	{
		Assert.AreEqual(expected, input.ToSnakeCase());
	}

	[TestMethod]
	[DataRow("O'Neil", "o'neil")]
	[DataRow("D'Angelo", "d'angelo")]
	[DataRow("Don'T", "don't")]
	public void ACapitalisedApostropheNameShouldConvertLikeItsLowercaseSpelling(string input, string lowercase)
	{
		Assert.AreEqual(lowercase.ToSnakeCase(), input.ToSnakeCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToTitleCase().ToSnakeCase());
	}

	// "ß" and "ﬁ" are lowercase letters with no single-character uppercase form, so they survive
	// ToMacroCase. They used to read as lowercase when that output was converted again, which split
	// "STRAßE" into "STR Aß E" and kept IsAllCaps from recognizing it.

	[TestMethod]
	[DataRow("straße")]
	[DataRow("maßnahmeLimit")]
	[DataRow("MAX_GRÖßE")]
	[DataRow("ﬁle")]
	public void ToMacroCaseShouldBeIdempotentForLowercaseLettersWithNoUppercaseForm(string input)
	{
		string once = input.ToMacroCase();
		Assert.AreEqual(once, once.ToMacroCase());
	}

	[TestMethod]
	public void AnAllCapsWordContainingSharpSShouldStayOneWord()
	{
		Assert.AreEqual("STRAßE", "straße".ToMacroCase());
		Assert.AreEqual("straße", "STRAßE".ToSnakeCase());
		Assert.AreEqual("Straße", "STRAßE".ToPascalCase());
		Assert.AreEqual("Straße", "STRAßE".ToTitleCase());
		Assert.AreEqual("maßnahme", "MAßNAHME".ToSnakeCase());
		Assert.AreEqual("maxGröße", "MAX_GRÖßE".ToCamelCase());
		Assert.AreEqual("maßnahme_limit", "maßnahmeLimit".ToMacroCase().ToSnakeCase());
		Assert.AreEqual("ﬁLE", "ﬁle".ToMacroCase().ToMacroCase());
	}

	[TestMethod]
	public void ALowercaseWordEndingInSharpSShouldStillSplitBeforeACapital()
	{
		Assert.AreEqual("groß_foo", "großFoo".ToSnakeCase());
	}

	[TestMethod]
	public void IsAllCapsShouldIgnoreLowercaseLettersWithNoUppercaseForm()
	{
		Assert.IsTrue("STRAßE".IsAllCaps());
		Assert.IsFalse("Straße".IsAllCaps());
	}

	// .NET's invariant casing leaves "İ" (U+0130) and "ı" (U+0131) unmapped. Unicode's simple mappings
	// are "İ" -> "i" and "ı" -> "I", and without them the output breaks its own case style's rule.

	[TestMethod]
	[DataRow("İzmir city", "İzmirCity", "izmirCity", "izmir_city", "izmir-city", "İZMIR_CITY")]
	[DataRow("İZMİR", "İzmir", "izmir", "izmir", "izmir", "İZMİR")]
	public void DottedCapitalIShouldBeCaseMapped(string input, string pascal, string camel, string snake, string kebab, string macro)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(kebab, input.ToKebabCase());
		Assert.AreEqual(macro, input.ToMacroCase());
	}

	[TestMethod]
	[DataRow("kırmızı", "Kırmızı", "KIRMIZI")]
	[DataRow("ılık su", "IlıkSu", "ILIK_SU")]
	public void DotlessSmallIShouldBeCaseMapped(string input, string pascal, string macro)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(macro, input.ToMacroCase());
	}

	[TestMethod]
	public void DottedCapitalIAndDotlessSmallIShouldBeCaseMappedInTheFirstCharHelpersAndTitleCase()
	{
		Assert.AreEqual("istanbul", "İstanbul".ToLowercaseFirstChar());
		Assert.AreEqual("Ilık su", "ılık su".ToUppercaseFirstChar());
		Assert.AreEqual("Ilık Su", "ılık su".ToTitleCase());
		Assert.AreEqual("İzmir", "İZMİR".ToTitleCase());
	}

	[TestMethod]
	[DataRow("kırmızı")]
	[DataRow("İzmir city")]
	[DataRow("ılık su")]
	public void ToMacroCaseShouldBeIdempotentForDottedAndDotlessI(string input)
	{
		string once = input.ToMacroCase();
		Assert.AreEqual(once, once.ToMacroCase());
	}

	// Uppercasing and lowering again is not an identity for some lowercase letters: final sigma "ς"
	// comes back as "σ", and the micro sign "µ" (U+00B5) as the Greek "μ" (U+03BC).

	[TestMethod]
	[DataRow("λόγος", "λόγος", "λόγος")]
	[DataRow("already_snake_ς", "already_snake_ς", "already-snake-ς")]
	[DataRow("µs delay", "µs_delay", "µs-delay")]
	[DataRow("ſtraße", "ſtraße", "ſtraße")]
	[DataRow("kırmızı", "kırmızı", "kırmızı")]
	public void ToSnakeCaseAndToKebabCaseShouldKeepLowercaseLetters(string input, string snake, string kebab)
	{
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(kebab, input.ToKebabCase());
		Assert.AreEqual(snake, snake.ToSnakeCase());
	}

	[TestMethod]
	[DataRow("µs delay", "µsDelay")]
	[DataRow("ςx", "ςx")]
	[DataRow("ılık su", "ılıkSu")]
	public void ToCamelCaseShouldKeepALowercaseFirstLetter(string input, string camel) =>
		Assert.AreEqual(camel, input.ToCamelCase());

	[TestMethod]
	public void IsAllCapsShouldTreatDotlessSmallIAsLowercase()
	{
		Assert.IsFalse("ı".IsAllCaps());
		Assert.IsFalse("KıRMıZı".IsAllCaps());
		Assert.IsTrue("İZMİR".IsAllCaps());
	}

	// A combining mark belongs to the letter before it, and a decimal digit from any script is a digit.
	// Both used to be replaced with a space, which broke Devanagari words apart at every vowel sign and
	// dropped the accents of decomposed (NFD) Latin text and the digits of Arabic-Indic numbers.

	[TestMethod]
	[DataRow("हिन्दी भाषा", "हिन्दीभाषा", "हिन्दीभाषा", "हिन्दी_भाषा", "हिन्दी-भाषा", "हिन्दी_भाषा")]
	[DataRow("éclair", "Éclair", "éclair", "éclair", "éclair", "ÉCLAIR")]
	[DataRow("café au lait", "CaféAuLait", "caféAuLait", "café_au_lait", "café-au-lait", "CAFÉ_AU_LAIT")]
	[DataRow("x١٢٣ y", "X١٢٣Y", "x١٢٣Y", "x_١٢٣_y", "x-١٢٣-y", "X_١٢٣_Y")]
	[DataRow("x१२३ y", "X१२३Y", "x१२३Y", "x_१२३_y", "x-१२३-y", "X_१२३_Y")]
	public void CombiningMarksAndNonAsciiDigitsShouldBeKept(string input, string pascal, string camel, string snake, string kebab, string macro)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(snake, input.ToSnakeCase());
		Assert.AreEqual(kebab, input.ToKebabCase());
		Assert.AreEqual(macro, input.ToMacroCase());
		Assert.AreEqual(snake, input.ToMacroCase().ToSnakeCase());
	}

	// A caseless script such as Devanagari has no capital to mark a word in PascalCase, so only cased
	// text can round-trip through it.

	[TestMethod]
	[DataRow("e\u0301clair")]
	[DataRow("cafe\u0301 au lait")]
	[DataRow("x\u0661\u0662\u0663 y")]
	public void CasedTextWithCombiningMarksOrNonAsciiDigitsShouldRoundTripThroughPascalCase(string input)
	{
		Assert.AreEqual(input.ToSnakeCase(), input.ToPascalCase().ToSnakeCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToCamelCase().ToSnakeCase());
	}

	[TestMethod]
	public void ANonAsciiDigitAfterALetterShouldStartANewWordInSnakeCase()
	{
		Assert.AreEqual("x_١٢٣y", "x١٢٣y".ToSnakeCase());
	}

	[TestMethod]
	[DataRow("ÉCLAIR", "éclair")]
	[DataRow("CAFÉ AU LAIT", "café au lait")]
	public void AnAllCapsWordWithACombiningMarkShouldConvertLikeItsLowercaseSpelling(string input, string lowercase)
	{
		Assert.AreEqual(lowercase.ToPascalCase(), input.ToPascalCase());
		Assert.AreEqual(lowercase.ToTitleCase(), input.ToTitleCase());
	}

	[TestMethod]
	public void ACapitalAfterACombiningMarkShouldStartANewWord()
	{
		Assert.AreEqual("café_bar", "caféBar".ToSnakeCase());
		Assert.AreEqual("été_xml_doc", "étéXMLDoc".ToSnakeCase());
	}

	// Adjacent one-letter words would join into a run of capitals ("VectorXY") that reads back as one
	// all-caps word, so every one-letter word after the first in a run is lowercased instead.
	[TestMethod]
	[DataRow("vector x y", "VectorXy", "vectorXy")]
	[DataRow("a b c", "Abc", "abc")]
	[DataRow("x_y", "Xy", "xy")]
	public void AdjacentOneLetterWordsShouldConvertToOutputThatConvertsBackToItself(string input, string pascal, string camel)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(pascal, input.ToPascalCase().ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(camel, input.ToCamelCase().ToCamelCase());
	}

	[TestMethod]
	public void AdjacentOneLetterWordsShouldFoldTogetherThroughPascalCase()
	{
		// The boundary between adjacent one-letter words is lost by design; a lone one is kept.
		Assert.AreEqual("vector_xy", "vector_x_y".ToPascalCase().ToSnakeCase());
		Assert.AreEqual("get_a_value", "get_a_value".ToPascalCase().ToSnakeCase());
	}

	// A caseless letter (Han, kana, Hangul) has no lowercase form, so it must not make a Latin
	// acronym joined to it look "not all caps" and skip the acronym's normalization.
	[TestMethod]
	[DataRow("API名", "Api名", "api名", "Api名")]
	[DataRow("SQL文", "Sql文", "sql文", "Sql文")]
	[DataRow("URLの取得", "Urlの取得", "urlの取得", "Urlの取得")]
	[DataRow("HTTP요청", "Http요청", "http요청", "Http요청")]
	public void AnAcronymJoinedToCaselessLettersShouldBeNormalized(string input, string pascal, string camel, string title)
	{
		Assert.AreEqual(pascal, input.ToPascalCase());
		Assert.AreEqual(camel, input.ToCamelCase());
		Assert.AreEqual(title, input.ToTitleCase());
		Assert.AreEqual(input.ToSnakeCase(), input.ToCamelCase().ToSnakeCase());
		Assert.AreEqual(input.ToPascalCase(), input.ToSnakeCase().ToPascalCase());
	}

	[TestMethod]
	public void IsAllCapsShouldIgnoreCaselessLetters()
	{
		Assert.IsTrue("API名".IsAllCaps());
		Assert.IsTrue("名".IsAllCaps());
		Assert.IsFalse("Api名".IsAllCaps());
		Assert.IsFalse("\u01C5".IsAllCaps());
	}
}
