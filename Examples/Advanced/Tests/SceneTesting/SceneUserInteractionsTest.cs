namespace GdUnit4.Examples.Advanced.Tests.SceneTesting;

using Godot;

using Scenes;

using static Assertions;

/// <summary>
///     Demonstrates efficient scene testing combining suite-level loading with user interaction simulation.
///     This example shows the optimal approach for testing multiple user interactions on the same scene
///     by loading the scene once and reusing it across different interaction test scenarios.
///     Key Testing Concepts Demonstrated:
///     • Suite-level scene loading with [Before] for performance optimization
///     • Multiple user interaction tests sharing the same scene instance
///     • Mouse interaction simulation with precise positioning
///     • Signal monitoring and timeout-based assertions
///     • Async test execution for time-sensitive UI operations
///     Test Patterns Illustrated:
///     • Hybrid approach: suite-level setup + interaction testing
///     • Shared scene state across multiple interaction scenarios
///     • Component discovery and validation within loaded scenes
///     • Mouse position simulation and click event generation
///     • Signal emission monitoring with configurable timeouts
///     When to Use This Pattern:
///     • Testing multiple user interactions on the same UI layout
///     • Performance-critical test suites with expensive scene loading
///     • Validating different interaction scenarios on identical scene setups
///     • UI component testing where scene state remains consistent
///     Advantages Over Per-Test Loading:
///     • Significantly faster execution (single scene load)
///     • Reduced memory allocation overhead
///     • Consistent scene baseline for all interaction tests
///     • Simplified test method implementation
///     Considerations:
///     • Tests should not modify persistent scene state
///     • All tests must work with the same scene configuration
///     • Interaction tests should reset transient UI states if needed.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class SceneUserInteractionsTest
{
    // ReSharper disable once NullableWarningSuppressionIsUsed
    private ISceneRunner runner = null!;

    /// <summary>
    ///     Suite-level setup: loads the test scene once before all test methods execute.
    /// </summary>
    [Before]
    public void Before()
        => runner = ISceneRunner.Load("res://SceneTesting/Scenes/TestSceneWithButton.tscn", true);

    /// <summary>
    ///     Tests that a button correctly emits its 'Pressed' signal when clicked via mouse simulation.
    ///     This test demonstrates a complete user interaction workflow using the shared scene
    ///     loaded in the [Before] method, showcasing how multiple interaction tests can
    ///     efficiently reuse the same scene instance.
    ///     Test Workflow:
    ///     1. Locate the target button component within the pre-loaded scene
    ///     2. Set up signal monitoring to capture button press emissions
    ///     3. Simulate realistic mouse interaction (positioning + click)
    ///     4. Verify the expected signal was emitted within the specified timeout
    ///     Interaction Simulation Details:
    ///     • Mouse positioned at button center plus offset (5,5) for reliable clicking
    ///     • Left mouse button press simulated through GdUnit4 input system
    ///     • Signal monitoring captures BaseButton.SignalName.Pressed emission
    ///     • 50ms timeout prevents test hangs while allowing for processing delays
    ///     Validation Points:
    ///     • Button component exists and is properly accessible
    ///     • Signal monitoring system is correctly initialized
    ///     • Mouse simulation triggers expected button behavior
    ///     • Signal emission occurs within reasonable timeframe
    ///     Scene Requirements:
    ///     • Must contain a Button node named "ExampleButton"
    ///     • Button must be enabled and configured to emit pressed signals
    ///     • Button must have valid position and size for mouse interaction
    ///     This test can be extended with additional interaction scenarios (hover, release, etc.)
    ///     while reusing the same efficiently loaded scene instance.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the "ExampleButton" component cannot be found in the loaded scene.
    ///     Common causes include:
    ///     • Scene file path changed or scene structure modified
    ///     • Button node renamed (search is case-sensitive)
    ///     • Scene loading failed during [Before] setup
    ///     • Scene file corruption or invalid format
    ///     Troubleshooting:
    ///     • Verify scene file exists at specified path
    ///     • Check button node name matches "ExampleButton" exactly
    ///     • Ensure scene loads successfully in Godot editor.
    /// </exception>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    ///     Async execution is required for:
    ///     • Signal monitoring with timeout-based assertions
    ///     • Non-blocking test execution allowing proper frame processing
    ///     • Integration with Godot's asynchronous input processing system
    ///     • Prevention of test runner blocking during UI interaction simulation.
    /// </returns>
    [TestCase]
    public async Task MouseButtonPressedSignalIsEmitted()
    {
        // Locate the target button within the pre-loaded scene
        var button = runner.FindChild("ExampleButton") as Button
                     ?? throw new InvalidOperationException("Could not find ExampleButton in scene");

        // Verify successful button discovery and type casting
        AssertThat(button).IsNotNull();

        // Initialize signal monitoring before interaction simulation
        var monitor = AssertSignal(button).StartMonitoring();

        // Simulate realistic user mouse interaction
        runner
            .SetMousePos(button.Position + new Vector2(5, 5)) // Position mouse inside button bounds
            .SimulateMouseButtonPressed(MouseButton.Left); // Generate left mouse button press event

        // Assert signal emission within reasonable timeout window
        await monitor.IsEmitted(BaseButton.SignalName.Pressed).WithTimeout(50);
    }

    /// <summary>
    ///     Tests keyboard input handling and signal emission when Space key is pressed.
    ///     This test demonstrates keyboard interaction simulation and validates that the scene
    ///     properly responds to keyboard input by emitting the expected GameStarted signal.
    ///     Test Workflow:
    ///     1. Set up signal monitoring on the scene root node
    ///     2. Simulate Space key press through GdUnit4 input system
    ///     3. Verify GameStarted signal is emitted within timeout
    ///     Keyboard Simulation Details:
    ///     • Space key press simulated directly on the scene
    ///     • Input is processed through scene's _GuiInput method
    ///     • Signal monitoring captures TestSceneWithButton.SignalName.GameStarted
    ///     • 50ms timeout allows for input processing and signal emission
    ///     Validation Points:
    ///     • Scene properly handles keyboard input events
    ///     • _GuiInput method correctly processes Space key presses
    ///     • GameStarted signal is emitted as expected
    ///     • Input processing occurs within reasonable timeframe
    ///     Scene Requirements:
    ///     • Scene must override _GuiInput to handle InputEventKey
    ///     • Space key (Key.Space) must trigger GameStarted signal emission
    ///     • Scene must be properly focused to receive input events
    ///     This test validates keyboard-driven game state changes and demonstrates
    ///     how to test input handling beyond mouse interactions.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    ///     Async execution is required for:
    ///     • Signal monitoring with timeout-based assertions
    ///     • Non-blocking test execution allowing proper frame processing
    ///     • Integration with Godot's asynchronous input processing system
    ///     • Prevention of test runner blocking during UI interaction simulation.
    /// </returns>
    [TestCase]
    public async Task KeyPressed()
    {
        // Set up signal monitoring on the scene root to capture GameStarted emission
        var monitor = AssertSignal(runner.Scene()!).StartMonitoring();

        // Simulate Space key press to trigger game start
        runner.SimulateKeyPressed(Key.Space);

        // Assert GameStarted signal emission within reasonable timeout window
        await monitor.IsEmitted(TestSceneWithButton.SignalName.GameStarted).WithTimeout(50);
    }

    /// <summary>
    ///     Tests asynchronous scene processing and signal chaining with AwaitSignal pattern.
    ///     This test demonstrates how to wait for signals that are emitted asynchronously
    ///     after triggering an initial action, validating complete workflow execution.
    ///     Test Workflow:
    ///     1. Simulate Space key press to initiate game start sequence
    ///     2. Wait for the complete game cycle to finish (GameStopped signal)
    ///     3. Verify the asynchronous workflow completes within extended timeout
    ///     Asynchronous Processing Details:
    ///     • Space key triggers GameStarted signal immediately
    ///     • GameStarted signal connects to StartGame() method in scene
    ///     • StartGame() creates a 0.1s timer and waits asynchronously
    ///     • After timer expires, GameStopped signal is emitted
    ///     • Test waits for the final GameStopped signal with 150ms timeout
    ///     Validation Points:
    ///     • Initial keyboard input triggers expected workflow
    ///     • Asynchronous scene processing executes correctly
    ///     • Timer-based delays work as expected in test environment
    ///     • Final signal emission occurs after async operations complete
    ///     Scene Requirements:
    ///     • Space key must trigger GameStarted signal
    ///     • GameStarted signal must be connected to StartGame() method
    ///     • StartGame() must create timer and emit GameStopped after delay
    ///     • All async operations must complete within 150ms total
    ///     This test pattern is essential for validating complex game state machines,
    ///     animation sequences, or any asynchronous scene behaviors that involve
    ///     multiple signals and timer-based operations.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    ///     Async execution is required for:
    ///     • Signal monitoring with timeout-based assertions
    ///     • Non-blocking test execution allowing proper frame processing
    ///     • Integration with Godot's asynchronous input processing system
    ///     • Prevention of test runner blocking during UI interaction simulation.
    /// </returns>
    [TestCase]
    public async Task AwaitSignalOnSceneProcessing()
    {
        // Trigger the game start sequence with Space key press
        runner.SimulateKeyPressed(Key.Space);

        // Wait for the complete asynchronous workflow to finish with GameStopped signal
        // Extended timeout (150ms) accounts for input processing + timer delay + signal emission
        await runner.AwaitSignal(TestSceneWithButton.SignalName.GameStopped).WithTimeout(150);
    }
}
