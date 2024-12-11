namespace RSA.UI;

/// <summary>
/// Statische Klasse zur Anzeige des Hauptmenüs der RSA-Anwendung.
/// </summary>
public static class MenuDialog
{
    /// <summary>
    /// Zeigt das Hauptmenü an und verarbeitet die Benutzereingaben.
    /// </summary>
    public static void Display()
    {
        const string title = """
                             #####################
                             #                   #
                             #        RSA        #
                             #                   #
                             #####################

                             """;
        var correctInput = true;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);

            Console.WriteLine("1. Generate keys");
            Console.WriteLine("2. Encrypt");
            Console.WriteLine("3. Decrypt");
            Console.WriteLine("4. Exit");
            Console.WriteLine();

            if (!correctInput)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            Console.Write("Select an option by entering the number: ");
            var option = Console.ReadLine();

            correctInput = int.TryParse(option, out var selected);
            if (!correctInput) continue;

            switch (selected)
            {
                case 1:
                    GenerateKeysDialog.Display();
                    break;
                case 2:
                    EncryptDialog.Display();
                    break;
                case 3:
                    DecryptDialog.Display();
                    break;
                case 4:
                    Environment.Exit(0);
                    return;
            }

            Console.ReadLine();
            correctInput = true;
        }
    }
}