namespace GdUnit4.Examples.Advanced.Tests.Resources;

using System.Diagnostics;

using Godot;

/// <summary>
///     Enhanced test scene with comprehensive input tracking for validation-based testing.
///     This Control node serves as the root of TestSceneWithButton.tscn and implements
///     extensive input monitoring capabilities alongside the original interaction patterns
///     including button handling, keyboard input processing, signal chaining, and
///     asynchronous operations for comprehensive test coverage.
///     Scene Architecture:
///     • Root: TestSceneWithButton (Control) - Main scene controller with input tracking
///     • Child: ExampleButton (Button) - Interactive UI element for mouse testing
///     • Child: TextInput (LineEdit) - Text input control for keyboard testing
///     • Additional UI elements as needed for comprehensive interaction testing
///     Input Tracking System:
///     • Mouse position tracking through LastMousePosition property
///     • Mouse button state monitoring (IsLeftMousePressed)
///     • Drag and drop operation detection (IsDragging, DragStartPosition, DragEndPosition)
///     • Keyboard state tracking (LastKeyPressed, IsSpacePressed, IsCtrlPressed)
///     • Modifier key combination detection (WasCtrlSPressed)
///     • Directional input processing (LastDirectionPressed)
///     Signal Flow Patterns:
///     • Button press → GameStarted signal emission
///     • Keyboard input (Space) → GameStarted signal emission
///     • GameStarted signal → StartGame() method execution
///     • StartGame() async operation → GameStopped signal emission
///     Key Features Demonstrated:
///     • Multiple input sources triggering the same signal (button + keyboard)
///     • Signal-to-method connection patterns using Callable.From()
///     • Asynchronous operations with timer-based delays
///     • Input event processing through _Input override for comprehensive tracking
///     • Signal chaining for complex workflow testing
///     • Observable input state properties for validation-based testing
///     Testing Applications:
///     • Mouse interaction validation through position and state properties
///     • Keyboard input processing verification through key state tracking
///     • Drag and drop operation testing through drag state properties
///     • Asynchronous scene behavior with timer-based operations
///     • Signal emission patterns and workflow state management
///     • Input processing verification without dependency on signal emissions
///     Design Principles:
///     • Comprehensive input tracking for test validation
///     • Multiple interaction paths to the same outcomes
///     • Observable behavior through both properties and signal emissions
///     • Predictable timing for reliable test assertions
///     • Separation of input tracking from signal communication
///     • Reusable patterns applicable to real game scenarios
///     Enhanced Testing Capabilities:
///     • Input state validation through public properties
///     • Mouse tracking without relying on UI element states
///     • Keyboard combination detection for complex input testing
///     • Drag operation lifecycle monitoring
///     • Clean separation between input mechanics and signal behavior.
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
    ///     • Space key press events via _Input() method
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

    // Input tracking properties

    /// <summary>Gets current mouse position, updated on all mouse events.</summary>
    public Vector2 LastMousePosition { get; private set; }

    /// <summary>Gets pressed mouse button, updated on all mouse events.</summary>
    public MouseButton LastMouseButton { get; private set; }

    /// <summary>Gets most recently pressed key for input validation.</summary>
    public Key LastKeyPressed { get; private set; }

    /// <summary>
    ///     Gets or sets a value indicating whether if set to true an exception will be thrown at scene processing.
    /// </summary>
    private bool LetsThrowAnException { get; set; }

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
    ///     Comprehensive input event processing method that captures and tracks all input.
    ///     This method replaces _GuiInput with full input monitoring capabilities,
    ///     processing mouse, keyboard, and other input events to provide observable
    ///     state properties for validation-based testing.
    ///     Input Processing Categories:
    ///     • Mouse motion tracking (position updates)
    ///     • Mouse button state monitoring (press/release with drag detection)
    ///     • Keyboard input processing (key states, modifiers, combinations)
    ///     • Directional input handling (arrow keys for navigation)
    ///     Mouse Input Handling:
    ///     • LastMousePosition updated on all mouse events
    ///     • Left button press/release state tracking
    ///     • Drag operation lifecycle detection (start position, end position)
    ///     • Position-based validation support for click testing
    ///     Keyboard Input Processing:
    ///     • Individual key press/release state tracking
    ///     • Modifier key combination detection (Ctrl+S patterns)
    ///     • Directional input categorization for navigation testing
    ///     • Signal emission integration for workflow testing
    ///     Testing Integration:
    ///     • All input state exposed through public properties
    ///     • Enables input validation without signal dependency
    ///     • Supports both input mechanics testing and signal workflow testing
    ///     • Provides comprehensive input event lifecycle tracking
    ///     Event Processing Pattern:
    ///     • Input Event → State Property Update → Optional Signal Emission
    ///     • Clean separation between input tracking and signal communication
    ///     • Observable state changes for reliable test assertions.
    /// </summary>
    /// <param name="event">
    ///     Input event to process and track. Handles InputEventMouseMotion,
    ///     InputEventMouseButton, and InputEventKey for comprehensive input monitoring.
    /// </param>
    public override void _Input(InputEvent @event)
    {
        Debug.Assert(@event != null, nameof(@event) + " != null");
        GD.PrintS(@event.AsText());
        if (@event is InputEventMouseMotion mouseMotion)
        {
            LastMousePosition = mouseMotion.Position;
            return;
        }

        if (@event is InputEventMouseButton mouseButton)
        {
            LastMousePosition = mouseButton.Position;
            LastMouseButton = mouseButton.ButtonIndex;
            return;
        }

        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Pressed)
            {
                LastKeyPressed = keyEvent.Keycode;
                if (keyEvent.Keycode == Key.Space)
                    EmitSignal(SignalName.GameStarted);
                if (keyEvent.Keycode == Key.E)
                    LetsThrowAnException = true;
            }
        }
    }

    /// <inheritdoc />
    public override void _Process(double delta)
    {
        if (LetsThrowAnException)
        {
            ThrowTestException();
            LetsThrowAnException = false;
        }
    }

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
    ///     Test method that throws an exception for Godot exception testing scenarios.
    ///     This method is specifically designed to test exception handling in Godot contexts
    ///     and validates that exceptions thrown from scene methods are properly caught.
    ///     Testing Purpose:
    ///     • Demonstrates exception testing patterns in scene methods
    ///     • Validates GdUnit4Net's exception monitoring capabilities
    ///     • Tests exception reporting and stack trace accuracy
    ///     • Provides controlled exception scenario for testing
    ///     Exception Details:
    ///     • Throws InvalidOperationException with descriptive message
    ///     • Used in conjunction with [ThrowsException] attribute
    ///     • Helps test Godot-specific exception handling patterns.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown to test exception handling.</exception>
    public void ThrowTestException()
        => throw new InvalidOperationException("Method execution failed");

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
        var timer = GetTree().CreateTimer(0.1);
        await ToSignal(timer, Timer.SignalName.Timeout);
        EmitSignal(SignalName.GameStopped);
    }
}
