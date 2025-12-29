namespace GdUnit4.Examples.Advanced.Setup.MultiProjectSetup;

using Godot;

public partial class ExampleScene : Control
{
    private string state = "Unknown";

#pragma warning disable SA1600
    public static int Check() => 0;
#pragma warning restore SA1600

    public override void _Ready()
    {
        base._Ready();
        state = "_Ready";
    }

    public string GetState() => state;
}
