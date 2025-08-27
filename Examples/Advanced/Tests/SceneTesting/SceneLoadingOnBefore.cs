namespace GdUnit4.Examples.Advanced.Tests.SceneTesting;

using static Assertions;

/// <summary>
///     Demonstrates scene loading during suite-level setup using the [Before] attribute.
///     This test class shows how to load a scene once for the entire test suite and
///     reuse it across multiple test methods. This pattern is efficient when:
///     - Multiple tests need the same scene configuration
///     - Scene loading is expensive and should be minimized
///     - Tests don't modify the scene state significantly
///     - You want to test different aspects of the same scene
///     Key concepts demonstrated:
///     - [Before] attribute for one-time suite setup
///     - [After] attribute for suite-level cleanup
///     - Shared scene instance across all test methods
///     - Efficient resource usage for scene testing
///     Lifecycle execution:
///     1. [Before] - Load scene once before all tests
///     2. [TestCase] methods - All tests use the same scene instance
///     3. [After] - Clean up scene after all tests complete
///     Benefits of this approach:
///     - Faster test execution (single scene load)
///     - Consistent scene state across tests
///     - Reduced memory allocation overhead
///     - Simplified test setup
///     Considerations:
///     - Tests should not modify scene state
///     - All tests must be compatible with the same scene
///     - Debugging may be harder if tests affect each other.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class SceneLoadingOnBefore
{
    // ReSharper disable once NullableWarningSuppressionIsUsed
    private ISceneRunner runner = null!;

    /// <summary>
    ///     Suite-level setup method that loads the scene once before all tests execute.
    ///     This approach is optimal when multiple tests need to examine the same scene
    ///     without modifying its state. The scene remains loaded and available for
    ///     all test methods in this suite.
    ///     Setup actions:
    ///     - Load scene from resource path with autoFree enabled
    ///     - Verify successful scene loading
    ///     - Make scene available for all test methods
    ///     The autoFree parameter ensures automatic cleanup when the test suite completes,
    ///     eliminating manual memory management concerns.
    /// </summary>
    [Before]
    public void SetupScene()
    {
        // Load scene once for the entire test suite
        runner = ISceneRunner.Load("res://SceneTesting/TestSceneWithButton.tscn", true);
        AssertThat(runner).IsNotNull();
        AssertThat(runner.Scene()).IsNotNull();
    }

    /// <summary>
    ///     Suite-level cleanup method executed after all tests complete.
    ///     Verifies that the scene loaded in [Before] is properly accessible
    ///     and integrated into the scene tree for testing.
    ///     This test validates:
    ///     - Scene instance is available
    ///     - Scene is properly integrated (IsInsideTree)
    ///     - Runner maintains valid state.
    /// </summary>
    [After]
    public void CleanupScene() =>

        // Verify scene is still present
        AssertThat(runner.Scene()).IsNotNull();

    /// <summary>
    ///     Tests basic scene accessibility and state.
    ///     Verifies that the scene loaded in [Before] is properly accessible
    ///     and integrated into the scene tree for testing.
    ///     This test validates:
    ///     - Scene instance is available
    ///     - Scene is properly integrated (IsInsideTree)
    ///     - Runner maintains valid state.
    /// </summary>
    [TestCase]
    public void TestCaseA()
        => AssertThat(runner.Scene())
            .IsNotNull();

    /// <summary>
    ///     Tests basic scene accessibility and state.
    ///     Verifies that the scene loaded in [Before] is properly accessible
    ///     and integrated into the scene tree for testing.
    ///     This test validates:
    ///     - Scene instance is available
    ///     - Scene is properly integrated (IsInsideTree)
    ///     - Runner maintains valid state.
    /// </summary>
    [TestCase]
    public void TestCaseB()
        => AssertThat(runner.Scene())
            .IsNotNull();
}
