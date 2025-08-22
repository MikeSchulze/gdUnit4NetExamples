#pragma warning disable CS8604 // Possible null reference argument.
namespace GdUnit4.Examples.Basics.Tests.OnGodotObjects;

using System.Diagnostics.CodeAnalysis;

using Godot;

using static Assertions;

/// <summary>
///     Demonstrates testing basic Godot Node objects and their properties.
///     This test class shows how to test:
///     - Node objects and their properties with proper memory management
///     - RefCounted objects (automatic memory management)
///     - Node hierarchy and parent-child relationships
///     - Different Node types (Node2D, Node3D, Control) and their specialized properties
///     - Memory management differences between Node and RefCounted objects
///     - Proper use of AutoFree() for Node cleanup
///     - Fluent assertion chaining for related object properties
///     Essential patterns for testing Godot-specific Node functionality and managing object lifecycles.
/// </summary>
[TestSuite]
public class GodotNodeTests
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
    /// <summary>
    ///     Tests basic Node creation and properties.
    ///     Demonstrates fundamental Node testing patterns with proper memory management.
    ///     Shows how to validate basic Node properties and state after creation.
    ///     Expected result: Node is created with correct default properties and properly cleaned up.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void TestBasicNodeCreation()
    {
        // Create a Node and register for auto-cleanup
        var player = AutoFree(new Player("Hero", 10, 100.0f, true));

        AssertThat(player)
            .IsNotNull()
            .IsEqual(new Player("Hero", 10, 100.0f, true));
    }

    /// <summary>
    ///     Tests Godot object equality and comparison patterns.
    ///     Demonstrates different approaches to object comparison in tests.
    ///     Shows how reference equality differs from value equality.
    ///     Expected result: Object references and values behave according to C# semantics.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void TestObjectEqualityAndComparison()
    {
        // Create objects with same values
        var player1 = AutoFree(new Player("Mage", 15, 75.0f, true));
        var player2 = AutoFree(new Player("Mage", 15, 75.0f, true));
        var player3 = AutoFree(new Player("Mage", 15, 75.0f, false));
        var player4 = player1; // Same reference

        // Test reference equality
        AssertObject(player1)
            .IsNotSame(player2) // Different instances
            .IsSame(player4) // Same reference
            .IsEqual(player2) // equal by properties
            .IsNotEqual(player3); // Different properties
    }

    /// <summary>
    ///     Tests Node hierarchy creation and parent-child relationships.
    ///     Shows how to test scene structure and node tree operations.
    ///     Demonstrates validating complex object relationships and tree structures.
    ///     Expected result: Parent-child relationships are correctly established and accessible.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void TestNodeHierarchy()
    {
        // Create parent and child nodes
        var parent = AutoFree(new Node());
        var child1 = AutoFree(new Node());
        var child2 = AutoFree(new Node());

        // Set up hierarchy
        parent.Name = "Parent";
        child1.Name = "Child1";
        child2.Name = "Child2";

        parent.AddChild(child1);
        parent.AddChild(child2);

        // Test hierarchy structure
        AssertThat(parent.GetChildCount()).IsEqual(2);
        AssertThat(child1.GetParent()).IsEqual(parent);
        AssertThat(child2.GetParent()).IsEqual(parent);
        AssertThat(parent.GetChild(0)).IsEqual(child1);
        AssertThat(parent.GetChild(1)).IsEqual(child2);
    }

    /// <summary>
    ///     Tests specific Node types (Node2D, Node3D).
    ///     Demonstrates testing inheritance and type-specific functionality.
    ///     Shows proper use of fluent assertion chaining for related properties.
    ///     Expected result: Different node types have their expected default properties and pass type checks.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void TestSpecificNodeTypes()
    {
        // Test Node2D - demonstrates fluent chaining for instance type validation
        var node2D = AutoFree(new Node2D());
        AssertThat(node2D)
            .IsInstanceOf<Node2D>()
            .IsInstanceOf<Node>();

        // Test Node3D - shows inheritance chain validation
        var node3D = AutoFree(new Node3D());
        AssertThat(node3D)
            .IsInstanceOf<Node3D>()
            .IsInstanceOf<Node>();
    }

    /// <summary>
    ///     Tests RefCounted objects which have automatic memory management.
    ///     Shows the difference between Node (manual Free()) and RefCounted (automatic).
    ///     Demonstrates that RefCounted objects don't need AutoFree() registration.
    ///     Expected result: RefCounted objects work correctly without manual memory management.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void TestRefCountedObjects()
    {
        // RefCounted objects don't need AutoFree() - they're automatically managed
        // Test basic RefCounted functionality
        var resource = new Resource();
        AssertThat(resource).IsNotNull();
        resource.ResourceName = "TestResource";
        AssertThat(resource.ResourceName).IsEqual("TestResource");

        // Test Image properties
        var image = new Image();
        AssertThat(image).IsNotNull();
        AssertThat(image.GetWidth()).IsEqual(0); // Empty image
        AssertThat(image.GetHeight()).IsEqual(0);

        // No manual cleanup needed - RefCounted handles memory automatically
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
}

/// <summary>
///     Simple example class for testing Godot Node.
///     Demonstrates a basic Godot Node class with properties that can be tested.
/// </summary>
#pragma warning disable CS1591, SA1402, SA1600
public partial class Player : Node
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Reviewed.")]
    public Player(string name, int level, float health, bool isAlive)
    {
        Name = name;
        Level = level;
        Health = health;
        IsAlive = isAlive;
    }

    public int Level { get; set; }

    public float Health { get; set; }

    public bool IsAlive { get; set; }
}
#pragma warning restore CS1591, SA1402, SA1600
