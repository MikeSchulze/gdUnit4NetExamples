namespace GdUnit4.Examples.Basics.Tests.OnGodotObjects;

using Godot;

using static Assertions;

/// <summary>
///     Demonstrates testing Godot Vector3 objects with good vs bad assertion patterns.
///     This test class shows:
///     - Testing Vector3 constants and comparisons effectively
///     - ❌ Bad approach: Testing individual vector components (X, Y, Z) separately
///     - ✅ Good approach: Using direct vector comparison with expected values
///     - Why component-by-component testing is verbose and less maintainable
///     - How direct comparison makes vector tests cleaner and more readable
///     Key learning: Test vector values as complete units rather than individual components.
/// </summary>
[TestSuite]
public class GodotVector3Tests
{
    /// <summary>
    ///     Tests Vector3 constants and basic equality comparisons.
    ///     Demonstrates the correct way to verify Vector3 constants and perform comparisons.
    ///     Shows proper use of fluent chaining for related assertions.
    ///     Expected result: Vector3 constants match expected values and comparisons work correctly.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void Vector3ComparisonsConstants()
    {
        // Test Vector3 creation and equality
        var vector1 = new Vector3(1, 2, 3);
        var vector2 = new Vector3(1, 2, 3);
        var vector3 = new Vector3(3, 2, 1);

        // Test equality comparisons with proper fluent chaining
        AssertThat(vector1)
            .IsEqual(vector2)
            .IsNotEqual(vector3);

        // Test Vector3 constants - clean and direct
        AssertThat(new Vector3(0, 0, 0)).IsEqual(Vector3.Zero);
        AssertThat(new Vector3(1, 1, 1)).IsEqual(Vector3.One);
        AssertThat(new Vector3(0, 1, 0)).IsEqual(Vector3.Up);
    }

    /// <summary>
    ///     ❌ BAD APPROACH: Testing Vector3 by checking individual components (X, Y, Z) separately.
    ///     This approach is verbose, repetitive, and hard to maintain:
    ///     - Requires 3 separate assertions per vector
    ///     - Harder to see the complete vector value at a glance
    ///     - More prone to copy-paste errors
    ///     - Doesn't leverage Vector3's built-in equality comparison
    ///     Expected result: Test passes but demonstrates poor testing style.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void Vector3ComparisonsBadSyntax()
    {
        // Test Vector3 creation and basic operations
        var vector1 = new Vector3(1, 2, 3);
        var vector2 = new Vector3(1, 2, 3);
        var vector3 = new Vector3(3, 2, 1);

        // Test Vector3 operations - these are fine
        AssertThat(vector1 + vector2).IsEqual(new Vector3(2, 4, 6));
        AssertThat(vector1 * 2).IsEqual(new Vector3(2, 4, 6));

        // ❌ BAD: Testing individual components - verbose and repetitive
        // This approach requires 9 separate assertions for 3 simple vectors
        AssertThat(vector1.X).IsEqual(1);
        AssertThat(vector1.Y).IsEqual(2);
        AssertThat(vector1.Z).IsEqual(3);
        AssertThat(vector2.X).IsEqual(1);
        AssertThat(vector2.Y).IsEqual(2);
        AssertThat(vector2.Z).IsEqual(3);
        AssertThat(vector3.X).IsEqual(3);
        AssertThat(vector3.Y).IsEqual(2);
        AssertThat(vector3.Z).IsEqual(1);
    }

    /// <summary>
    ///     ✅ GOOD APPROACH: Testing Vector3 using direct comparison with expected values.
    ///     This approach is clean, readable, and maintainable:
    ///     - Single assertion per vector that's clear and concise
    ///     - Immediately shows the expected vector value
    ///     - Uses Vector3's optimized equality comparison
    ///     - Much more maintainable and readable
    ///     Expected result: Test passes and demonstrates best practice for vector testing.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void Vector3ComparisonsGoodSyntax()
    {
        // Test Vector3 creation and basic operations
        var vector1 = new Vector3(1, 2, 3);
        var vector2 = new Vector3(1, 2, 3);
        var vector3 = new Vector3(3, 2, 1);

        // Test Vector3 operations
        AssertThat(vector1 + vector2).IsEqual(new Vector3(2, 4, 6));
        AssertThat(vector1 * 2).IsEqual(new Vector3(2, 4, 6));

        // ✅ GOOD: Direct vector comparison - clean and readable
        // This approach requires only 3 assertions for the same validation
        AssertThat(vector1).IsEqual(new Vector3(1, 2, 3));
        AssertThat(vector2).IsEqual(new Vector3(1, 2, 3));
        AssertThat(vector3).IsEqual(new Vector3(3, 2, 1));
    }
}
