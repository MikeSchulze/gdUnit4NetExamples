namespace GdUnit4.Examples.Advanced.Tests.Resources;

using Godot;

/// <summary>
///     Example game scene demonstrating testable game logic and scene completion patterns.
///     This Node2D serves as a showcase for testing game scenes that have autonomous behavior,
///     completion criteria, and interactive elements that respond to game events.
///     Scene Functionality:
///     • Collision detection and response handling
///     • Body tracking and lifecycle management
///     • Scene completion signaling for workflow testing
///     • Autonomous scene behavior that can be validated through testing
///     Testing Applications:
///     • Frame-based processing validation with SimulateFrames()
///     • Signal-based completion testing with AwaitSignal()
///     • Game logic testing with collision detection and response
///     • Scene lifecycle testing and memory management validation
///     Design Pattern:
///     • Game Event → Body Processing → Scene Completion Signal
///     • Demonstrates testable game logic with observable outcomes
///     • Self-contained scene behavior that signals completion
///     • Clean separation between game logic and testing infrastructure.
/// </summary>
public partial class GameScene : Node2D
{
    /// <summary>
    ///     Signal emitted when the scene has completed its intended workflow.
    ///     This signal indicates that the scene has finished processing and can be used
    ///     by tests to validate scene completion without relying on arbitrary timeouts.
    ///     Emission Context:
    ///     • Triggered when OnGroundEntered processes a collision
    ///     • Indicates scene workflow has reached completion
    ///     • Provides deterministic endpoint for signal-based testing
    ///     Testing Usage:
    ///     • Use with AwaitSignal() to test scene completion
    ///     • Validates scene can properly signal workflow completion
    ///     • Enables event-driven testing without fixed timeouts.
    /// </summary>
    [Signal]
    public delegate void SceneFinishedEventHandler();

    /// <summary>
    ///     Gets the last body that entered the ground collision area.
    ///     This property tracks collision events and provides observable state
    ///     for testing collision detection and body management logic.
    ///     State Information:
    ///     • Contains reference to body that triggered collision
    ///     • Set to null after body is freed during collision processing
    ///     • Provides validation point for collision detection testing
    ///     Testing Applications:
    ///     • Verify collision detection works correctly
    ///     • Validate body lifecycle management
    ///     • Test collision response and cleanup logic.
    /// </summary>
    public Node2D? Body { get; private set; }

    /// <summary>
    ///     Handles collision events when bodies enter the ground area.
    ///     This method demonstrates game collision logic that processes colliding bodies,
    ///     manages their lifecycle, and signals scene completion.
    ///     Collision Processing:
    ///     1. Store reference to the colliding body
    ///     2. Free the body from the scene tree
    ///     3. Emit SceneFinished signal to indicate completion
    ///     Game Logic Pattern:
    ///     • Collision Detection → Body Processing → Scene State Change
    ///     • Demonstrates cleanup and lifecycle management
    ///     • Provides observable completion through signal emission
    ///     Testing Validation Points:
    ///     • Body property is set before cleanup
    ///     • Body is properly freed from scene tree
    ///     • SceneFinished signal is emitted after processing
    ///     • Scene reaches completion state after collision event
    ///     Use Cases:
    ///     • Testing collision response logic
    ///     • Validating object lifecycle management
    ///     • Testing scene completion workflows
    ///     • Verifying proper resource cleanup and signal emission.
    /// </summary>
    /// <param name="body">The Node2D body that entered the collision area.</param>
    public void OnGroundEntered(Node2D body)
    {
        Body = body;
        body?.Free();
        EmitSignalSceneFinished();
    }
}
