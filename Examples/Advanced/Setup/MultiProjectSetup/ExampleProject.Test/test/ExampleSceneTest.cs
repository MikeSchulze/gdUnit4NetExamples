namespace GdUnit4.Examples.Advanced.Setup.MultiProjectSetup.Test;

using Godot;

using static Assertions;

[TestSuite]
[RequireGodotRuntime]
public class ExampleSceneTest
{
    [TestCase]
    public async Task SceneLoading()
    {
        // Example to load a scene by resource path form subproject 'ExampleProject'
        var runner = ISceneRunner.Load("res://src/ExampleScene.tscn", true);

        GD.Print("Loading scene...");
        runner.MaximizeView();
        await runner.SimulateFrames(100);

        // verify
        AssertThat(runner).IsNotNull();
        AssertThat(runner.Scene()).IsNotNull();
    }
}
