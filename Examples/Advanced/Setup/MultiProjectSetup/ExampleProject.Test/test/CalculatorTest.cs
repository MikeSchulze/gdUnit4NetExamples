namespace GdUnit4.Examples.Advanced.Setup.MultiProjectSetup.Test;

using static Assertions;

[TestSuite]
public class CalculatorTest
{
    [TestCase]
    public void Add()
    {
        var calculator = new Calculator();

        AssertThat(calculator.Add(1, 2)).IsEqual(3);
    }
}
