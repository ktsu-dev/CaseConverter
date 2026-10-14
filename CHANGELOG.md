## v1.9.2 (patch)

Changes since v1.9.1:

- Split an acronym from a following decomposed accented word [patch] (Claude)
- Keep an apostrophe after a decomposed accent inside its word [patch] (Claude)

## v1.9.1 (patch)

Changes since v1.9.0:

- Merge remote-tracking branch 'origin/main' into fix/115-macro-digit-letters (Claude)
- Keep a run of capitals after a digit in the digit's word in MACRO_CASE [patch] (Claude)

## v1.9.0 (minor)

Changes since v1.8.0:

- Normalize an acronym joined to Han, kana or Hangul text [minor] (Claude)
- Lowercase a one-letter word that follows another in Pascal and camel case [patch] (Claude)
- Keep lowercase letters as they are in Snake, Kebab and Camel case [patch] (Claude)
- Keep combining marks and non-ASCII digits in Pascal, Camel, Snake, Kebab and Macro case [patch] (Claude)
- Keep a letter after a digit in the same word in Title, Pascal and Camel case [patch] (Claude)
- Merge remote-tracking branch 'origin/main' into fix/96-capitalised-apostrophe-names (Claude)
- Move the post-apostrophe lowercasing into a helper to satisfy S3776 (Claude)
- Lower the letter after an apostrophe with a ternary to keep cognitive complexity in bounds (Claude)
- Keep a capitalised apostrophe name like "O'Neil" in one word [patch] (Claude)
- Keep a plural acronym in one piece: "APIs" no longer splits as "AP Is" [patch] (Claude)
- Case-map İ and ı, which the invariant mapping leaves alone [patch] (Claude)
- Move CI onto the shared ci-shared.yml pipeline (Claude)
- Keep an all-caps word containing ß in one piece, so ToMacroCase is idempotent [patch] (Claude)
- Keep a possessive acronym in one piece in Pascal/Camel/Snake/Kebab/Macro [patch] (Claude)
- Normalize a possessive all-caps word in ToTitleCase the same as the word alone [patch] (Claude)
- Keep the letter after a typographic apostrophe lowercase in ToTitleCase [patch] (Claude)
- Move the #82 test beside the other ToTitleCase all-caps tests (Claude)
- Normalize an all-caps word joined by '-', '.' or a tab in ToTitleCase [patch] (Claude)
- Keep an in-word apostrophe from splitting the word in Pascal/Camel/Snake/Kebab/Macro [patch] (Claude)

## v1.8.9 (patch)

Changes since v1.8.8:

- Lowercase a one-letter word that follows another in Pascal and camel case [patch] (Claude)

## v1.8.9-pre.1 (prerelease)

Changes since v1.8.8:

- Bump MSTest.Sdk from 4.4.1 to 4.5.1 (dependabot[bot])

## v1.8.8 (patch)

Changes since v1.8.7:

- Keep lowercase letters as they are in Snake, Kebab and Camel case [patch] (Claude)

## v1.8.7 (patch)

Changes since v1.8.6:

- Keep a letter after a digit in the same word in Title, Pascal and Camel case [patch] (Claude)

## v1.8.6 (patch)

Changes since v1.8.5:

- Keep combining marks and non-ASCII digits in Pascal, Camel, Snake, Kebab and Macro case [patch] (Claude)

## v1.8.5 (patch)

Changes since v1.8.4:

- Merge remote-tracking branch 'origin/main' into fix/96-capitalised-apostrophe-names (Claude)
- Move the post-apostrophe lowercasing into a helper to satisfy S3776 (Claude)
- Lower the letter after an apostrophe with a ternary to keep cognitive complexity in bounds (Claude)
- Keep a capitalised apostrophe name like "O'Neil" in one word [patch] (Claude)

## v1.8.4 (patch)

Changes since v1.8.3:

- Keep a plural acronym in one piece: "APIs" no longer splits as "AP Is" [patch] (Claude)
- Case-map İ and ı, which the invariant mapping leaves alone [patch] (Claude)

## v1.8.3 (patch)

Changes since v1.8.2:

