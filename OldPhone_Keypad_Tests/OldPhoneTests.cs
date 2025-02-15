using OldPhone_Keypad_Solution;

namespace OldPhone_Keypad_Tests;

/// <summary>
/// Tests for the OldPhone class.
/// </summary>
[TestFixture]
public class OldPhoneTests
{
    /// <summary>
    /// If the input string contains invalid characters, an ArgumentException should be thrown.
    /// </summary>
    /// <param name="input"></param>
    [TestCase("ABC#")]
    [TestCase("123ab^%#")]
    public void InvalidInput_ThrowsArgumentException(string input)
    {
        Assert.Throws<ArgumentException>(() => OldPhone.OldPhonePad(input));
    }

    /// <summary>
    /// If the input string is null, an ArgumentNullException should be thrown.
    /// </summary>
    [Test]
    public void NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => OldPhone.OldPhonePad(null!));
    }

    /// <summary>
    /// If the input string is empty, an empty string should be returned.
    /// </summary>
    [Test]
    public void EmptyInput_ReturnsEmptyString()
    {
        Assert.That(OldPhone.OldPhonePad(""), Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// If the input string does not end with the send character, an empty string should be returned.
    /// </summary>
    [Test]
    public void NoSendCharacter_ReturnsEmptyString()
    {
        Assert.That(OldPhone.OldPhonePad("222"), Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// If the input string only contains the send character, an empty string should be returned.
    /// </summary>
    /// <param name="input"></param>
    [TestCase("##")]
    [TestCase("######")]
    public void OnlySendCharacter_ReturnsEmptyString(string input)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// If the input string only contains the backspace character, an empty string should be returned.
    /// </summary>
    /// <param name="input"></param>
    [TestCase("*#")]
    [TestCase("****#")]
    public void OnlyBackspaceCharacter_ReturnsEmptyString(string input)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(string.Empty));
    }

    /// <summary>
    /// For valid input, the method should return the expected result.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("33#", "E")]
    [TestCase("227*#", "B")]
    [TestCase("4433555 555666096667775553#", "HELLO WORLD")]
    [TestCase("8 88777444666*664#", "TURING")]
    [TestCase("4440222666 6755533833 308443302226663 33#", "I COMPLETED THE CODE")]
    public void GivenExamples_ReturnExpectedResults(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    /// <summary>
    /// If the same character is pressed multiple times, the method should cycle through the available characters.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("222#", "C")]
    [TestCase("2222#", "A")]
    [TestCase("22222#", "B")]
    public void RepeatedButton_CyclesThroughLetters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    /// <summary>
    /// If the input string contains between same characters, the method  should treat them as separate characters.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("2 2#", "AA")]
    [TestCase("22 2#", "BA")]
    [TestCase("222 2#", "CA")]
    public void SpaceSeparatedInputs_HandledCorrectly(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    /// <summary>
    /// If the backspace character is pressed, the last character should be deleted.
    /// </summary>
    [Test]
    public void BackspaceAfterCharacter_DeletesLastCharacter()
    {
        Assert.That(OldPhone.OldPhonePad("222 2*#"), Is.EqualTo("C"));
    }
    /// <summary>
    /// For multiple backspaces method  should delete multiple characters
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("222 2***#", "")]
    [TestCase("8 88777444666***664***#", "T")]
    public void MultipleBackspaces_DeleteMultipleCharacters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    /// <summary>
    /// If multiple zero is pressed without pause the method should consider them  as single space
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("777700000000#", "S ")]
    [TestCase("777700000000 0077#", "S  Q")]
    [TestCase("0#", " ")]
    public void MultipleZeroButton_ReturnsSingleSpace(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    /// <summary>
    /// If the input string contains repeated 1s, the method should cycle through the special characters.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="expected"></param>
    [TestCase("1#", "&")]
    [TestCase("11#", "'")]
    [TestCase("111#", "(")]
    public void Button1_CyclesThroughSpecialCharacters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }
}
