namespace OldPhone_Keypad_Solution;

/// <summary>
///  Emulates  old phone keypad
/// </summary>
public class OldPhone
{
    private const char _space = ' ';
    private const char _send = '#';
    private const char _backspace = '*';
    private static readonly HashSet<char> _allowedCharacters = 
        ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', _backspace, _send, _space];

    private static readonly Dictionary<char, string> _keyPad = new()
    {
        {'1', "&'(" },
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"},
        {'0', " "  }
    };

    /// <summary>
    /// This method takes the user
    /// Calls the validator
    /// After validation it calls the parser
    /// </summary>
    /// <param name="input"></param>
    ///  <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="ArgumentException"></exception>
    /// <returns>For valid input it returns the parsed string</returns>
    public static string OldPhonePad(string input)
    {
        StringValidator.Validate(input, _allowedCharacters);

        if (input.Length == 0 || input[^1] != _send)
        {
            return string.Empty;
        }
        var result = ParseMessage(input);
        return result;
    }

    /// <summary>
    /// This method parse the valid users input
    /// It handles all the cases when string is not empty
    /// </summary>
    /// <param name="input"></param>
    /// <returns>For valid input it returns the parsed string</returns>
    private static string ParseMessage(string input)
    {
        var resultStack = new Stack<char>();
        var currentValue = input[0];
        var count = 0;

        foreach (char c in input)
        {
            if (c == _send)
            {
                if (count > 0)
                {
                    var val = _keyPad[currentValue];
                    var i = (count - 1) % val.Length;
                    resultStack.Push(val[i]);
                }
                break;
            }
            else if (c == _backspace)
            {
                if (count > 0)
                {
                    currentValue = _space;
                    count = 0;
                }
                else if (resultStack.Count > 0)
                {
                    resultStack.Pop();
                }
            }
            else if (c != currentValue)
            {
                if (count > 0)
                {
                    var val = _keyPad[currentValue];
                    var i = (count - 1) % val.Length;
                    resultStack.Push(val[i]);
                }
                currentValue = c;
                count = c == _space ? 0 : 1;
            }
            else
            {
                count = c == _space ? 0 : count + 1;
            }
        }

        var reault = string.Empty;
        while (resultStack.Count > 0)
        {
            reault = resultStack.Pop() + reault;
        }
        return reault;
    }
}