- Move CI onto the shared ci-shared.yml pipeline (Claude)

## v1.8.2 (patch)

Changes since v1.8.1:

- Keep an all-caps word containing ß in one piece, so ToMacroCase is idempotent [patch] (Claude)
- Keep a possessive acronym in one piece in Pascal/Camel/Snake/Kebab/Macro [patch] (Claude)
- Normalize a possessive all-caps word in ToTitleCase the same as the word alone [patch] (Claude)
- Keep the letter after a typographic apostrophe lowercase in ToTitleCase [patch] (Claude)

## v1.8.1 (patch)

Changes since v1.8.0:

- Move the #82 test beside the other ToTitleCase all-caps tests (Claude)
- Normalize an all-caps word joined by '-', '.' or a tab in ToTitleCase [patch] (Claude)
- Keep an in-word apostrophe from splitting the word in Pascal/Camel/Snake/Kebab/Macro [patch] (Claude)

## v1.8.0 (minor)

Changes since v1.7.0:

- Trim before case-mapping the first char in the first-char helpers (matt-edmondson)

## v1.7.0 (minor)

Changes since v1.6.0:

- Keep punctuation attached to its word in ToTitleCase (Claude)
- Case-map an astral first letter in ToCamelCase and the first-char helpers (Claude)

## v1.6.0 (minor)

Changes since v1.5.0:

- Normalize an all-caps word per word, not per string [minor] (Claude)

## v1.5.0 (minor)

Changes since v1.4.0:

- Preserve letters outside the BMP in every case conversion (Claude)
- Gate Dependabot auto-merge on CI actually being green (Claude)
- ci: adopt the consolidated .NET workflow [patch] (Claude)

## v1.4.7 (patch)

Changes since v1.4.6:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.4.6 (patch)

Changes since v1.4.5:

- Bump Polyfill from 11.3.0 to 11.4.0 (dependabot[bot])
- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.4.5 (patch)

Changes since v1.4.4:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.4.4 (patch)

Changes since v1.4.3:

- Bump ktsu.Sdk and 8 others (dependabot[bot])
- Bump MSTest.Sdk from 4.4.0 to 4.4.1 (dependabot[bot])

## v1.4.3 (patch)

Changes since v1.4.2:

- Gate Dependabot auto-merge on CI actually being green (Claude)

## v1.4.2 (patch)

Changes since v1.4.1:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.4.1 (patch)

Changes since v1.4.0:

- ci: adopt the consolidated .NET workflow [patch] (Claude)

## v1.4.0 (minor)

Changes since v1.3.0:

