namespace GdUnit4.Examples.Basics.Tests.TestAttributes;

using static Assertions;

/// <summary>
///     Demonstrates the most fundamental gdUnit4Net attributes for basic test organization.
///     This test class focuses on:
///     - [TestSuite] for marking test classes
///     - [TestCase] for individual test methods
///     - Basic test descriptions and timeouts
///     - Custom test names with TestName property
///     - Simple parameterized tests
///     These are the essential attributes every gdUnit4Net developer should know.
/// </summary>
[TestSuite]
public class BasicTestAttributes
{
    /// <summary>
    ///     The simplest possible test using just the [TestCase] attribute.
    ///     This is the most basic way to mark a method as a test.
    ///     Expected result: Test passes with a simple assertion.
    /// </summary>
    [TestCase]
    public void SimpleTestCase()
        => AssertThat(2 + 2).IsEqual(4);

    /// <summary>
    ///     Test case with a description that appears in test runners and reports.
    ///     Descriptions help document what the test is checking without reading code.
    ///     Expected result: Test passes and shows the custom description.
    /// </summary>
    [TestCase(Description = "Verifies that string concatenation works correctly")]
    public void TestCaseWithDescription()
        => AssertThat("Hello" + " " + "World").IsEqual("Hello World");

    /// <summary>
    ///     Test case with a timeout to prevent infinite loops or hanging tests.
    ///     The timeout is specified in milliseconds (1000ms = 1 second).
    ///     Expected result: Test completes within timeout and passes.
    /// </summary>
    [TestCase(Timeout = 1000)]
    public void TestCaseWithTimeout()
    {
        // Simulate some work that should complete quickly
        Thread.Sleep(50);
        AssertThat(true).IsTrue();
    }

    /// <summary>
    ///     Test case with a custom name using the TestName property.
    ///     Custom test names make test results more readable and meaningful.
    ///     The TestName overrides the method name in test runner displays.
    ///     Expected result: Test appears as "MathTest" instead of "SomeTestWithSomething".
    /// </summary>
    [TestCase(TestName = "MathTest")]
    public void SomeTestWithSomething()
        => AssertThat(10 * 5).IsEqual(50);

    /// <summary>
    ///     Simple parameterized test case demonstrating basic parameter passing.
    ///     Shows how to pass arguments to test methods for data-driven testing.
    /// </summary>
    /// <param name="x">First integer parameter for multiplication.</param>
    /// <param name="y">Second integer parameter for multiplication.</param>
    [TestCase(10, 5)]
    public void TestCaseWithParameters(int x, int y)
        => AssertThat(x * y).IsEqual(50);
}
