using OldPhone_Keypad_Solution;

namespace OldPhone_Keypad_Tests;

/// <summary>
/// Tests for the StringValidator class.
/// </summary>
[TestFixture]
public class StringValidatorTests
{
    private HashSet<char> _allowedCharacters;

    [SetUp]
    public void Setup()
    {
        _allowedCharacters = new HashSet<char> { '1', '2', '3', 'A', 'B', 'C' };
    }

    /// <summary>
    /// If the input string is null, an ArgumentNullException should be thrown.
    /// </summary>
    [Test]
    public void NullInput_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() =>
            StringValidator.Validate(null!, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain("Input string cannot be null"));
    }

    /// <summary>
    /// If the allowed characters collection is null, an ArgumentNullException should be thrown.
    /// </summary>
    [Test]
    public void NullAllowedCharacters_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            StringValidator.Validate("ABC", null!));
    }

    /// <summary>
    /// If the allowed characters collection is empty, an ArgumentOutOfRangeException should be thrown.
    /// </summary>
    /// <param name="input"></param>
    [TestCase("1A2B3C")]
    [TestCase("  ")]
    public void EmptyAllowedCharacters_ThrowsArgumentOutOfRangeException(string input)
    {
        var emptyAllowedChars = new HashSet<char>();
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            StringValidator.Validate(input, emptyAllowedChars));

        Assert.That(ex.Message, Does.Contain("Allowed characters collection cannot be empty."));
    }

    /// <summary>
    /// If the input string is empty,  the method should not throw an exception.
    /// </summary>
    [Test]
    public void EmptyInput_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
            StringValidator.Validate(string.Empty, _allowedCharacters));
    }

    /// <summary>
    /// If the input string contains only valid characters, the method should not throw an exception.
    /// </summary>
    /// <param name="input"></param>
    [TestCase("1")]
    [TestCase("CAB")]
    [TestCase("1A2B3C")]
    public void MultipleValidCharacters_DoesNotThrow(string input)
    {
        Assert.DoesNotThrow(() =>
            StringValidator.Validate(input, _allowedCharacters));
    }

    /// <summary>
    /// If the input string contains only invalid characters, an ArgumentException should be thrown.
    /// It should throw an exception for the first instance of the invalid character.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="invalidChar"></param>
    [TestCase("X", 'X')]
    [TestCase("4", '4')]
    [TestCase("!", '!')]
    [TestCase(" ", ' ')]
    [TestCase("phrm@(&70912", 'p')]
    public void InvalidCharacter_ThrowsArgumentException(string input, char invalidChar)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate(input, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{invalidChar}' found in input."));
    }

    /// <summary>
    /// If the input string contains a mix of valid and invalid characters, an ArgumentException should be thrown.
    /// It should throw an exception for the first instance of the invalid character.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="invalidChar"></param>
    [TestCase("12ABX", 'X')]
    [TestCase("1235", '5')]
    [TestCase("CBA!ADC", '!')]
    [TestCase("B   A", ' ')]
    [TestCase("phA3rm@(B11&70912", 'p')]
    public void MixOfValiAndInvalidCharacter_ThrowsArgumentException(string input, char invalidChar)
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate(input, _allowedCharacters));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{invalidChar}' found in input."));
    }

    /// <summary>
    /// Validate method should be case-sensitive.
    /// </summary>
    [Test]
    public void CaseSensitiveValidation()
    {
        var allowedChars = new HashSet<char> { 'a', 'b', 'c' };
        var ex = Assert.Throws<ArgumentException>(() =>
            StringValidator.Validate("ABC", allowedChars));

        Assert.That(ex.Message, Does.Contain($"Invalid character '{'A'}' found in input."));
    }
}