- ci: correct two stale comments in the dotnet workflow [patch] (matt-edmondson)
- ci: make the SonarQube quality gate opt in [patch] (matt-edmondson)
- ci: adopt the unified dotnet workflow [patch] (matt-edmondson)
- chore: store icon.png in LFS as .gitattributes declares (matt-edmondson)
- docs: scope build badge to the default branch (matt-edmondson)
- docs: correct README, DESCRIPTION and TAGS metadata (matt-edmondson)
- Stop Update SDKs failing when there is nothing to update (matt-edmondson)
- Fix build for ktsu.Sdk 2.27.0 analyzers: Polyfill PrivateAssets (KTSU0007), direct netstandard framework package refs (KTSU0001) [patch] (matt-edmondson)
- Sync .github\workflows\dotnet.yml (KtsuTools)
- Sync .editorconfig (KtsuTools)
- Sync global.json (KtsuTools)
- Sync .github\workflows\dotnet.yml (KtsuTools)
- chore: update ktsu.Sdk to 2.21.1 [patch] (matt-edmondson)
- chore: remove source link package references (matt-edmondson)
- Remove unused static import and clean up package references (matt-edmondson)
- Add TAGS.md with NuGet package tags (matt-edmondson)
- Sync .github\workflows\dotnet.yml (KtsuTools)
- Refactor permissions in dotnet.yml for minimal access; add SonarLint configuration in settings.json (matt-edmondson)
- Remove legacy build scripts (matt-edmondson)
- Remove .github\workflows\project.yml (matt-edmondson)
- Refactor null checks to use Ensure.NotNull for consistency (matt-edmondson)
- Refactor project files and update test assertions for clarity (matt-edmondson)
- Merge branch 'ktsu-dev:main' into main ([@chhaugen](https://github.com/chhaugen))
- Update .NET version to 10.0 and adjust coverage reporting in CI workflows (matt-edmondson)
- Add CLAUDE.md for project guidance and update project files for .NET 10.0 support (matt-edmondson)
- Extend to Unicode letters (Carl Henning Haugen)
- Update .gitattributes, .runsettings, and build scripts for improved test coverage and CI/CD configuration (matt-edmondson)
- Add new configuration files and update build scripts for SDK management and CI/CD improvements (matt-edmondson)
- Update configuration files and scripts for improved build and test processes (matt-edmondson)
- Fix project URL in New-License function to remove Owner variable, simplifying the URL construction. (matt-edmondson)
- Remove obsolete build configuration files and scripts, including Directory.Build.props, Directory.Build.targets, and various PowerShell scripts for versioning and changelog generation. Update CaseConverter and its tests to use 'var' for variable declarations and add copyright headers. (matt-edmondson)
- Update README and project files to reflect new SDK and enhance documentation (matt-edmondson)

## v1.3.37 (patch)

Changes since v1.3.36:

- Bump Polyfill from 11.2.0 to 11.3.0 (dependabot[bot])

## v1.3.36 (patch)

Changes since v1.3.35:

- Bump MSTest.Sdk from 4.3.3 to 4.4.0 (dependabot[bot])

## v1.3.35 (patch)

Changes since v1.3.34:

- ci: correct two stale comments in the dotnet workflow [patch] (matt-edmondson)

## v1.3.34 (patch)

Changes since v1.3.33:

- ci: make the SonarQube quality gate opt in [patch] (matt-edmondson)
- ci: adopt the unified dotnet workflow [patch] (matt-edmondson)

## v1.3.33 (patch)

Changes since v1.3.32:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.3.32 (patch)

Changes since v1.3.31:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.3.31 (patch)

Changes since v1.3.30:

- Bump the ktsu group with 9 updates (dependabot[bot])

## v1.3.30 (patch)

Changes since v1.3.29:

- chore: store icon.png in LFS as .gitattributes declares (matt-edmondson)
- docs: scope build badge to the default branch (matt-edmondson)
- docs: correct README, DESCRIPTION and TAGS metadata (matt-edmondson)

## v1.3.29 (patch)

Changes since v1.3.28:

- Stop Update SDKs failing when there is nothing to update (matt-edmondson)

## v1.3.28 (patch)

Changes since v1.3.27:

- Fix build for ktsu.Sdk 2.27.0 analyzers: Polyfill PrivateAssets (KTSU0007), direct netstandard framework package refs (KTSU0001) [patch] (matt-edmondson)
- Sync .github\workflows\dotnet.yml (KtsuTools)
- Sync .editorconfig (KtsuTools)
- Sync global.json (KtsuTools)
- Sync .github\workflows\dotnet.yml (KtsuTools)

## v1.3.27 (patch)

Changes since v1.3.26:

- chore: update ktsu.Sdk to 2.21.1 [patch] (matt-edmondson)

## v1.3.26 (patch)

Changes since v1.3.25:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.25 (patch)

Changes since v1.3.24:

- Bump the ktsu group with 8 updates (dependabot[bot])

## v1.3.24 (patch)

Changes since v1.3.23:

- Sync .github\workflows\update-sdks.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])
- Sync .gitattributes (ktsu[bot])

## v1.3.23 (patch)

Changes since v1.3.22:

- Bump MSTest.Sdk from 4.3.2 to 4.3.3 (dependabot[bot])

## v1.3.22 (patch)

Changes since v1.3.21:

- Bump the ktsu group with 8 updates (dependabot[bot])

## v1.3.21 (patch)

Changes since v1.3.20:

- Bump Polyfill from 11.0.0 to 11.0.1 (dependabot[bot])

## v1.3.20 (patch)

Changes since v1.3.19:

