namespace GdUnit4.Examples.Advanced.Tests.SceneTesting.Scenes;

using Godot;

/// <summary>
///     A comprehensive test scene script demonstrating common Godot patterns for scene testing.
///     This Control node serves as the root of TestSceneWithButton.tscn and implements
///     various interaction patterns including button handling, keyboard input processing,
///     signal chaining, and asynchronous operations for comprehensive test coverage.
///     Scene Architecture:
///     • Root: TestSceneWithButton (Control) - Main scene controller
///     • Child: ExampleButton (Button) - Interactive UI element for mouse testing
///     • Additional UI elements as needed for comprehensive interaction testing
///     Signal Flow Patterns:
///     • Button press → GameStarted signal emission
///     • Keyboard input (Space) → GameStarted signal emission
///     • GameStarted signal → StartGame() method execution
///     • StartGame() async operation → GameStopped signal emission
///     Key Features Demonstrated:
///     • Multiple input sources triggering the same signal (button + keyboard)
///     • Signal-to-method connection patterns using Callable.From()
///     • Asynchronous operations with timer-based delays
///     • Input event processing through _GuiInput override
///     • Signal chaining for complex workflow testing
///     Testing Applications:
///     • Mouse interaction validation through button components
///     • Keyboard input processing and event handling
///     • Asynchronous scene behavior with timer-based operations
///     • Signal emission patterns and workflow state management
///     • Scene loading and component discovery patterns
///     Design Principles:
///     • Minimal complexity for clear test examples
///     • Multiple interaction paths to the same outcomes
///     • Observable behavior through signal emissions
///     • Predictable timing for reliable test assertions
///     • Reusable patterns applicable to real game scenarios.
/// </summary>
public partial class TestSceneWithButton : Control
{
    /// <summary>
    ///     Signal emitted when the game/interaction sequence begins.
    ///     This signal serves as a central event hub that can be triggered by multiple
    ///     input sources (button clicks, keyboard input) and connects to downstream
    ///     processing workflows.
    ///     Emission Triggers:
    ///     • Button press events via OnButtonPressed() method
    ///     • Space key press events via _GuiInput() method
    ///     • Direct method calls for programmatic testing
    ///     Connected Handlers:
    ///     • StartGame() method - initiates asynchronous game sequence
    ///     Testing Usage:
    ///     • Monitor this signal to verify input processing works correctly
    ///     • Use as trigger point for downstream workflow validation
    ///     • Verify multiple input sources can trigger the same game state
    ///     Signal Characteristics:
    ///     • Emitted immediately when triggered (no delays)
    ///     • Can be emitted multiple times safely
    ///     • Serves as entry point to asynchronous processing chain.
    /// </summary>
    [Signal]
    public delegate void GameStartedEventHandler();

    /// <summary>
    ///     Signal emitted when the game/interaction sequence completes.
    ///     This signal represents the end of an asynchronous processing workflow
    ///     and is used to test timer-based operations and signal chaining patterns.
    ///     Emission Context:
    ///     • Emitted by StartGame() method after timer completion
    ///     • Represents the end of a timed asynchronous operation
    ///     • Follows GameStarted signal in the workflow chain
    ///     Timing Characteristics:
    ///     • Emitted approximately 100ms after GameStarted (timer delay)
    ///     • Timing may vary slightly due to frame processing
    ///     • Reliable for testing with appropriate timeout margins
    ///     Testing Applications:
    ///     • Validate asynchronous scene processing works correctly
    ///     • Test signal chaining and workflow completion
    ///     • Verify timer-based operations in scene context
    ///     • Confirm complete interaction cycles from start to finish
    ///     Usage Pattern:
    ///     • GameStarted → StartGame() → Timer.Timeout → GameStopped
    ///     • Represents a complete testable interaction workflow.
    /// </summary>
    [Signal]
    public delegate void GameStoppedEventHandler();

    /// <summary>
    ///     Scene initialization method that establishes signal connections.
    ///     This method demonstrates the standard Godot pattern for connecting signals
    ///     to methods during scene setup, creating the workflow chain that enables
    ///     asynchronous processing testing.
    ///     Connection Established:
    ///     • GameStarted signal → StartGame() method via Callable.From()
    ///     • Creates automatic workflow progression for testing
    ///     • Enables signal chaining validation in test scenarios
    ///     Pattern Benefits:
    ///     • Automatic workflow initiation when GameStarted is emitted
    ///     • Decoupled design allowing multiple signal sources
    ///     • Testable signal connection patterns
    ///     • Realistic scene setup matching real game scenarios
    ///     Testing Implications:
    ///     • Signal connections are established before any test execution
    ///     • Workflows will execute automatically when triggered
    ///     • Connection can be validated independently if needed
    ///     • Demonstrates proper signal wiring for complex scenes.
    /// </summary>
    public override void _Ready()
        => Connect(SignalName.GameStarted, Callable.From(StartGame));

