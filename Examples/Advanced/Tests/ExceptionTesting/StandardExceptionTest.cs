namespace GdUnit4.Examples.Advanced.Tests.ExceptionTesting;

/// <summary>
///     Demonstrates exception testing patterns in GdUnit4Net.
///     This test suite shows how to test expected exceptions including:
///     • Testing for specific exception types with ThrowsException attribute
///     • Validating exception messages for accuracy
///     • Testing common runtime exceptions (ArgumentException, NullReferenceException)
///     • Line number validation for precise error location tracking
///     Exception testing ensures your code fails gracefully and provides meaningful error messages.
/// </summary>
[TestSuite]
public class StandardExceptionTest
{
    /// <summary>
    ///     Tests argument validation exception handling.
    ///     Validates that methods properly validate input parameters and throw ArgumentException
    ///     with descriptive messages when invalid arguments are provided.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(ArgumentException), "Invalid parameter: value cannot be negative")]
    public void TestArgumentException() =>
        ValidatePositiveNumber(-5); // Simulate a method that validates input parameters

    /// <summary>
    ///     Tests null reference exception handling.
    ///     Validates that operations on null references throw NullReferenceException as expected.
    ///     This pattern helps test null safety and defensive programming practices.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(NullReferenceException))]
    public void TestNullReferenceException()
    {
        string? nullValue = null;

        // This operation should throw NullReferenceException
        nullValue!.Contains("test", StringComparison.InvariantCulture);
    }

    /// <summary>
    ///     Tests division by zero exception handling.
    ///     Validates that mathematical operations properly handle edge cases and throw
    ///     appropriate exceptions for invalid operations.
    /// </summary>
    [TestCase]
    [ThrowsException(typeof(DivideByZeroException), "Attempted to divide by zero.", 73)]
    public void TestDivisionByZero()
        => PerformDivision(10, 0);

    #region Helper Methods for Exception Testing

    /// <summary>
    ///     Helper method that validates positive numbers and throws ArgumentException for negative values.
    /// </summary>
    /// <param name="value">Value to validate.</param>
    /// <exception cref="ArgumentException">Thrown when value is negative.</exception>
    private static void ValidatePositiveNumber(int value)
    {
        if (value < 0)
            throw new ArgumentException("Invalid parameter: value cannot be negative");
    }

    /// <summary>
    ///     Helper method that performs division and throws DivideByZeroException for zero divisor.
    /// </summary>
    /// <param name="dividend">Number to be divided.</param>
    /// <param name="divisor">Number to divide by.</param>
    /// <returns>Division result.</returns>
    /// <exception cref="DivideByZeroException">Thrown when divisor is zero.</exception>
    private static double PerformDivision(double dividend, double divisor)
    {
        if (Math.Abs(divisor) < double.Epsilon)
            throw new DivideByZeroException("Attempted to divide by zero.");

        return dividend / divisor;
    }

    #endregion
}