- Bump Polyfill from 10.11.2 to 11.0.0 (dependabot[bot])

## v1.3.19 (patch)

Changes since v1.3.18:

- Bump MSTest.Sdk from 4.3.0 to 4.3.2 (dependabot[bot])

## v1.3.18 (patch)

Changes since v1.3.17:

- Bump MSTest.Sdk from 4.2.3 to 4.3.0 (dependabot[bot])

## v1.3.17 (patch)

Changes since v1.3.16:

- Bump the ktsu group with 8 updates (dependabot[bot])

## v1.3.16 (patch)

Changes since v1.3.15:

- Bump Polyfill from 10.11.1 to 10.11.2 (dependabot[bot])

## v1.3.15 (patch)

Changes since v1.3.14:

- Bump Polyfill from 10.3.0 to 10.11.1 (dependabot[bot])
- Bump the ktsu group with 8 updates (dependabot[bot])

## v1.3.14 (patch)

Changes since v1.3.13:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.13 (patch)

Changes since v1.3.12:

- chore: remove source link package references (matt-edmondson)
- Remove unused static import and clean up package references (matt-edmondson)
- Add TAGS.md with NuGet package tags (matt-edmondson)
- Sync .github\workflows\dotnet.yml (KtsuTools)

## v1.3.13-pre.1 (prerelease)

Changes since v1.3.12:

- Sync .github\workflows\dotnet.yml (KtsuTools)

## v1.3.12 (patch)

Changes since v1.3.11:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.3.11 (patch)

Changes since v1.3.10:

- Refactor permissions in dotnet.yml for minimal access; add SonarLint configuration in settings.json (matt-edmondson)

## v1.3.11-pre.1 (prerelease)

Changes since v1.3.10:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.3.10 (patch)

Changes since v1.3.9:

- Remove legacy build scripts (matt-edmondson)

## v1.3.9 (patch)

Changes since v1.3.8:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync global.json (ktsu[bot])
- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])
- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync global.json (ktsu[bot])
- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync global.json (ktsu[bot])
- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync global.json (ktsu[bot])
- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync global.json (ktsu[bot])
- Sync COPYRIGHT.md (ktsu[bot])
- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])
- Sync global.json (ktsu[bot])
- Sync COPYRIGHT.md (ktsu[bot])

## v1.3.9-pre.7 (prerelease)

Changes since v1.3.9-pre.6:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])

## v1.3.9-pre.6 (prerelease)

Changes since v1.3.9-pre.5:

- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.9-pre.5 (prerelease)

Changes since v1.3.9-pre.4:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.9-pre.4 (prerelease)

Changes since v1.3.9-pre.3:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.9-pre.3 (prerelease)

Changes since v1.3.9-pre.2:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync global.json (ktsu[bot])
- Sync COPYRIGHT.md (ktsu[bot])

## v1.3.9-pre.2 (prerelease)

Changes since v1.3.9-pre.1:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])

## v1.3.9-pre.1 (prerelease)

Changes since v1.3.8:

- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])
- Sync global.json (ktsu[bot])
- Sync COPYRIGHT.md (ktsu[bot])

## v1.3.8 (patch)

Changes since v1.3.7:

- Remove .github\workflows\project.yml (matt-edmondson)

## v1.3.7 (patch)

Changes since v1.3.6:

- Refactor null checks to use Ensure.NotNull for consistency (matt-edmondson)
- Refactor project files and update test assertions for clarity (matt-edmondson)

## v1.3.6 (patch)

Changes since v1.3.5:

- Merge branch 'ktsu-dev:main' into main ([@chhaugen](https://github.com/chhaugen))
- Extend to Unicode letters (Carl Henning Haugen)

## v1.3.6-pre.3 (prerelease)

Changes since v1.3.6-pre.2:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])

## v1.3.6-pre.2 (prerelease)

Changes since v1.3.6-pre.1:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync global.json (ktsu[bot])

## v1.3.6-pre.1 (prerelease)

Changes since v1.3.5:

- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync .gitignore (ktsu[bot])

## v1.3.5 (patch)

Changes since v1.3.4:

