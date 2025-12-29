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
        var current = runner.Scene();
        AssertThat(current)
            .IsNotNull()
            .IsInstanceOf<ExampleScene>();

        var scene = current as ExampleScene;
        AssertThat(scene?.GetState()).IsEqual("_Ready");
    }
}
