namespace GdUnit4.Examples.Basics.Tests.TestAttributes;

using static Assertions;

/// <summary>
///     Demonstrates parameterized testing using [TestCase] attributes with arguments.
///     This test class focuses on:
///     - [TestCase] with single parameters
///     - [TestCase] with multiple parameters
///     - Custom test names for parameterized tests
///     - Testing the same logic with different input values
///     Parameterized tests help reduce code duplication while testing various scenarios.
/// </summary>
[TestSuite]
public class ParameterizedTestAttributes
{
    /// <summary>
    ///     Tests a single parameter with multiple values.
    ///     Each [TestCase] creates a separate test run with the specified value.
    ///     Expected result: All three test cases pass with their respective values.
    /// </summary>
    /// <param name="number">Integer value to test (must be greater than 0).</param>
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(100)]
    public void TestWithSingleParameter(int number)
        => AssertThat(number).IsGreater(0);

    /// <summary>
    ///     Tests multiple parameters showing different data types.
    ///     Demonstrates how to pass various types (int, string) to test methods.
    ///     Expected result: All test cases pass with their parameter combinations.
    /// </summary>
    /// <param name="number">Integer value to test (must be >= 0).</param>
    /// <param name="description">String description that must not be empty.</param>
    [TestCase(42, "answer")]
    [TestCase(7, "lucky")]
    [TestCase(0, "zero")]
    public void TestWithMultipleParameters(int number, string description)
    {
        AssertThat(number).IsGreaterEqual(0);
        AssertThat(description).IsNotEmpty();
    }

    /// <summary>
    ///     Tests with custom names for better test result readability.
    ///     Custom TestName makes it easier to identify which test case failed.
    ///     Expected result: Test results show meaningful names instead of parameter lists.
    /// </summary>
    /// <param name="a">First number for addition.</param>
    /// <param name="b">Second number for addition.</param>
    /// <param name="expected">Expected result of a + b.</param>
    [TestCase(1, 1, 2, TestName = "Addition: 1 + 1 = 2")]
    [TestCase(5, 3, 8, TestName = "Addition: 5 + 3 = 8")]
    [TestCase(10, -2, 8, TestName = "Addition: 10 + (-2) = 8")]
    public void TestAdditionWithCustomNames(int a, int b, int expected)
        => AssertThat(a + b).IsEqual(expected);

    /// <summary>
    ///     Tests string operations with various input combinations.
    ///     Shows how parameterized tests work well for testing string manipulations.
    ///     Expected result: All string concatenation tests pass.
    /// </summary>
    /// <param name="first">First string part.</param>
    /// <param name="separator">String to place between first and second parts.</param>
    /// <param name="second">Second string part.</param>
    /// <param name="expected">Expected result after concatenation.</param>
    [TestCase("Hello", " ", "World", "Hello World")]
    [TestCase("Good", " ", "Morning", "Good Morning")]
    [TestCase("", "", "Test", "Test")]
    public void TestStringConcatenation(string first, string separator, string second, string expected)
    {
        var result = first + separator + second;
        AssertThat(result).IsEqual(expected);
    }

    /// <summary>
    ///     Tests boolean logic with different combinations.
    ///     Demonstrates testing logical operations with parameterized inputs.
    ///     Expected result: All boolean logic tests pass with their expected results.
    /// </summary>
    /// <param name="a">First boolean value for AND operation.</param>
    /// <param name="b">Second boolean value for AND operation.</param>
    /// <param name="expected">Expected result of a AND b.</param>
    [TestCase(true, true, true)] // true AND true = true
    [TestCase(true, false, false)] // true AND false = false
    [TestCase(false, true, false)] // false AND true = false
    [TestCase(false, false, false)] // false AND false = false
    public void TestBooleanAnd(bool a, bool b, bool expected)
        => AssertThat(a && b).IsEqual(expected);

    /// <summary>
    ///     Tests edge cases and boundary values using parameters.
    ///     Important for ensuring code handles extreme values correctly.
    ///     Expected result: All boundary tests pass, showing robust handling.
    /// </summary>
    /// <param name="value">Integer value to test (including extreme values).</param>
    [TestCase(int.MaxValue)]
    [TestCase(0)]
    [TestCase(1)]
    public void TestBoundaryValues(int value)
    {
        // Test that our code handles extreme values
        var result = Math.Abs(value);
        AssertThat(result).IsGreaterEqual(0);
    }
}
