## v1.9.0 (minor)

Changes since v1.8.0:

- Normalize an acronym joined to Han, kana or Hangul text [minor] ([@Claude](https://github.com/Claude))
- Lowercase a one-letter word that follows another in Pascal and camel case [patch] ([@Claude](https://github.com/Claude))
- Keep lowercase letters as they are in Snake, Kebab and Camel case [patch] ([@Claude](https://github.com/Claude))
- Keep combining marks and non-ASCII digits in Pascal, Camel, Snake, Kebab and Macro case [patch] ([@Claude](https://github.com/Claude))
- Keep a letter after a digit in the same word in Title, Pascal and Camel case [patch] ([@Claude](https://github.com/Claude))
- Merge remote-tracking branch 'origin/main' into fix/96-capitalised-apostrophe-names ([@Claude](https://github.com/Claude))
- Move the post-apostrophe lowercasing into a helper to satisfy S3776 ([@Claude](https://github.com/Claude))
- Lower the letter after an apostrophe with a ternary to keep cognitive complexity in bounds ([@Claude](https://github.com/Claude))
- Keep a capitalised apostrophe name like "O'Neil" in one word [patch] ([@Claude](https://github.com/Claude))
- Keep a plural acronym in one piece: "APIs" no longer splits as "AP Is" [patch] ([@Claude](https://github.com/Claude))
- Case-map İ and ı, which the invariant mapping leaves alone [patch] ([@Claude](https://github.com/Claude))
- Move CI onto the shared ci-shared.yml pipeline ([@Claude](https://github.com/Claude))
- Keep an all-caps word containing ß in one piece, so ToMacroCase is idempotent [patch] ([@Claude](https://github.com/Claude))
- Keep a possessive acronym in one piece in Pascal/Camel/Snake/Kebab/Macro [patch] ([@Claude](https://github.com/Claude))
- Normalize a possessive all-caps word in ToTitleCase the same as the word alone [patch] ([@Claude](https://github.com/Claude))
- Keep the letter after a typographic apostrophe lowercase in ToTitleCase [patch] ([@Claude](https://github.com/Claude))
- Move the #82 test beside the other ToTitleCase all-caps tests ([@Claude](https://github.com/Claude))
- Normalize an all-caps word joined by '-', '.' or a tab in ToTitleCase [patch] ([@Claude](https://github.com/Claude))
- Keep an in-word apostrophe from splitting the word in Pascal/Camel/Snake/Kebab/Macro [patch] ([@Claude](https://github.com/Claude))

