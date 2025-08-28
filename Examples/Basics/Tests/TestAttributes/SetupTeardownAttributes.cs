namespace GdUnit4.Examples.Basics.Tests.TestAttributes;

using static Assertions;

/// <summary>
///     Demonstrates test lifecycle attributes for setup and cleanup operations.
///     This test class focuses on:
///     - [Before] for one-time test suite setup
///     - [After] for one-time test suite cleanup
///     - [BeforeTest] for setup before each test
///     - [AfterTest] for cleanup after each test
///     Understanding test lifecycle is crucial for managing test state and resources.
///     The execution order is: [Before] → [BeforeTest] → [TestCase] → [AfterTest] → repeat → [After].
/// </summary>
[TestSuite]
public class SetupTeardownAttributes
{
    private string? suiteData;
    private int testCounter;
    private string? testData;

    /// <summary>
    ///     Runs ONCE before ALL tests in this test suite start.
    ///     Use for expensive setup that can be shared across all tests.
    ///     Examples: Loading configuration, initializing databases, creating test files.
    ///     This method initializes suite-wide data and resets the test counter.
    /// </summary>
    [Before]
    public void SetupTestSuite()
    {
        suiteData = "Suite-wide data initialized";
        testCounter = 0;
        Console.WriteLine("🏁 Test suite setup completed");
    }

    /// <summary>
    ///     Runs ONCE after ALL tests in this test suite have finished.
    ///     Use for cleanup that should happen only once after all tests.
    ///     Examples: Closing connections, deleting test files, releasing resources.
    ///     This method displays the total test count and cleans up suite data.
    /// </summary>
    [After]
    public void CleanupTestSuite()
    {
        Console.WriteLine($"🏁 Test suite cleanup - Ran {testCounter} tests total");
        suiteData = null;
    }

    /// <summary>
    ///     Runs before EACH individual test method.
    ///     Use for setup that needs fresh state for every test.
    ///     Examples: Creating new objects, resetting variables, preparing test data.
    ///     This method creates fresh test data and increments the test counter.
    /// </summary>
    [BeforeTest]
    public void SetupEachTest()
    {
        testData = $"Fresh data for test #{++testCounter}";
        Console.WriteLine($"🔧 Setting up test #{testCounter}");
    }

    /// <summary>
    ///     Runs after EACH individual test method.
    ///     Use for cleanup after every test to prevent test pollution.
    ///     Examples: Disposing objects, clearing caches, resetting global state.
    ///     This method cleans up the test-specific data after each test.
    /// </summary>
    [AfterTest]
    public void CleanupEachTest()
    {
        Console.WriteLine($"🧹 Cleaning up test #{testCounter}");
        testData = null;
    }

    /// <summary>
    ///     First test demonstrating that suite setup ran before test setup.
    ///     Verifies that both [Before] and [BeforeTest] methods executed properly.
    ///     Expected result: Both suite data and test data are properly initialized.
    /// </summary>
    [TestCase]
    public void FirstTest()
    {
        // Verify suite setup ran
        AssertThat(suiteData).IsEqual("Suite-wide data initialized");

        // Verify test setup ran
        AssertThat(testData).IsEqual("Fresh data for test #1");
        AssertThat(testCounter).IsEqual(1);
    }

    /// <summary>
    ///     Second test demonstrating that test setup runs fresh for each test.
    ///     Shows that [BeforeTest] creates new state while [Before] data persists.
    ///     Expected result: Suite data persists, but test data is fresh for this test.
    /// </summary>
    [TestCase]
    public void SecondTest()
    {
        // Suite data should persist from [Before] method
        AssertThat(suiteData).IsEqual("Suite-wide data initialized");

        // Test data should be fresh from [BeforeTest] method
        AssertThat(testData).IsEqual("Fresh data for test #2");
        AssertThat(testCounter).IsEqual(2);
    }

    /// <summary>
    ///     Third test confirming consistent lifecycle behavior.
    ///     Demonstrates that the lifecycle pattern works reliably across multiple tests.
    ///     Expected result: Lifecycle methods continue to work as expected.
    /// </summary>
    [TestCase]
    public void ThirdTest()
    {
        // Consistent behavior across all tests
        AssertThat(suiteData).IsNotNull();
        AssertThat(testData).IsEqual("Fresh data for test #3");
        AssertThat(testCounter).IsEqual(3);
    }
}
