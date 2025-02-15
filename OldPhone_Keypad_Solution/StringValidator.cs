namespace OldPhone_Keypad_Solution;

/// <summary>
/// Validates the input string against the allowed characters.
/// </summary>
public class StringValidator
{
    /// <summary>
    /// Validates the input string against the allowed characters.          
    /// </summary>
    /// <param name="input"></param>
    /// <param name="allowedCharacters"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
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