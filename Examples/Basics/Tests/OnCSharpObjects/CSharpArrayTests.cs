namespace GdUnit4.Examples.Basics.Tests.OnCSharpObjects;

using static Assertions;

/// <summary>
///     Demonstrates testing C# Array objects with good vs bad assertion patterns.
///     This test class shows:
///     - ❌ Bad approach: Testing individual array elements separately
///     - ✅ Good approach: Using direct array comparison with expected values
///     - Proper use of Array-specific assertions (.IsEmpty(), .HasSize())
///     - Why direct comparison is more readable and maintainable
///     Key learning: Compare entire collections when possible instead of element-by-element testing.
///     Note: These tests don't require Godot runtime as they test standard C# types.
/// </summary>
[TestSuite]
public class CSharpArrayTests
{
    /// <summary>
    ///     ❌ BAD APPROACH: Testing C# Array by checking each element individually.
    ///     This approach is verbose, error-prone, and hard to maintain:
    ///     - Requires multiple assertions for simple data validation
    ///     - Makes tests longer and harder to read
    ///     - Easy to miss elements or make copy-paste errors
    ///     - Doesn't clearly show the expected array structure
    ///     Expected result: Test passes but demonstrates poor testing style.
    /// </summary>
    [TestCase]
    public void TestCSharpArrayIsEqualBadSyntax()
    {
        // Create and test empty array
        var emptyArray = Array.Empty<int>();
        AssertThat(emptyArray)
            .IsEmpty()
            .HasSize(0);

        // Create array with initial values
        var array = new object[] { 1, "hello", 3.14, true };

        // ❌ BAD: Testing each element individually - verbose and error-prone
        AssertThat(array.Length).IsEqual(4);
        AssertThat(array[0]).IsEqual(1);
        AssertThat(array[1]).IsEqual("hello");
        AssertThat(array[2]).IsEqual(3.14);
        AssertThat(array[3]).IsEqual(true);
    }

    /// <summary>
    ///     ✅ GOOD APPROACH: Testing C# Array using direct comparison with expected values.
    ///     This approach is clean, readable, and maintainable:
    ///     - Single assertion that clearly shows expected array content
    ///     - Much shorter and easier to understand
    ///     - Less prone to copy-paste errors
    ///     - Immediately shows the complete expected structure
    ///     - Uses collection literals for clean syntax
    ///     Expected result: Test passes and demonstrates best practice for array testing.
    /// </summary>
    [TestCase]
    public void TestCSharpArrayIsEqualGoodSyntax()
    {
        // Create and test empty array
        var emptyArray = Array.Empty<int>();
        AssertThat(emptyArray)
            .IsEmpty()
            .HasSize(0);

        // Create array with initial values
        var array = new object[] { 1, "hello", 3.14, true };

        // ✅ GOOD: Direct comparison with expected array - clean and readable
        AssertThat(array).IsEqual(new object[] { 1, "hello", 3.14, true });
    }
}
