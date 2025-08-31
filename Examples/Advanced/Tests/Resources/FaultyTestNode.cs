namespace GdUnit4.Examples.Advanced.Tests.Resources;

using Godot;

/// <summary>
///     Test node that throws an exception during initialization.
///     Used to test exception handling in Godot node lifecycle methods.
/// </summary>
public partial class FaultyTestNode : Node
{
    /// <summary>
    ///     Node ready callback that throws an exception for testing purposes.
    /// </summary>
    /// <exception cref="InvalidOperationException">Always thrown to test exception handling.</exception>
    public override void _Ready()
        => throw new InvalidOperationException("Node initialization failed");
}