    /// <summary>
    ///     Button press event handler that initiates the game sequence.
    ///     This method serves as one of multiple entry points into the game workflow,
    ///     demonstrating how UI interactions can trigger broader scene state changes
    ///     through signal emission patterns.
    ///     Functionality:
    ///     • Responds to button press events from connected UI elements
    ///     • Emits GameStarted signal to initiate workflow chain
    ///     • Provides observable entry point for mouse-based interaction testing
    ///     Connection Pattern:
    ///     • Typically connected to Button.pressed signal in scene setup
    ///     • Can be called directly for programmatic testing
    ///     • Demonstrates standard UI event handling patterns
    ///     Testing Applications:
    ///     • Validate button interaction triggers correct signal emission
    ///     • Test UI-driven workflow initiation
    ///     • Verify signal emission occurs immediately upon button press
    ///     • Confirm UI elements properly connect to scene logic
    ///     Method Characteristics:
    ///     • Executes synchronously and returns immediately
    ///     • No validation or error handling - focuses on signal emission
    ///     • Can be safely called multiple times
    ///     • Represents the start of an observable interaction chain
    ///     Design Pattern:
    ///     • UI Event → Signal Emission → Workflow Initiation
    ///     • Separates UI handling from business logic through signals
    ///     • Enables multiple UI elements to trigger the same workflow.
    /// </summary>
    public void OnButtonPressed()
        => EmitSignal(SignalName.GameStarted);

    /// <summary>
    ///     Input event processing method that handles keyboard interactions.
    ///     This method demonstrates Godot's input handling patterns and provides
    ///     an alternative input pathway to the same game workflow, enabling
    ///     comprehensive input testing scenarios.
    ///     Input Processing:
    ///     • Filters for InputEventKey events specifically
    ///     • Checks for Space key (Key.Space) with Pressed state
    ///     • Ignores key release events and other key types
    ///     • Emits GameStarted signal when Space is pressed
    ///     Event Filtering Logic:
    ///     • Pattern matching used for clean event type checking
    ///     • Only processes pressed events (not releases)
    ///     • Specific key targeting prevents unintended activations
    ///     • Immediate signal emission upon successful match
    ///     Testing Capabilities:
    ///     • Validates keyboard input processing works correctly
    ///     • Tests alternative input method to same workflow
    ///     • Verifies input filtering logic operates as expected
    ///     • Confirms multiple input types can trigger identical outcomes
    ///     Input Method Characteristics:
    ///     • Processes input at scene level (not specific UI elements)
    ///     • Requires scene to have input focus for events to be received
    ///     • Works alongside other input methods without conflicts
    ///     • Provides consistent behavior with button-based interactions
    ///     Pattern Demonstration:
    ///     • Input Event Processing → Signal Emission → Workflow Initiation
    ///     • Alternative input pathway to same destination
    ///     • Clean separation of input handling from workflow logic.
    /// </summary>
    /// <param name="event">
    ///     The input event to process, filtered for InputEventKey instances.
    ///     Only Space key press events will trigger signal emission.
    /// </param>
    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventKey { Pressed: true, Keycode: Key.Space })
            EmitSignal(SignalName.GameStarted);
    }

    /// <summary>
    ///     Asynchronous workflow method that simulates game processing with timer delay.
    ///     This method demonstrates async operations within Godot scenes and provides
    ///     a testable pattern for time-based processing that concludes with signal emission.
    ///     Async Workflow:
    ///     1. Create a SceneTreeTimer with 100ms (0.1s) duration
    ///     2. Await the timer's Timeout signal using ToSignal pattern
    ///     3. Emit GameStopped signal to indicate workflow completion
    ///     4. Method completes, allowing signal chaining to finish
    ///     Timer Implementation:
    ///     • Uses GetTree().CreateTimer() for scene-based timing
    ///     • 100ms delay provides measurable but brief async operation
    ///     • Timer automatically cleaned up by Godot after timeout
    ///     • ToSignal() provides clean async/await integration
    ///     Testing Implications:
    ///     • Creates predictable delay for timeout-based test assertions
    ///     • Demonstrates async operations that can be validated in tests
    ///     • Provides complete workflow from start signal to end signal
    ///     • Timing allows for reliable test execution with appropriate margins
    ///     Signal Connection:
    ///     • Connected to GameStarted signal via _Ready() method
    ///     • Executes automatically when GameStarted is emitted
    ///     • Represents middle processing step in signal chain
    ///     • Culminates workflow with GameStopped emission
    ///     Async Pattern Benefits:
    ///     • Demonstrates real-world async scene processing
    ///     • Provides testable time-based operations
    ///     • Shows proper integration of timers with signal patterns
    ///     • Creates observable workflow completion events
    ///     Method Characteristics:
    ///     • Returns immediately but continues processing asynchronously
    ///     • Uses async void pattern appropriate for event handlers
    ///     • Timer-based delay makes async behavior observable in tests
    ///     • Completes workflow chain initiated by input events.
    /// </summary>
    private async void StartGame()
    {
        // Create scene timer for realistic async operation simulation
        var timer = GetTree().CreateTimer(0.1);

        // Wait for timer completion using Godot's async signal pattern
        await ToSignal(timer, Timer.SignalName.Timeout);

        // Emit completion signal to indicate workflow finished
        EmitSignal(SignalName.GameStopped);
    }
}
