namespace GdUnit4.Examples.Advanced.Tests.InputTesting;

using Godot;

using Resources;

using static Assertions;

/// <summary>
///     Demonstrates comprehensive keyboard input testing patterns based on GdUnit4Net capabilities.
///     This test suite covers essential keyboard input mechanics including:
///     • Single key press and release sequences
///     • Multiple simultaneous key presses
///     • Modifier key combinations (Ctrl, Shift, Alt)
///     • Physical key vs logical key handling
///     • Action-based input mapping
///     • Key state validation through Input singleton
///     Tests validate input processing by checking both Godot's Input singleton and scene properties.
/// </summary>
[TestSuite]
[RequireGodotRuntime]
public class KeyboardInputTest
{
    private ISceneRunner runner = null!;
    private TestSceneWithButton scene = null!;

    /// <summary>
    ///     Suite-level setup: loads the test scene once before all test methods execute.
    /// </summary>
    [Before]
    public void Before()
    {
        runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);
        scene = runner.Scene() as TestSceneWithButton
                ?? throw new InvalidOperationException("Could not cast scene");
    }

    /// <summary>
    ///     Tests single key press processing.
    ///     Validates key input through both scene properties and Input singleton.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task SingleKeyPress()
    {
        runner.SimulateKeyPressed(Key.Space);
        await runner.AwaitInputProcessed();

        // Validate through scene property and Input singleton
        AssertThat(scene.LastKeyPressed).IsEqual(Key.Space);
        AssertThat(Input.IsKeyPressed(Key.Space)).IsFalse(); // The key is released
        AssertThat(Input.IsPhysicalKeyPressed(Key.Space)).IsFalse(); // The key is released
    }

    /// <summary>
    ///     Tests key press and release sequence.
    ///     Validates that keys can be pressed, held, and then released properly.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task KeyPressAndRelease()
    {
        // Press and hold key
        runner.SimulateKeyPress(Key.A);
        await runner.AwaitInputProcessed();

        AssertThat(scene.LastKeyPressed).IsEqual(Key.A);
        AssertThat(Input.IsKeyPressed(Key.A)).IsTrue();

        // Release key
        runner.SimulateKeyRelease(Key.A);
        await runner.AwaitInputProcessed();

        AssertThat(Input.IsKeyPressed(Key.A)).IsFalse(); // The key is released
    }

    /// <summary>
    ///     Tests multiple keys pressed simultaneously.
    ///     Validates that multiple keys can be held at the same time.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task MultipleKeysPressed()
    {
        // Press multiple keys
        runner
            .SimulateKeyPress(Key.W)
            .SimulateKeyPress(Key.A)
            .SimulateKeyPress(Key.S)
            .SimulateKeyPress(Key.D);

        await runner.AwaitInputProcessed();

        // All keys should be pressed
        AssertThat(Input.IsKeyPressed(Key.W)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.A)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.S)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.D)).IsTrue();

        // Release one key while others remain pressed
        runner.SimulateKeyRelease(Key.W);
        await runner.AwaitInputProcessed();

        AssertThat(Input.IsKeyPressed(Key.W)).IsFalse();
        AssertThat(Input.IsKeyPressed(Key.A)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.S)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.D)).IsTrue();
    }

    /// <summary>
    ///     Tests modifier key combinations.
    ///     Validates Ctrl+Key combinations work correctly.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task ModifierKeyCombinations()
    {
        // Press Ctrl+S combination
        runner
            .SimulateKeyPress(Key.Ctrl)
            .SimulateKeyPress(Key.S);

        await runner.AwaitInputProcessed();

        AssertThat(Input.IsKeyPressed(Key.Ctrl)).IsTrue();
        AssertThat(Input.IsKeyPressed(Key.S)).IsTrue();
        AssertThat(scene.LastKeyPressed).IsEqual(Key.S);

        // Release keys in reverse order
        runner
            .SimulateKeyRelease(Key.S)
            .SimulateKeyRelease(Key.Ctrl);

        await runner.AwaitInputProcessed();

        AssertThat(Input.IsKeyPressed(Key.Ctrl)).IsFalse(); // The key is released
        AssertThat(Input.IsKeyPressed(Key.S)).IsFalse(); // The key is released
    }

    /// <summary>
    ///     Tests Shift+Key combinations for uppercase input.
    ///     Validates modifier key behavior with letter keys.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task ShiftKeyCombination()
    {
        // Press Shift+A combination
        runner
            .SimulateKeyPress(Key.Shift)
            .SimulateKeyPress(Key.A);

        await runner.AwaitInputProcessed();

        AssertThat(scene.LastKeyPressed).IsEqual(Key.A);
        AssertThat(Input.IsKeyPressed(Key.Shift)).IsTrue(); // The key is current pressing
        AssertThat(Input.IsKeyPressed(Key.A)).IsTrue(); // The key is current pressing
    }

    /// <summary>
    ///     Tests action-based input mapping.
    ///     Validates that key presses trigger mapped actions.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task ActionBasedInput()
    {
        // Ensure action exists in InputMap
        AssertThat(InputMap.HasAction("ui_up")).IsTrue();

        runner.SimulateActionPress("ui_up");
        await runner.AwaitInputProcessed();

        AssertThat(Input.IsActionPressed("ui_up")).IsTrue();

        runner.SimulateActionRelease("ui_up");
        await runner.AwaitInputProcessed();

        AssertThat(Input.IsActionPressed("ui_up")).IsFalse();
    }

    /// <summary>
    ///     Tests text input functionality.
    ///     Validates keyboard input in text controls.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task TextInputProcessing()
    {
        var lineEdit = runner.FindChild("TextInput") as LineEdit
                       ?? throw new InvalidOperationException("Could not find TextInput");

        lineEdit.Text = string.Empty;

        // Focus the text input and clear any existing content
        lineEdit.GrabFocus();
        AssertThat(lineEdit.HasFocus()).IsTrue();

        // Type individual characters
        runner.SimulateKeyPressed(Key.H);
        runner.SimulateKeyPressed(Key.I);
        await runner.AwaitInputProcessed();

        // Verify text accumulation
        AssertThat(lineEdit.Text).IsEqual("HI");
    }
}
