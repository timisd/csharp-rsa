using System.Text;
using RSA.Logic;

namespace RSA.UI;

/// <summary>
/// Statische Klasse zur Anzeige des Entschlüsselungsdialogs.
/// </summary>
public static class DecryptDialog
{
    /// <summary>
    /// Zeigt den Entschlüsselungsdialog an und verarbeitet die Benutzereingaben.
    /// </summary>
    public static void Display()
    {
        const string title = """
                             #####################
                             #                   #
                             #      DECRYPT      #
                             #                   #
                             #####################

                             """;

        Console.Clear();
        Console.WriteLine(title);

        Console.WriteLine("Please provide us with the values: ");
        var d = InputValueHandling.GetBigInt("Enter the private exponent: ");
        var n = InputValueHandling.GetBigInt("Enter the modulus: ");

        Console.WriteLine("Enter the encrypted message: ");
        var message = Console.ReadLine();
        var bytes = Convert.FromBase64String(message!);

        // Entschlüsselt die Nachricht mit den angegebenen Werten
        var encrypted = Encryption.Decrypt(bytes, d, n);
        Console.Clear();
        Console.WriteLine(title);
        Console.WriteLine("Encrypted message: ");
        Console.WriteLine(Encoding.UTF8.GetString(encrypted));
        Console.WriteLine();
        Console.Write("Press any key to get back to menu...");
    }
}