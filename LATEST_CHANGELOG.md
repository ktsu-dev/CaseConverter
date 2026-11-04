## v1.9.2 (patch)

Changes since v1.9.1:

- Split a capitalised word after a capital followed by ß or a ligature [patch] (#141) (matt-edmondson)
- Look past a decomposed accent when lowercasing a capital after an apostrophe [patch] (#146) (matt-edmondson)
- Start a new word at "Is" or "As" after an acronym [patch] (#157) (matt-edmondson)
- Keep a plural's "s" in the word after a digit and an apostrophe [patch] (#155) (matt-edmondson)
- Keep an all-caps possessive after a lowercased capital run in its word [patch] (#153) (matt-edmondson)
- Title-case a word-initial Armenian ligature, and step the scan by code point [patch] (#152) (matt-edmondson)
- Add CompatibilitySuppressions.xml for the Polyfill shim drift [patch] (#151) (matt-edmondson)
- Keep a zero-width joiner, non-joiner or soft hyphen inside its word [patch] (#147) (matt-edmondson)
- Keep a word-initial "ʼN" together and capitalised [patch] (#143) (matt-edmondson)
- Convert long runs of letters with no uppercase form in linear time (Claude)
- Keep a possessive after a lowercased all-caps run in the same word [patch] (Claude)
- Title-case a word that starts with a ligature or "ŉ" [patch] (Claude)
- Pull letter casing and the lowercase-run check out of ReplaceNonAlphaNumericWithSpace (Claude)
- Keep a run of capitals after a caseless letter in its word [patch] (Claude)
- Keep a lowercase letter, apostrophe and all-caps run in one word [patch] (Claude)
- Lowercase a word-final capital sigma to final sigma [patch] (Claude)
- Lowercase a two-letter "s" word after a one-letter word in Pascal and camel case [patch] (Claude)
- Split an acronym from a following decomposed accented word [patch] (Claude)
- Keep an apostrophe after a decomposed accent inside its word [patch] (Claude)

