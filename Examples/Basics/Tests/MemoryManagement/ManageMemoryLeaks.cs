namespace GdUnit4.Examples.Basics.Tests.MemoryManagement;

using Godot;

using static Assertions;

/// <summary>
///     Demonstrates proper memory management when testing Godot objects.
///     This test class shows:
///     - ❌ Manual memory management with Free() calls
///     - ✅ Automatic memory management using AutoFree()
///     - Why AutoFree() is the preferred approach for test cleanup
///     - Testing different types of Godot objects
///     Godot objects must be manually freed to prevent memory leaks. The AutoFree()
///     helper makes this automatic and prevents common mistakes in test cleanup.
/// </summary>
[TestSuite]
public class ManageMemoryLeaks
{
    /// <summary>
    ///     Demonstrates what happens without proper memory management.
    ///     This test intentionally shows the problematic pattern to avoid.
    ///     ⚠️ WARNING: This pattern will cause memory leaks in real applications.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    [IgnoreUntil(Description = "Intentionally ignored to prevent memory leaks - demonstrates bad pattern")]
    public void DoNotUseThisPattern()
    {
        // ❌ BAD: Creating Godot objects without cleanup
        var node1 = new Node();
        var node2 = new Node();
        var node3 = new Node();

        // Test the objects
        AssertThat(node1).IsNotNull();
        AssertThat(node2).IsNotNull();
        AssertThat(node3).IsNotNull();

        // ❌ NO CLEANUP = MEMORY LEAK!
        // These objects will never be freed, causing memory leaks
        // This is why this test is marked with [IgnoreUntil]
    }

    /// <summary>
    ///     ❌ BAD APPROACH - Manual memory management with Free() calls.
    ///     This approach is error-prone because you must remember to call Free()
    ///     on every Godot object, and if a test fails, cleanup might be skipped.
    ///     Expected result: Test passes but requires manual cleanup.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void BadApproach()
    {
        // Create Godot objects - these need manual cleanup
        var node = new Node();
        var sprite = new Sprite2D();
        var audioPlayer = new AudioStreamPlayer();

        // Perform your tests
        AssertThat(node).IsNotNull();
        AssertThat(sprite).IsNotNull();
        AssertThat(audioPlayer).IsNotNull();

        // ❌ MANUAL CLEANUP REQUIRED - easy to forget or miss if test fails
        node.Free();
        sprite.Free();
        audioPlayer.Free();
    }

    /// <summary>
    ///     ✅ GOOD APPROACH - Automatic memory management using AutoFree().
    ///     AutoFree() registers Godot objects for automatic cleanup when the test finishes.
    ///     This prevents memory leaks even if tests fail or throw exceptions.
    ///     Expected result: Test passes and objects are automatically cleaned up.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void GoodApproach()
    {
        // Create Godot objects and register them for auto-cleanup
        var node = AutoFree(new Node());
        var sprite = AutoFree(new Sprite2D());
        var audioPlayer = AutoFree(new AudioStreamPlayer());

        // Perform your tests
        AssertThat(node).IsNotNull();
        AssertThat(sprite).IsNotNull();
        AssertThat(audioPlayer).IsNotNull();

        // ✅ NO MANUAL CLEANUP NEEDED - AutoFree() handles it automatically
        // Objects are freed even if the test fails or throws an exception
    }
}
