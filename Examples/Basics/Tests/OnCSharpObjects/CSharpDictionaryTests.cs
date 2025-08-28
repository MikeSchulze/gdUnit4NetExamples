namespace GdUnit4.Examples.Basics.Tests.OnCSharpObjects;

using static Assertions;

/// <summary>
///     Demonstrates testing C# Dictionary objects with good vs bad assertion patterns.
///     This test class shows:
///     - ❌ Bad approach: Testing dictionary by accessing individual keys and values
///     - ✅ Good approach: Using direct dictionary comparison with expected structure
///     - Why element-by-element testing is verbose and error-prone
///     - How direct comparison makes tests more readable and maintainable
///     Key learning: Compare entire data structures when possible instead of field-by-field validation.
///     Note: These tests don't require Godot runtime as they test standard C# types.
/// </summary>
[TestSuite]
public class CSharpDictionaryTests
{
    /// <summary>
    ///     ❌ BAD APPROACH: Testing C# Dictionary by checking each key-value pair individually.
    ///     This approach is verbose, error-prone, and hard to maintain:
    ///     - Requires multiple assertions for simple data validation
    ///     - Doesn't clearly show the expected dictionary structure
    ///     - Prone to errors when adding/removing dictionary entries
    ///     - Makes tests longer and harder to maintain
    ///     Expected result: Test passes but demonstrates poor testing style.
    /// </summary>
    [TestCase]
    public void TestCSharpDictionaryBadSyntax()
    {
        // Create dictionary with values
        var dict = new Dictionary<string, object>
        {
            { "name", "Player" },
            { "level", 42 },
            { "health", 100.0f },
            { "alive", true }
        };

        // ❌ BAD: Testing each key-value pair individually - verbose and error-prone
        AssertThat(dict.Count).IsEqual(4);
        AssertThat(dict["name"]).IsEqual("Player");
        AssertThat(dict["level"]).IsEqual(42);
        AssertThat(dict["health"]).IsEqual(100.0f);
        AssertThat(dict["alive"]).IsEqual(true);
    }

    /// <summary>
    ///     ✅ GOOD APPROACH: Testing C# Dictionary using direct comparison with expected structure.
    ///     This approach is clean, readable, and maintainable:
    ///     - Single assertion that clearly shows the complete expected structure
    ///     - Much more readable and concise
    ///     - Easy to modify when dictionary structure changes
    ///     - Immediately obvious what the dictionary should contain
    ///     Expected result: Test passes and demonstrates best practice for dictionary testing.
    /// </summary>
    [TestCase]
    public void TestCSharpDictionaryGoodSyntax()
    {
        // Create dictionary with values
        var dict = new Dictionary<string, object>
        {
            { "name", "Player" },
            { "level", 42 },
            { "health", 100.0f },
            { "alive", true }
        };

        // ✅ GOOD: Direct comparison with expected dictionary - clean and readable
        AssertThat(dict).IsEqual(
            new Dictionary<string, object>
            {
                { "name", "Player" },
                { "level", 42 },
                { "health", 100.0f },
                { "alive", true }
            });
    }
}
