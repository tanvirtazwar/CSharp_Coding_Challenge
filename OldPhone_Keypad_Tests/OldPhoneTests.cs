using OldPhone_Keypad_Solution;

namespace OldPhone_Keypad_Tests;

[TestFixture]
public class OldPhoneTests
{
    [TestCase("ABC#")]
    [TestCase("123ab^%#")]
    public void InvalidInput_ThrowsArgumentException(string input)
    {
        Assert.Throws<ArgumentException>(() => OldPhone.OldPhonePad(input));
    }

    [Test]
    public void NullInput_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => OldPhone.OldPhonePad(null!));
    }

    [Test]
    public void EmptyInput_ReturnsEmptyString()
    {
        Assert.That(OldPhone.OldPhonePad(""), Is.EqualTo(string.Empty));
    }

    [Test]
    public void NoSendCharacter_ReturnsEmptyString()
    {
        Assert.That(OldPhone.OldPhonePad("222"), Is.EqualTo(string.Empty));
    }

    [TestCase("##")]
    [TestCase("######")]
    public void OnlySendCharacter_ReturnsEmptyString(string input)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(string.Empty));
    }

    [TestCase("*#")]
    [TestCase("****#")]
    public void OnlyBackspaceCharacter_ReturnsEmptyString(string input)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(string.Empty));
    }

    [TestCase("33#", "E")]
    [TestCase("227*#", "B")]
    [TestCase("4433555 555666096667775553#", "HELLO WORLD")]
    [TestCase("8 88777444666*664#", "TURING")]
    [TestCase("4440222666 6755533833 308443302226663 33#", "I COMPLETED THE CODE")]
    public void GivenExamples_ReturnExpectedResults(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    [TestCase("222#", "C")]
    [TestCase("2222#", "A")]
    [TestCase("22222#", "B")]
    public void RepeatedButton_CyclesThroughLetters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    [TestCase("2 2#", "AA")]
    [TestCase("22 2#", "BA")]
    [TestCase("222 2#", "CA")]
    public void SpaceSeparatedInputs_HandledCorrectly(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    [Test]
    public void BackspaceAfterCharacter_DeletesLastCharacter()
    {
        Assert.That(OldPhone.OldPhonePad("222 2*#"), Is.EqualTo("C"));
    }

    [TestCase("222 2***#", "")]
    [TestCase("8 88777444666***664***#", "T")]
    public void MultipleBackspaces_DeleteMultipleCharacters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    [TestCase("777700000000#", "S ")]
    [TestCase("777700000000 0077#", "S  Q")]
    [TestCase("0#", " ")]
    public void MultipleZeroButton_ReturnsSingleSpace(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }

    [TestCase("1#", "&")]
    [TestCase("11#", "'")]
    [TestCase("111#", "(")]
    public void Button1_CyclesThroughSpecialCharacters(string input, string expected)
    {
        Assert.That(OldPhone.OldPhonePad(input), Is.EqualTo(expected));
    }
}
