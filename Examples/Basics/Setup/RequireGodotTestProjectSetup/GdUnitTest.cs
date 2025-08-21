namespace GdUnit4.Test;

// This 'using static' directive allows us to call AssertThat() directly
// instead of writing Assertions.AssertThat(), making test code cleaner
using Godot;

using static Assertions;

/// <summary>
///     A test class demonstrating how to test Godot-specific objects and functionality.
///     This example shows how to test classes that inherit from Godot types and require
///     the Godot runtime to be available. Unlike pure .NET testing, these tests need:
///     - The [RequireGodotRuntime] attribute on test methods
///     - Proper memory management (calling Free() on Godot objects)
///     - Access to Godot's type system and runtime
///     Key concepts demonstrated:
///     - Testing classes derived from GodotObject.
///     - Using [RequireGodotRuntime] for tests that need Godot's runtime.
///     - Proper cleanup of Godot objects with Free().
///     - Basic assertions with Godot objects.
/// </summary>
[TestSuite]
public class GdUnitTest
{
    /// <summary>
    ///     Tests instantiation and basic properties of a class derived from GodotObject.
    ///     This test demonstrates:
    ///     - Creating instances of classes that inherit from Godot types
    ///     - Using [RequireGodotRuntime] to indicate this test needs Godot's runtime
    ///     - Verifying the object is properly instantiated (not null)
    ///     - Proper cleanup by calling Free() on Godot objects
    ///     Important: All Godot objects should be freed after use to prevent memory leaks.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void IsNodeNotNull()
    {
        var node = new Node();
        AssertThat(node).IsNotNull();
        node.Free();
    }
}
