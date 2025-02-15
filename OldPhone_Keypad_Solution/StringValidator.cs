namespace OldPhone_Keypad_Solution;

public class StringValidator
{
    public static void Validate(string input, ICollection<char> allowedValues)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
        }

        foreach (char c in input)
        {
            if (!allowedValues.Contains(c))
            {
                throw new ArgumentException($"Invalid character '{c}' found in input.", nameof(input));
            }
        }
    }
}