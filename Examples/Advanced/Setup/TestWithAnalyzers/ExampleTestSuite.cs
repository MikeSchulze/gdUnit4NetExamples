namespace GdUnit4.Examples.Advanced.Setup.TestWithAnalyzers;

using Godot;

using static Assertions;

/// <summary>
///     Demonstrates the power of gdUnit4.analyzers for catching common testing mistakes at compile time.
///     This test class shows:
///     - How gdUnit4.analyzers detect missing [RequireGodotRuntime] attributes
///     - Compile-time warnings and errors for improper Godot object usage
///     - The difference between properly configured and misconfigured tests
///     - Why static analysis improves test reliability and developer productivity
///     Key learning: The gdUnit4.analyzers package helps prevent runtime errors by catching
///     mistakes during compilation, making your test suite more reliable and easier to maintain.
///     Note: Make sure to include the gdUnit4.analyzers NuGet package in your test project
///     to get these compile-time checks.
/// </summary>
[TestSuite]
public class ExampleTestSuite
{
    /// <summary>
    ///     ✅ CORRECT: Test using Godot types with proper [RequireGodotRuntime] attribute.
    ///     This test correctly uses the [RequireGodotRuntime] attribute when testing Godot objects.
    ///     The analyzer recognizes this pattern and will not generate any warnings or errors.
    ///     What the analyzer checks:
    ///     - [RequireGodotRuntime] is present when using Godot types
    ///     - AutoFree() is used properly with Godot Node objects
    ///     - Test method signature is correct
    ///     Expected result: Test compiles cleanly and runs successfully.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public void IsNodeNotNull()
    {
        var node = AutoFree(new Node());
        AssertThat(node).IsNotNull();
    }

    /// <summary>
    ///     ❌ ANALYZER ERROR: Test using Godot types WITHOUT [RequireGodotRuntime] attribute.
    ///     This test demonstrates what happens when you forget the [RequireGodotRuntime] attribute
    ///     while using Godot types. The gdUnit4.analyzers will detect this mistake and generate:
    ///     COMPILE-TIME ERROR/WARNING:
    ///     "Test method uses Godot types but is missing [RequireGodotRuntime] attribute"
    ///     Why this is caught:
    ///     - The analyzer detects usage of Godot.Node type
    ///     - It notices the missing [RequireGodotRuntime] attribute
    ///     - It generates a diagnostic to help you fix the issue
    ///     Without the analyzer: This test would compile but fail at runtime with confusing errors
    ///     about missing Godot runtime initialization.
    ///     With the analyzer: You get a clear compile-time message telling you exactly what's wrong
    ///     and how to fix it.
    ///     ----
    ///     ⚠️ DEMO NOTE: This example project has GdUnit0501 configured as WARNING in the .csproj
    ///     to allow compilation for demonstration purposes. In real projects, this should be an ERROR
    ///     to prevent runtime failures. Do NOT configure GdUnit0501 as warning in production code!
    ///     Expected result: Compile-time error/warning prevents runtime surprises.
    /// </summary>
    [TestCase]

    // ❌ Missing [RequireGodotRuntime] - analyzer will flag this!
    [IgnoreUntil(Description = "We skip this test because it is not configured to run in a Godot environment.")]
    public void IsNodeNotNullInvalidTest()
    {
        var node = AutoFree(new Node());
        AssertThat(node).IsNotNull();
    }

    /// <summary>
    ///     ✅ CORRECT: Test using only C# types doesn't need [RequireGodotRuntime].
    ///     This test uses standard C# types and doesn't require Godot runtime.
    ///     The analyzer will not flag this as an error because no Godot types are used.
    ///     What the analyzer checks:
    ///     - No Godot types detected → no [RequireGodotRuntime] needed
    ///     - Standard C# objects don't need AutoFree()
    ///     - Test is properly configured for pure .NET testing
    ///     Expected result: Test compiles cleanly and runs without Godot runtime.
    /// </summary>
    [TestCase]
    public void TestPureCSharpObject()
    {
        var player = new
        {
            Name = "Hero",
            Level = 10
        };
        AssertThat(player).IsNotNull();
    }
}
