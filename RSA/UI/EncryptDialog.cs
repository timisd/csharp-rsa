using System.Text;
using RSA.Logic;

namespace RSA.UI;

public static class EncryptDialog
{
    public static void Display()
    {
        const string title = """
                             #####################
                             #                   #
                             #      ENCRYPT      #
                             #                   #
                             #####################

                             """;

        Console.Clear();
        Console.WriteLine(title);

        Console.WriteLine("Please provide us with the values: ");
        var e = InputValueHandling.GetBigInt("Enter the public exponent: ");
        var n = InputValueHandling.GetBigInt("Enter the modulus: ");

        Console.WriteLine("Enter the message to encrypt: ");
        string? message;
        do
        {
            message = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(message)) continue;
            Console.WriteLine("Message cannot be empty. Please enter a valid message.");
            Console.WriteLine();
        } while (string.IsNullOrWhiteSpace(message));

        var data = Encoding.UTF8.GetBytes(message);
        var encrypted = Encryption.Encrypt(data, e, n);
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("Encrypted message: ");
        Console.WriteLine(Convert.ToBase64String(encrypted));
        Console.WriteLine();
        Console.Write("Press any key to get back to menu...");
    }
}