- Update .NET version to 10.0 and adjust coverage reporting in CI workflows (matt-edmondson)
- Add CLAUDE.md for project guidance and update project files for .NET 10.0 support (matt-edmondson)

## v1.3.5-pre.3 (prerelease)

Changes since v1.3.5-pre.2:

- Sync scripts\update-winget-manifests.ps1 (ktsu[bot])

## v1.3.5-pre.2 (prerelease)

Changes since v1.3.5-pre.1:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.3.5-pre.1 (prerelease)

Changes since v1.3.4:

- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.3.4 (patch)

Changes since v1.3.3:

- Update .gitattributes, .runsettings, and build scripts for improved test coverage and CI/CD configuration (matt-edmondson)

## v1.3.3 (patch)

Changes since v1.3.2:

- Add new configuration files and update build scripts for SDK management and CI/CD improvements (matt-edmondson)
- Update configuration files and scripts for improved build and test processes (matt-edmondson)

## v1.3.3-pre.3 (prerelease)

Changes since v1.3.3-pre.2:

- Merge remote-tracking branch 'refs/remotes/origin/main' (ktsu[bot])
- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync .editorconfig (ktsu[bot])
- Sync .gitattributes (ktsu[bot])
- Sync .gitignore (ktsu[bot])
- Sync .mailmap (ktsu[bot])
- Sync .runsettings (ktsu[bot])

## v1.3.3-pre.2 (prerelease)

Changes since v1.3.3-pre.1:

- Sync scripts\PSBuild.psm1 (ktsu[bot])
- Sync .editorconfig (ktsu[bot])
- Sync .gitattributes (ktsu[bot])
- Sync .gitignore (ktsu[bot])
- Sync .mailmap (ktsu[bot])
- Sync .runsettings (ktsu[bot])

## v1.3.3-pre.1 (prerelease)

No significant changes detected since v1.3.2.

## v1.3.2 (patch)

Changes since v1.3.1:

- Fix project URL in New-License function to remove Owner variable, simplifying the URL construction. (matt-edmondson)

## v1.3.2-pre.14 (prerelease)

No significant changes detected since v1.3.2-pre.13.

## v1.3.2-pre.13 (prerelease)

No significant changes detected since v1.3.2-pre.12.

## v1.3.2-pre.12 (prerelease)

No significant changes detected since v1.3.2-pre.11.

## v1.3.2-pre.11 (prerelease)

No significant changes detected since v1.3.2-pre.10.

## v1.3.2-pre.10 (prerelease)

No significant changes detected since v1.3.2-pre.9.

## v1.3.2-pre.9 (prerelease)

No significant changes detected since v1.3.2-pre.8.

## v1.3.2-pre.8 (prerelease)

No significant changes detected since v1.3.2-pre.7.

## v1.3.2-pre.7 (prerelease)

No significant changes detected since v1.3.2-pre.6.

## v1.3.2-pre.6 (prerelease)

No significant changes detected since v1.3.2-pre.5.

## v1.3.2-pre.5 (prerelease)

No significant changes detected since v1.3.2-pre.4.

## v1.3.2-pre.4 (prerelease)

No significant changes detected since v1.3.2-pre.3.

## v1.3.2-pre.3 (prerelease)

No significant changes detected since v1.3.2-pre.2.

## v1.3.2-pre.2 (prerelease)

No significant changes detected since v1.3.2-pre.1.

## v1.3.2-pre.1 (prerelease)

Changes since v1.3.1:

- Sync scripts\PSBuild.psm1 (ktsu[bot])

## v1.3.1 (patch)

Changes since v1.3.0:

- Remove obsolete build configuration files and scripts, including Directory.Build.props, Directory.Build.targets, and various PowerShell scripts for versioning and changelog generation. Update CaseConverter and its tests to use 'var' for variable declarations and add copyright headers. (matt-edmondson)
- Update README and project files to reflect new SDK and enhance documentation (matt-edmondson)

## v1.3.1-pre.2 (prerelease)

Changes since v1.3.1-pre.1:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .editorconfig (ktsu[bot])
- Sync .runsettings (ktsu[bot])

