namespace GdUnit4.Examples.Advanced.Tests.ExceptionTesting;

using Core.Execution.Exceptions;

using Godot;

using Resources;

/// <summary>
///     Demonstrates Godot-specific exception testing and monitoring patterns in GdUnit4Net.
///     This test suite shows how to test exceptions that occur during Godot runtime including:
///     • Testing exceptions thrown in Godot node lifecycle methods (_Ready, _Process)
///     • Using GodotExceptionMonitor attribute to monitor exceptions without failing tests
///     • Testing exceptions during scene tree operations and frame processing
///     • Validating exception messages and source locations from Godot contexts
///     • Testing scene runner exception handling during invoke operations
///     • Monitoring Godot error reporting (GD.PushError) as test failures
///     Exception monitoring helps catch silent failures and runtime errors in Godot scenes.
/// </summary>
/// <remarks>
///     GdUnit4Net provides the [GodotExceptionMonitor] attribute to capture exceptions that occur
///     during Godot's main thread execution. These exceptions are normally caught and hidden by
///     Godot's CSharpInstanceBridge.Call method. The monitor makes these silent exceptions visible
///     and testable, enabling proper validation of error conditions in Godot runtime contexts.
/// </remarks>
[TestSuite]
[RequireGodotRuntime]
public class GodotRuntimeExceptionTest
{
    /// <summary>
    ///     Tests exception handling when adding faulty nodes to the scene tree.
    ///     Validates that exceptions thrown in node lifecycle methods (_Ready) are properly caught
    ///     and reported with accurate location information.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(InvalidOperationException), "Node initialization failed")]
    public void TestNodeLifecycleException()
    {
        var sceneTree = (SceneTree)Engine.GetMainLoop();
        sceneTree.Root.AddChild(new FaultyTestNode());
    }

    /// <summary>
    ///     Tests normal scene processing where exceptions occur but remain silent.
    ///     Demonstrates that without monitoring, exceptions thrown during frame processing
    ///     are caught silently by Godot runtime and do not cause test failures.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    public async Task TestSilentExceptionDuringProcessing()
    {
        var runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);

        // Process several frames - no exceptions occurs
        await runner.SimulateFrames(5);

        // Press key E where will result to an exception at runtime but silently caught by Godot runtime
        runner.SimulateKeyPressed(Key.E);
        await runner.SimulateFrames(5);
    }

    /// <summary>
    ///     Tests exception monitoring during scene processing with GodotExceptionMonitor.
    ///     Uses GodotExceptionMonitor to observe exceptions that occur during frame processing.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    [GodotExceptionMonitor]
    public async Task TestExceptionMonitoringDuringProcessing()
    {
        var runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);

        // Process several frames, there is no exception thrown the test will be succeeded
        await runner.SimulateFrames(5);
    }

    /// <summary>
    ///     Tests exception monitoring during scene processing with GodotExceptionMonitor.
    ///     Uses GodotExceptionMonitor to capture exceptions that occur during frame processing
    ///     and convert them into test failures for proper validation.
    /// </summary>
    /// <returns>
    ///     A Task representing the asynchronous test operation.
    /// </returns>
    [TestCase]
    [GodotExceptionMonitor]
    [ThrowsException(typeof(InvalidOperationException), "Method execution failed")]
    public async Task TestMonitoringExceptionDuringProcessing()
    {
        var runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);

        // Process several frames - no exceptions occurs
        await runner.SimulateFrames(5);

        // Press key E where will result to an exception at runtime we're now monitoring and propagate
        runner.SimulateKeyPressed(Key.E);
        await runner.SimulateFrames(5);
    }

    /// <summary>
    ///     Tests exception handling during scene method invocation.
    ///     Validates that exceptions thrown when invoking scene methods are properly
    ///     caught and reported with accurate stack trace information.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(InvalidOperationException), "Method execution failed")]
    public void TestSceneMethodException()
    {
        var runner = ISceneRunner.Load("res://Resources/TestSceneWithButton.tscn", true);
        var scene = runner.Scene() as TestSceneWithButton
                    ?? throw new InvalidOperationException("Could not cast scene");

        // Call a method that throws an exception
        scene.ThrowTestException();
    }

    /// <summary>
    ///     Tests Godot error reporting integration with test failures.
    ///     Validates that GD.PushError calls are captured and converted to test failures
    ///     for better error visibility during testing.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(TestFailedException), "Godot error detected")]
    public void TestGodotErrorReporting()
        => GD.PushError("Godot error detected"); // Godot errors should be captured as test failures
}
