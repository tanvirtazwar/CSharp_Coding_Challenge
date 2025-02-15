using OldPhone_Keypad_Solution;

namespace OldPhone_Keypad_Tests;

[TestFixture]
public class StringValidatorTests
{
    private HashSet<char> _allowedCharacters;

    [SetUp]
    public void Setup()
    {
        _allowedCharacters = new HashSet<char> { '1', '2', '3', 'A', 'B', 'C' };
    }

    [Test]
    public void Validate_NullInput_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            StringValidator.Validate(null!, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain("Input string cannot be null"));
    }

    [Test]
    public void Validate_NullAllowedCharacters_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            StringValidator.Validate("ABC", null!));
    }

    [TestCase("1A2B3C")]
    [TestCase("  ")]
    public void Validate_EmptyAllowedCharacters_ThrowsArgumentOutOfRangeException(string input)
    {
        var emptyAllowedChars = new HashSet<char>();
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            StringValidator.Validate(input, emptyAllowedChars));

        Assert.That(ex.Message, Does.Contain("Allowed characters collection cannot be empty."));
    }

    [Test]
    public void Validate_EmptyInput_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
            StringValidator.Validate(string.Empty, _allowedCharacters));
    }

    [TestCase("1")]
    [TestCase("CAB")]
    [TestCase("1A2B3C")]
    public void Validate_MultipleValidCharacters_DoesNotThrow(string input)
    {
        Assert.DoesNotThrow(() =>
            StringValidator.Validate(input, _allowedCharacters));
    }

    [TestCase("X", 'X')]
    [TestCase("4", '4')]
    [TestCase("!", '!')]
    [TestCase(" ", ' ')]
    [TestCase("phrm@(&70912", 'p')]
    public void Validate_InvalidCharacter_ThrowsArgumentException(string input, char invalidChar)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate(input, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{invalidChar}' found in input."));
    }

    [TestCase("12ABX", 'X')]
    [TestCase("1235", '5')]
    [TestCase("CBA!ADC", '!')]
    [TestCase("B   A", ' ')]
    [TestCase("phA3rm@(B11&70912", 'p')]
    public void Validate_MixOfValiAndInvalidCharacter_ThrowsArgumentException(string input, char invalidChar)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate(input, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{invalidChar}' found in input."));
    }

    [Test]
    public void Validate_CaseSensitiveValidation()
    {
        var allowedChars = new HashSet<char> { 'a', 'b', 'c' };
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate("ABC", allowedChars));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{'A'}' found in input."));
    }
}
