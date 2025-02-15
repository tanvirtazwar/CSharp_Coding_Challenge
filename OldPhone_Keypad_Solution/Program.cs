namespace OldPhone_Keypad_Solution;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(OldPhone.OldPhonePad(""));
        Console.WriteLine(OldPhone.OldPhonePad("33#"));
        Console.WriteLine(OldPhone.OldPhonePad("227*#"));
        Console.WriteLine(OldPhone.OldPhonePad("4433555 555666096667775553#"));
        Console.WriteLine(OldPhone.OldPhonePad("8 88777444666*664#"));
        Console.WriteLine(OldPhone.OldPhonePad("4440222666 6755533833 308443302226663 33#"));
    }
}
