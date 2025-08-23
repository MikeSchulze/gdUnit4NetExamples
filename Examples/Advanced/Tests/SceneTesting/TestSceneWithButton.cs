namespace GdUnit4.Examples.Advanced.Tests.SceneTesting;

using Godot;

/// <summary>
///     A simple test scene script used for demonstrating scene testing patterns.
///     This Control node serves as the root of TestSceneWithButton.tscn and provides
///     basic functionality for testing scene loading, node interaction, and signal handling.
///     Scene Structure:
///     - Root: TestSceneWithButton (Control)
///     - Child nodes: Button, Label, and other UI elements for testing
///     Key features:
///     - Button interaction handling
///     - Simple console output for verification
///     - Demonstrates basic scene script patterns
///     Usage in tests:
///     - Load via ISceneRunner.Load("res://SceneTesting/TestSceneWithButton.tscn")
///     - Access this script instance through the loaded scene
///     - Test button interactions and method calls
///     - Verify scene behavior and state changes
///     This scene is specifically designed for testing purposes and demonstrates
///     common patterns found in real Godot scenes while keeping complexity minimal
///     for clear test examples.
/// </summary>
public partial class TestSceneWithButton : Control
{
    /// <summary>
    ///     Handles button press events from child Button nodes.
    ///     This method demonstrates basic event handling and provides
    ///     observable output for test verification.
    ///     Functionality:
    ///     - Responds to button press signals
    ///     - Outputs confirmation message to console
    ///     - Can be monitored in tests for verification
    ///     Testing considerations:
    ///     - Console output can be captured and verified in tests
    ///     - Method can be called directly for unit testing
    ///     - Signal connections can be tested separately
    ///     Expected behavior:
    ///     - Prints "OnButtonPressed" to console/debug output
    ///     - Executes without errors
    ///     - Can be triggered multiple times safely.
    /// </summary>
    public void OnButtonPressed() => GD.Print("OnButtonPressed");
}
