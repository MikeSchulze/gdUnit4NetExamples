namespace GdUnit4.Examples.Basics.Tests.AssertionExamples;

using static Assertions;

/// <summary>
///     Basic examples demonstrating string assertions in gdUnit4Net.
///     This test class shows common string testing patterns including:
///     - Equality comparisons (exact and case-insensitive)
///     - Content checking (contains/not contains)
///     - Fluent assertion chaining for multiple checks
///     Perfect for beginners learning how to test string values and operations.
/// </summary>
[TestSuite]
public class StringAssertion
{
    /// <summary>
    ///     Tests exact string equality - both strings must match exactly.
    ///     This is the most basic string assertion for testing identical values.
    ///     Expected result: The test should pass since both strings are identical.
    /// </summary>
    [TestCase]
    public void IsEqual()
        => AssertThat("This is a test")
            .IsEqual("This is a test");

    /// <summary>
    ///     Tests case-insensitive string equality - strings match regardless of capitalization.
    ///     Useful when testing user input or data where case doesn't matter.
    ///     Expected result: The test should pass even though the cases differ.
    /// </summary>
    [TestCase]
    public void IsEqualIgnoringCase()
        => AssertThat("This is a test")
            .IsEqualIgnoringCase("this is a Test");

    /// <summary>
    ///     Demonstrates different ways to test multiple conditions on the same string.
    ///     ❌ BAD STYLE - Separate assertions (repeating the same string):
    ///     AssertThat("This is a test").Contains("test");
    ///     AssertThat("This is a test").NotContains("not");
    ///     ✅ GOOD STYLE - Fluent chaining (preferred approach):
    ///     AssertThat("This is a test").Contains("test").NotContains("not");
    ///     Always prefer fluent chaining when testing multiple conditions on the same value.
    ///     It's more readable, efficient, and follows gdUnit4Net best practices.
    /// </summary>
    [TestCase]
    public void ContainsGoodVsBadStyle()
    {
        // ❌ BAD: Multiple separate assertions on the same string
        // This is repetitive and less efficient
        AssertThat("This is a test").Contains("test");
        AssertThat("This is a test").NotContains("not");

        // ✅ GOOD: Use fluent chaining for multiple checks on the same value
        // This is the preferred gdUnit4Net style
        AssertThat("This is a test")
            .Contains("test")
            .NotContains("not");
    }
}
