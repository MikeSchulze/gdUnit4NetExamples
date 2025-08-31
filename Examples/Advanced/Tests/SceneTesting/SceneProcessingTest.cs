namespace GdUnit4.Examples.Advanced.Tests.SceneTesting;

using Resources;

/// <summary>
///     Demonstrates scene processing and frame simulation testing patterns.
///     This test suite shows how to test game scenes that require frame-based processing
///     and demonstrates different approaches to scene execution validation including:
///     • Frame-based testing with specific frame counts
///     • Signal-based testing that waits for scene completion
///     • Scene lifecycle testing and timing validation
///     • Visual testing with maximized view for observation
///     • Per-test scene loading for isolated test scenarios
///     This pattern is essential for testing game logic that depends on Godot's frame processing,
///     physics simulations, animations, or time-based behaviors.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class SceneProcessingTest
{
    // ReSharper disable once NullableWarningSuppressionIsUsed
    private ISceneRunner runner = null!;

    /// <summary>
    ///     Loads the test scene and prepares it for testing.
    ///     Uses per-test loading to ensure each test starts with a fresh scene instance,
    ///     which is important for testing frame-based logic that may modify scene state.
    ///     Setup Process:
    ///     • Load GameScene.tscn with automatic cleanup enabled
    ///     • Maximize view for visual observation and proper event handling
    ///     • Ensure scene is ready for frame processing tests
    ///     The maximized view brings the window to foreground, making it possible to
    ///     observe the actual scene behavior during testing and ensuring proper
    ///     input event handling for interactive scenes.
    /// </summary>
    [BeforeTest]
    public void TestSetup()
    {
        runner = ISceneRunner.Load("res://Resources/GameScene.tscn", true);

        // We maximize the view to bring the window to foreground to see what actually happened in the scene.
        runner.MaximizeView();
    }

    /// <summary>
    ///     Tests scene processing for a specific number of frames.
    ///     Validates that scenes can process frames correctly without errors or crashes,
    ///     demonstrating frame-based testing for time-dependent game logic.
    ///     Testing Pattern:
    ///     • Process exactly 100 frames of scene execution
    ///     • Validate scene remains stable throughout processing
    ///     • Test frame-dependent logic and timing behaviors
    ///     • Ensure no exceptions occur during extended processing
    ///     Use Cases:
    ///     • Testing animations that complete after specific frame counts
    ///     • Validating physics simulations over time
    ///     • Testing performance and stability under frame processing load
    ///     • Verifying frame-dependent game state changes
    ///     Frame Processing Benefits:
    ///     • Predictable test duration (frame count vs time-based)
    ///     • Consistent behavior across different hardware
    ///     • Reliable testing for frame-dependent logic
    ///     • Observable scene state at specific processing points.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task SceneProcessingUntil100Frames()
        => await runner.SimulateFrames(100);

    /// <summary>
    ///     Tests scene processing until completion using signal-based termination.
    ///     Validates that scenes can signal their own completion and demonstrates
    ///     event-driven testing patterns for self-terminating game scenarios.
    ///     Testing Pattern:
    ///     • Wait for scene to emit SceneFinished signal
    ///     • Scene determines its own completion criteria
    ///     • Test validates scene can complete its intended workflow
    ///     • No predetermined frame count or timeout required
    ///     Use Cases:
    ///     • Testing cutscenes that signal when finished
    ///     • Validating level completion detection
    ///     • Testing game states that transition based on conditions
    ///     • Verifying autonomous scene behaviors and workflows
    ///     Signal-Based Testing Benefits:
    ///     • Scene controls its own completion timing
    ///     • Tests validate actual completion criteria rather than arbitrary timeouts
    ///     • Flexible testing for variable-duration scenarios
    ///     • Integration testing of scene logic and signal communication
    ///     Timeout Handling:
    ///     • Uses default AwaitSignal timeout to prevent indefinite waiting
    ///     • Scene must emit SceneFinished signal or test will timeout
    ///     • Validates that scene completion logic works correctly.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task SceneProcessingUntilSceneFinished()
        => await runner.AwaitSignal(GameScene.SignalName.SceneFinished);
}