## v1.3.1-pre.1 (prerelease)

Changes since v1.3.0:

- Sync .editorconfig (ktsu[bot])

## v1.3.0 (minor)

Changes since v1.2.0:

- Add LICENSE template (matt-edmondson)

## v1.2.1-pre.2 (prerelease)

Changes since v1.2.1-pre.1:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])

## v1.2.1-pre.1 (prerelease)

Changes since v1.2.0:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])
- Sync .editorconfig (ktsu[bot])
- Sync .gitignore (ktsu[bot])
- Sync Directory.Build.targets (ktsu[bot])

## v1.2.0 (minor)

Changes since v1.1.0:

- Update .editorconfig and enhance CaseConverter tests (matt-edmondson)

## v1.1.2-pre.3 (prerelease)

Changes since v1.1.2-pre.2:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])

## v1.1.2-pre.2 (prerelease)

Changes since v1.1.2-pre.1:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.1.2-pre.1 (prerelease)

Changes since v1.1.1:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Bump MSTest from 3.7.2 to 3.7.3 (dependabot[bot])
- Bump MSTest from 3.7.1 to 3.7.2 (dependabot[bot])
- Bump coverlet.collector from 6.0.3 to 6.0.4 (dependabot[bot])
- Bump MSTest from 3.7.0 to 3.7.1 (dependabot[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync scripts\make-version.ps1 (ktsu[bot])

## v1.1.1 (patch)

Changes since v1.1.0:

- Sync .mailmap (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])

## v1.1.1-pre.23 (prerelease)

Changes since v1.1.1-pre.22:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.1.1-pre.22 (prerelease)

Changes since v1.1.1-pre.21:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.1.1-pre.21 (prerelease)

No significant changes detected since v1.1.1-pre.20.

## v1.1.1-pre.20 (prerelease)

No significant changes detected since v1.1.1-pre.19.

## v1.1.1-pre.19 (prerelease)

Changes since v1.1.1-pre.18:

- Bump MSTest from 3.7.2 to 3.7.3 (dependabot[bot])

## v1.1.1-pre.18 (prerelease)

No significant changes detected since v1.1.1-pre.17.

## v1.1.1-pre.17 (prerelease)

No significant changes detected since v1.1.1-pre.16.

## v1.1.1-pre.16 (prerelease)

Changes since v1.1.1-pre.15:

- Bump MSTest from 3.7.1 to 3.7.2 (dependabot[bot])

## v1.1.1-pre.15 (prerelease)

Changes since v1.1.1-pre.14:

- Bump coverlet.collector from 6.0.3 to 6.0.4 (dependabot[bot])

## v1.1.1-pre.14 (prerelease)

No significant changes detected since v1.1.1-pre.13.

## v1.1.1-pre.13 (prerelease)

No significant changes detected since v1.1.1-pre.12.

## v1.1.1-pre.12 (prerelease)

No significant changes detected since v1.1.1-pre.11.

## v1.1.1-pre.11 (prerelease)

Changes since v1.1.1-pre.10:

- Bump MSTest from 3.7.0 to 3.7.1 (dependabot[bot])

## v1.1.1-pre.10 (prerelease)

No significant changes detected since v1.1.1-pre.9.

## v1.1.1-pre.9 (prerelease)

No significant changes detected since v1.1.1-pre.8.

## v1.1.1-pre.8 (prerelease)

Changes since v1.1.1-pre.7:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.1.1-pre.7 (prerelease)

Changes since v1.1.1-pre.6:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync scripts\make-changelog.ps1 (ktsu[bot])

## v1.1.1-pre.6 (prerelease)

No significant changes detected since v1.1.1-pre.5.

## v1.1.1-pre.5 (prerelease)

Changes since v1.1.1-pre.4:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.1.1-pre.4 (prerelease)

No significant changes detected since v1.1.1-pre.3.

## v1.1.1-pre.3 (prerelease)

No significant changes detected since v1.1.1-pre.2.

## v1.1.1-pre.2 (prerelease)

Changes since v1.1.1-pre.1:

- Sync scripts\make-version.ps1 (ktsu[bot])
- Sync .mailmap (ktsu[bot])

## v1.1.1-pre.1 (prerelease)

Changes since v1.1.0:

- Sync scripts\make-changelog.ps1 (ktsu[bot])

## v1.1.0 (minor)

Changes since v1.0.29:

- Add unit test project for CaseConverter (matt-edmondson)

## v1.0.29

No significant changes detected since v1.0.29-pre.7.

## v1.0.29-pre.7 (prerelease)

Changes since v1.0.29-pre.6:

- Add automation scripts for metadata management and versioning (matt-edmondson)

## v1.0.29-pre.6 (prerelease)

No significant changes detected since v1.0.29-pre.5.

## v1.0.29-pre.5 (prerelease)

Changes since v1.0.29-pre.4:

- Bump coverlet.collector from 6.0.2 to 6.0.3 (dependabot[bot])

## v1.0.29-pre.4 (prerelease)

No significant changes detected since v1.0.29-pre.3.

## v1.0.29-pre.3 (prerelease)

Changes since v1.0.29-pre.2:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.29-pre.2 (prerelease)

Changes since v1.0.29-pre.1:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.29-pre.1 (prerelease)

Changes since v1.0.28-pre.1:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.28-pre.1 (prerelease)

Changes since v1.0.27:

- Renamed metadata files (matt-edmondson)

## v1.0.27 (patch)

Changes since v1.0.26:

- Sync icon.png (ktsu[bot])

## v1.0.26 (patch)

Changes since v1.0.25:

- Sync Directory.Build.props (ktsu[bot])

## v1.0.25 (patch)

Changes since v1.0.24:

- Sync Directory.Build.props (ktsu[bot])

## v1.0.24 (patch)

Changes since v1.0.23:

- Replace LICENSE file with LICENSE.md for improved formatting and clarity (matt-edmondson)

## v1.0.23 (patch)

Changes since v1.0.22:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.22 (patch)

Changes since v1.0.21:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.21 (patch)

Changes since v1.0.20:

- Sync Directory.Build.targets (ktsu[bot])
- Sync Directory.Build.props (ktsu[bot])

## v1.0.20 (patch)

Changes since v1.0.19:

- Sync Directory.Build.targets (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync Directory.Build.targets (ktsu[bot])
- Sync Directory.Build.props (ktsu[bot])
- Bump MSTest.TestAdapter from 3.6.4 to 3.7.0 (dependabot[bot])
- Sync Directory.Build.props (ktsu[bot])
- Bump MSTest.TestFramework from 3.6.4 to 3.7.0 (dependabot[bot])
- Sync Directory.Build.targets (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.19 (patch)

Changes since v1.0.18:

- Bump MSTest.TestAdapter from 3.6.3 to 3.6.4 (dependabot[bot])
- Bump MSTest.TestFramework from 3.6.3 to 3.6.4 (dependabot[bot])

## v1.0.18 (patch)

Changes since v1.0.17:

- Sync Directory.Build.targets (ktsu[bot])

## v1.0.17 (patch)

Changes since v1.0.16:

- Sync Directory.Build.props (ktsu[bot])

## v1.0.16 (patch)

Changes since v1.0.15:

- Sync Directory.Build.targets (ktsu[bot])
- Sync Directory.Build.props (ktsu[bot])

## v1.0.15 (patch)

Changes since v1.0.14:

- Bump Microsoft.NET.Test.Sdk in the microsoft group (dependabot[bot])

## v1.0.14 (patch)

Changes since v1.0.13:

- Bump MSTest.TestFramework from 3.6.2 to 3.6.3 (dependabot[bot])
- Bump MSTest.TestAdapter from 3.6.2 to 3.6.3 (dependabot[bot])

## v1.0.13 (patch)

Changes since v1.0.12:

- Bump MSTest.TestFramework from 3.6.1 to 3.6.2 (dependabot[bot])
- Bump MSTest.TestAdapter from 3.6.0 to 3.6.2 (dependabot[bot])

## v1.0.12 (patch)

Changes since v1.0.11:

- Bump MSTest.TestFramework from 3.6.0 to 3.6.1 (dependabot[bot])

## v1.0.11 (patch)

Changes since v1.0.10:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.10 (patch)

Changes since v1.0.9:

- Update VERSION to 1.0.10 (matt-edmondson)

## v1.0.10-pre.1 (prerelease)

Changes since v1.0.9:

- Renamed metadata files (matt-edmondson)
- Replace LICENSE file with LICENSE.md for improved formatting and clarity (matt-edmondson)
- Update VERSION to 1.0.19 (matt-edmondson)
- Update VERSION to 1.0.18 (matt-edmondson)
- Update VERSION to 1.0.17 (matt-edmondson)
- Update VERSION to 1.0.16 (matt-edmondson)
- Update VERSION to 1.0.15 (matt-edmondson)
- Update VERSION to 1.0.14 (matt-edmondson)
- Update VERSION to 1.0.13 (matt-edmondson)
- Update VERSION to 1.0.12 (matt-edmondson)
- Update VERSION to 1.0.11 (matt-edmondson)
- Update VERSION to 1.0.10 (matt-edmondson)

## v1.0.9 (patch)

Changes since v1.0.8:

- Sync .github\dependabot.yml (ktsu[bot])
- Sync .github\dependabot.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.8 (patch)

Changes since v1.0.7:

- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])

## v1.0.7 (patch)

Changes since v1.0.6:

- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])

