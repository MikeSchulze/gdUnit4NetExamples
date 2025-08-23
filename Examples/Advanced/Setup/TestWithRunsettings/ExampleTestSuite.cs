namespace GdUnit4.Examples.Advanced.Setup.TestWithRunsettings;

using Godot;

using static Assertions;

/// <summary>
///     A test class demonstrating how to test Godot-specific objects and functionality.
///     This example shows how to test classes that inherit from Godot types and require
///     the Godot runtime to be available. Unlike pure .NET testing, these tests need:
///     - The [RequireGodotRuntime] attribute on test methods
///     - Proper memory management (calling Free() on Godot objects) or using AutoFree()
///     - Access to Godot's type system and runtime
///     Key concepts demonstrated:
///     - Testing classes derived from GodotObject.
///     - Using [RequireGodotRuntime] for tests that need Godot's runtime.
///     - Basic assertions with Godot objects.
/// </summary>
[TestSuite]
public class ExampleTestSuite
{
    /// <summary>
    ///     Tests instantiation and basic properties of a class derived from GodotObject.
    ///     This test demonstrates:
    ///     - Creating instances of classes that inherit from Godot types
    ///     - Using [RequireGodotRuntime] to indicate this test needs Godot's runtime
    ///     - Verifying the object is properly instantiated (not null).
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void IsNodeNotNull()
    {
        var node = AutoFree(new Node());
        AssertThat(node).IsNotNull();
    }
}
