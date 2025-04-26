namespace GdUnit4Net.Examples;

using System.Numerics;

using GdUnit4;

using static GdUnit4.Assertions;

/// <summary>
///     This test suite demonstrates how to test native .NET objects with gdUnit4Net.
///     Key Points:
///     - Testing .NET specific objects like System.Numerics.Vector2
///     - Using static imports for cleaner assertion syntax
///     - Basic string comparisons in .NET
///     This class doesn't use the [RequireGodotRuntime] attribute because it only tests .NET types
///     and doesn't require the Godot engine to be running. This makes these tests faster to execute
///     and demonstrates how you can separate tests that need Godot's runtime from those that don't.
///     Note: For non-Godot objects, you can use standard .NET testing approaches as gdUnit4Net
///     seamlessly integrates with .NET's type system.
/// </summary>
[TestSuite]
public class DotNetObjectTest
{
    /// <summary>
    ///     Demonstrates comparing two System.Numerics.Vector2 instances.
    ///     This test verifies that value equality works correctly for .NET Vector2 objects.
    ///     The equality comparison for System.Numerics.Vector2 is based on value equality,
    ///     not reference equality.
    /// </summary>
    [TestCase]
    public void CompareDotNetVector2() => AssertThat(new Vector2(1, 1))
        .IsEqual(new Vector2(1, 1));

    /// <summary>
    ///     Demonstrates string comparison using gdUnit4Net assertions.
    ///     This test shows that standard .NET string equality works with
    ///     gdUnit4Net's assertion framework.
    /// </summary>
    [TestCase]
    public void CompareGodotString() => AssertThat("This is a test message")
        .IsEqual("This is a test message");
}