## v1.0.6 (patch)

Changes since v1.0.5:

- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])

## v1.0.5 (patch)

Changes since v1.0.4:

- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.4 (patch)

Changes since v1.0.3:

- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])
- Sync .github\workflows\dependabot-merge.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])
- Bump MSTest.TestAdapter from 3.5.2 to 3.6.0 (dependabot[bot])
- Bump MSTest.TestFramework from 3.5.2 to 3.6.0 (dependabot[bot])

## v1.0.3 (patch)

Changes since v1.0.2:

- Sync .github\dependabot.yml (ktsu[bot])
- Sync .github\workflows\dotnet.yml (ktsu[bot])

## v1.0.2 (patch)

Changes since v1.0.1:

- Update VERSION (matt-edmondson)
- Migrate ktsu.io to ktsu namespace (matt-edmondson)

## v1.0.1 (major)

- Update dotnet.yml (matt-edmondson)
- Update dotnet.yml (matt-edmondson)
- Update build scripts (matt-edmondson)
- Update dotnet.yml (matt-edmondson)
- Update Directory.Build.targets (matt-edmondson)
- Update Directory.Build.props (matt-edmondson)
- Update build config (matt-edmondson)
- Update LICENSE (matt-edmondson)
- Update nuget.config (matt-edmondson)
- Update nuget.config (matt-edmondson)
- Add github package support (matt-edmondson)
- Make title case split words on case change so it can convert pascal to title (matt-edmondson)
- Update description and readme (matt-edmondson)
- Update LICENSE (matt-edmondson)
- Create dependabot-merge.yml (matt-edmondson)
- Fix misplaced usings (matt-edmondson)
- Update dotnet.yml (matt-edmondson)
- Update .editorconfig (matt-edmondson)
- Update Directory.Build.props (matt-edmondson)
- Read from AUTHORS file during build (matt-edmondson)
- Read PackageDescription from DESCRIPTION file (matt-edmondson)
- Create VERSION (matt-edmondson)
- Read from VERSION when building (matt-edmondson)
- Assign dependabot PRs to matt (matt-edmondson)
- Dont try to push packages when building pull requests (matt-edmondson)
- Avoid double upload of symbols package (matt-edmondson)
- Enable dependabot and sourcelink (matt-edmondson)
- Migrate from .project.props to Directory.Build.props (matt-edmondson)
- Fix code style errors (matt-edmondson)
- Update .editorconfig (matt-edmondson)
- Update README.md (matt-edmondson)
- Make snake_case derive from MACRO_CASE rather than vice-versa per dotnet's suggestion as uppercase is more resilient to round-trip conversion (matt-edmondson)
- Add github workflow to build and upload to nuget (matt-edmondson)
- Initial commit (matt-edmondson)
- Initial commit (matt-edmondson)

