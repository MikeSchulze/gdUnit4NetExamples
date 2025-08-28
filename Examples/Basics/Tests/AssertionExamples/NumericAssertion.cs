namespace GdUnit4.Examples.Basics.Tests.AssertionExamples;

using static Assertions;

/// <summary>
///     Basic examples demonstrating numeric assertions in gdUnit4Net.
///     This test class shows common numeric testing patterns including:
///     - Equality comparisons for integers, floats, and doubles
///     - Range checking (greater than, less than, between)
///     - Approximate equality for floating-point numbers
///     - Fluent assertion chaining for multiple numeric checks
///     Perfect for beginners learning how to test numeric values and calculations.
/// </summary>
[TestSuite]
public class NumericAssertion
{
    /// <summary>
    ///     Tests exact integer equality - both numbers must match exactly.
    ///     This is the most basic numeric assertion for testing identical integer values.
    ///     Expected result: The test should pass since both integers are identical.
    /// </summary>
    [TestCase]
    public void IntegerIsEqual()
        => AssertThat(42)
            .IsEqual(42);

    /// <summary>
    ///     Tests exact floating-point equality - useful when exact precision is required.
    ///     Note: Be careful with float equality due to precision issues in calculations.
    ///     Expected result: The test should pass since both floats are identical.
    /// </summary>
    [TestCase]
    public void FloatIsEqual()
        => AssertThat(3.14f)
            .IsEqual(3.14f);

    /// <summary>
    ///     Tests approximate equality for floating-point numbers with a tolerance.
    ///     This is preferred over exact equality for float/double calculations.
    ///     Expected result: The test passes because the values are within 0.01 tolerance.
    /// </summary>
    [TestCase]
    public void FloatIsEqualWithTolerance()
        => AssertThat(3.14159)
            .IsEqualApprox(3.14, 0.01);

    /// <summary>
    ///     Tests that a number is greater than another value.
    ///     Useful for validating minimum thresholds or positive results.
    ///     Expected result: The test passes because 10 is greater than 5.
    /// </summary>
    [TestCase]
    public void IsGreaterThan()
        => AssertThat(10)
            .IsGreater(5);

    /// <summary>
    ///     Tests that a number is less than another value.
    ///     Useful for validating maximum limits or checking bounds.
    ///     Expected result: The test passes because 5 is less than 10.
    /// </summary>
    [TestCase]
    public void IsLessThan()
        => AssertThat(5)
            .IsLess(10);

    /// <summary>
    ///     Tests that a number falls within a specific range (inclusive).
    ///     Useful for validating that values are within acceptable bounds.
    ///     Expected result: The test passes because 7 is between 5 and 10 (inclusive).
    /// </summary>
    [TestCase]
    public void IsBetween()
        => AssertThat(7)
            .IsBetween(5, 10);

    /// <summary>
    ///     Demonstrates different ways to test multiple conditions on the same number.
    ///     ❌ BAD STYLE - Separate assertions (repeating the same value):
    ///     AssertThat(42).IsGreaterThan(40);
    ///     AssertThat(42).IsLessThan(50);
    ///     ✅ GOOD STYLE - Fluent chaining (preferred approach):
    ///     AssertThat(42).IsGreaterThan(40).IsLessThan(50);
    ///     Always prefer fluent chaining when testing multiple conditions on the same value.
    ///     It's more readable, efficient, and follows gdUnit4Net best practices.
    /// </summary>
    [TestCase]
    public void NumericRangeGoodVsBadStyle()
    {
        // ❌ BAD: Multiple separate assertions on the same number
        // This is repetitive and less efficient
        AssertThat(42).IsGreater(40);
        AssertThat(42).IsLess(50);

        // ✅ GOOD: Use fluent chaining for multiple checks on the same value
        // This is the preferred gdUnit4Net style
        AssertThat(42)
            .IsGreater(40)
            .IsLess(50);
    }

    /// <summary>
    ///     Demonstrates complex numeric validation using fluent chaining.
    ///     Tests multiple conditions: positive number within a specific range with tolerance.
    ///     Expected result: All assertions pass because 15.5 meets all the specified conditions.
    /// </summary>
    [TestCase]
    public void ComplexNumericValidation()
        => AssertThat(15.5)
            .IsGreater(0) // Must be positive
            .IsLess(20) // Must be under 20
            .IsEqualApprox(15.5, 0.1); // Must be approximately 15.5
}
