using RSA.Logic;

namespace RSA.UI;

public static class GenerateKeysDialog
{
    public static void Display()
    {
        const string title = """
                             #####################
                             #                   #
                             #   GENERATE KEYS   #
                             #                   #
                             #####################

                             """;
        var correctInput = true;

        while (true)
        {
            Console.Clear();
            Console.WriteLine(title);

            Console.WriteLine("1. Generate everything for you");
            Console.WriteLine("2. Provide your own prime numbers");
            Console.WriteLine("3. Back to menu");
            Console.WriteLine();

            if (correctInput == false)
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
                    Generate();
                    break;
                case 2:
                    Provide();
                    break;
                case 3:
                    MenuDialog.Display();
                    return;
            }

            break;
        }
    }

    private static void Generate()
    {
        var keys = Keys.Generate();

        DisplayKeys(keys);
    }

    private static void Provide()
    {
        const string title = """
                             ######################
                             #                    #
                             #    PROVIDE KEYS    #
                             #                    #
                             ######################

                             """;
        Console.WriteLine(title);
        Console.WriteLine();

        var p = InputValueHandling.GetPrimeNumber("Enter prime number P: ");
        var q = InputValueHandling.GetPrimeNumber("Enter prime number Q: ");
        var e = InputValueHandling.GetBigInt("Enter prime number E: ");

        var keys = Keys.Generate(p, q, e);
        DisplayKeys(keys);
    }

    private static void DisplayKeys(Keys keys)
    {
        const string title = """
                             ######################
                             #                    #
                             #   GENERATED KEYS   #
                             #                    #
                             ######################

                             """;

        Console.Clear();
        Console.WriteLine(title);

        DisplayResult(keys);
    }

    private static void DisplayResult(Keys keys)
    {
        Console.WriteLine($"P:      {keys.P}");
        Console.WriteLine($"Q:      {keys.Q}");
        Console.WriteLine($"N:      {keys.N}");
        Console.WriteLine($"E:      {keys.E}");
        Console.WriteLine($"D:      {keys.D}");
        Console.WriteLine($"Phi:    {keys.Phi}");
        Console.Write("Press any key to get back to menu...");
    }
}