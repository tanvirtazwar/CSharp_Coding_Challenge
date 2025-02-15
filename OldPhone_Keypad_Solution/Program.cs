using System.Text;

namespace OldPhone_Keypad_Solution;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(OldPhonePad("33#"));
        Console.WriteLine(OldPhonePad("227*#"));
        Console.WriteLine(OldPhonePad("4433555 555666#"));
        Console.WriteLine(OldPhonePad("8 88777444666*664#"));
        Console.WriteLine(OldPhonePad("***********#"));
        Console.WriteLine(OldPhonePad("##########"));
        Console.WriteLine(OldPhonePad("00000000#"));
    }

    private static Dictionary<char, string> _keyPad = new()
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

    public static string OldPhonePad(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input can not be null or empty");
        }

        var resultStack = new Stack<char>();
        char currentValue = input[0];
        int count = 0;

        foreach (char c in input)
        {
            if (c == '#')
            {
                if (count > 0)
                {
                    var val = _keyPad[currentValue];
                    var i = (count - 1) % val.Length;
                    resultStack.Push(val[i]);
                }
                break ;
            }
            if (c == '*')
            {
                if (count > 0)
                {
                    currentValue = ' ';
                    count = 0;
                }
                else if (resultStack.Count > 0)
                {
                    resultStack.Pop();
                }
                continue;
            }
            if (c != currentValue)
            {
                if (count > 0)
                {
                    var val = _keyPad[currentValue];
                    var i = (count - 1) % val.Length;
                    resultStack.Push(val[i]);
                }
                currentValue = c;
                count = c == ' ' ? 0 : 1;
                continue;
            }
            count = c == ' ' ? 0 : count + 1;
        }

        var reault = string.Empty;
        while (resultStack.Count > 0)
        {
            reault = resultStack.Pop() + reault;
        }
        return reault;
    }
}
