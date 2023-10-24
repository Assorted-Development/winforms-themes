// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Design",
    "CA1001:Types that own disposable fields should be disposable",
    Justification = "See https://github.com/Assorted-Development/winforms-themes/pull/21#discussion_r1367962245",
    Scope = "type",
    Target = "~T:WinFormsThemes.ThemeRegistryBuilder")]

[assembly: SuppressMessage(
    "Maintainability",
    "CA1502: Avoid excessive complexity",
    Justification = "Not possible to reduce further due to constructor initialization",
    Scope = "member",
    Target = "~M:WinFormsThemes.Themes.FileTheme.#ctor(System.Text.Json.Nodes.JsonNode)")]
