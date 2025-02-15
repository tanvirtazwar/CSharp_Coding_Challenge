namespace OldPhone_Keypad_Solution;

public class StringValidator
{
    public static void Validate(string input, ICollection<char> allowedCharacters)
    {
        if (input == null)
        {
            throw new ArgumentNullException("Input string cannot be null.");
        }

        if (allowedCharacters == null)
        {
            throw new ArgumentNullException("Allowed characters cannot be null.");
        }

        if (allowedCharacters.Count == 0)
        {
            throw new ArgumentOutOfRangeException("Allowed characters collection cannot be empty.");
        }

        foreach (char c in input)
        {
            if (!allowedCharacters.Contains(c))
            {
                throw new ArgumentException($"Invalid character '{c}' found in input.");
            }
        }
    }
}