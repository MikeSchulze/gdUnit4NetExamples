namespace GdUnit4.Examples.Basics.Tests.TestAttributes;

using static Assertions;

/// <summary>
///     Demonstrates the [IgnoreUntil] attribute for controlling test execution.
///     This test class focuses on:
///     - [IgnoreUntil] for temporarily skipping tests
///     - When and why to ignore tests
///     - Using [IgnoreUntil] with and without descriptions
///     - Combining [IgnoreUntil] with other attributes
///     The [IgnoreUntil] attribute helps manage tests during development workflows.
/// </summary>
[TestSuite]
public class TestControlAttributes
{
    /// <summary>
    ///     A normal test that runs as expected.
    ///     Included to show contrast with ignored tests below.
    ///     Expected result: Test runs and passes normally.
    /// </summary>
    [TestCase]
    public void NormalTest()
        => AssertThat("This test runs normally").IsNotEmpty();

    /// <summary>
    ///     Test temporarily ignored using [IgnoreUntil] attribute without description.
    ///     Useful when a test is failing due to known issues or incomplete features.
    ///     Expected result: Test is skipped and shows as ignored in test results.
    ///     This test would normally fail (false should not be true), but it's ignored
    ///     so the failure doesn't affect the test run.
    /// </summary>
    [TestCase]
    [IgnoreUntil]
    public void TemporarilyIgnoredTest() =>

        // This test will be skipped - useful for:
        // - Tests that depend on features not yet implemented
        // - Tests that are temporarily failing due to known bugs
        // - Tests that need to be fixed later
        AssertThat(false).IsTrue(); // This would fail, but test is ignored

    /// <summary>
    ///     Test temporarily ignored using [IgnoreUntil] attribute with descriptive reason.
    ///     The Description property explains why the test is being ignored, which helps
    ///     team members understand the context and when to re-enable it.
    ///     Expected result: Test is skipped and shows the description in test results.
    /// </summary>
    [TestCase]
    [IgnoreUntil(Description = "This test is ignored until the new API is implemented")]
    public void TemporarilyIgnoredTestWithDescription() =>
        AssertThat(false).IsTrue(); // This would fail, but test is ignored
}
