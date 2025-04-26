namespace GdUnit4Net.Examples;

using GdUnit4;

using Godot;

using static GdUnit4.Assertions;

/// <summary>
///     This test suite demonstrates how to test Godot-specific objects with gdUnit4Net.
///     Key Points:
///     - The [RequireGodotRuntime] attribute is essential when testing Godot-specific objects
///     - Testing Godot's Vector2 is different from testing System.Numerics.Vector2
///     - Godot's StringName can be compared with regular C# strings
///     The [RequireGodotRuntime] attribute ensures that Godot's runtime is initialized before
///     running these tests. This is necessary because Godot objects like Vector2 and StringName
///     rely on Godot's internal systems, which must be properly initialized.
///     Without this attribute, tests involving Godot objects would fail as the Godot engine
///     wouldn't be properly initialized to handle its native types.
///     These tests demonstrate how gdUnit4Net seamlessly integrates with both .NET and
///     Godot object systems, allowing you to write clear, readable tests for Godot games.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class GodotObjectTest
{
    /// <summary>
    ///     Demonstrates comparing two Godot.Vector2 instances.
    ///     This test verifies that Godot's Vector2 equality comparison works correctly.
    ///     Note that this is testing Godot's Vector2 type, not the System.Numerics.Vector2 type.
    ///     The [RequireGodotRuntime] attribute at the class level ensures that Godot's
    ///     runtime is properly initialized before this test runs.
    /// </summary>
    [TestCase]
    public void CompareGodotVector2() => AssertThat(new Vector2(1, 1))
        .IsEqual(new Vector2(1, 1));

    /// <summary>
    ///     Demonstrates comparing Godot's StringName with a regular C# string.
    ///     This test shows that gdUnit4Net can correctly compare Godot's StringName type
    ///     with a standard C# string. The StringName class is Godot-specific and requires
    ///     the Godot runtime to be initialized.
    ///     This demonstrates the interoperability between Godot types and .NET types
    ///     when using gdUnit4Net for testing.
    /// </summary>
    [TestCase]
    public void CompareGodotString()
    {
        using var stringName = new StringName("This is a test message");
        AssertThat(stringName).IsEqual("This is a test message");
    }
}
