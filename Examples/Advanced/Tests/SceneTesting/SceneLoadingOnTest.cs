namespace GdUnit4.Examples.Advanced.Tests.SceneTesting;

using static Assertions;

/// <summary>
///     Demonstrates basic scene loading and verification using gdUnit4Net's ISceneRunner.
///     This test class shows fundamental scene testing patterns including:
///     - Loading scenes from resource paths
///     - Verifying scene instances are properly created
///     - Using ISceneRunner for scene management
///     - Basic scene validation techniques
///     Key concepts demonstrated:
///     - ISceneRunner.Load() for loading scenes with automatic cleanup
///     - Scene() method to access the loaded scene instance
///     - Basic assertions on scene objects
///     Prerequisites:
///     - Test scene file must exist at the specified path
///     - Scene must be properly configured and saved in Godot
///     - [RequireGodotRuntime] is required for all scene-based tests.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class SceneLoadingOnTest
{
    /// <summary>
    ///     Tests basic scene loading functionality using ISceneRunner.
    ///     This test demonstrates the fundamental pattern for loading and verifying scenes:
    ///     1. Load a scene using ISceneRunner.Load() with autoFree enabled
    ///     2. Verify the runner was created successfully
    ///     3. Verify the scene instance was loaded properly
    ///     The autoFree parameter (true) ensures automatic memory management,
    ///     cleaning up the scene when the test completes.
    ///     Expected behavior:
    ///     - Scene loads successfully from the resource path
    ///     - Runner and scene instances are not null
    ///     - Scene is properly instantiated and ready for testing.
    /// </summary>
    [TestCase]
    public void SceneLoading()
    {
        // Example to load a scene by resource path, set autoFree to true
        var runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);

        // verify
        AssertThat(runner).IsNotNull();
        AssertThat(runner.Scene()).IsNotNull();
    }
}
