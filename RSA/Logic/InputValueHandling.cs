using System.Numerics;

namespace RSA.Logic;

/// <summary>
/// Statische Klasse zur Handhabung von Benutzereingaben für große Ganzzahlen und Primzahlen.
/// </summary>
public static class InputValueHandling
{
    /// <summary>
    /// Fordert den Benutzer auf, eine Primzahl einzugeben und überprüft die Eingabe.
    /// </summary>
    /// <param name="prompt">Die Aufforderung, die dem Benutzer angezeigt wird.</param>
    /// <returns>Eine gültige Primzahl als <see cref="BigInteger"/>.</returns>
    public static BigInteger GetPrimeNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            // Überprüft, ob die Eingabe eine Primzahl ist
            if (BigInteger.TryParse(input, out var number) && number.IsProbablePrime()) return number;

            Console.WriteLine("Invalid input. Please enter a valid prime number.");
        }
    }

    /// <summary>
    /// Fordert den Benutzer auf, eine große Ganzzahl einzugeben und überprüft die Eingabe.
    /// </summary>
    /// <param name="prompt">Die Aufforderung, die dem Benutzer angezeigt wird.</param>
    /// <returns>Eine gültige große Ganzzahl als <see cref="BigInteger"/>.</returns>
    public static BigInteger GetBigInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            // Überprüft, ob die Eingabe eine gültige große Ganzzahl ist
            if (BigInteger.TryParse(input, out var number) && number > 1) return number;

            Console.WriteLine("Invalid input. The value must be greater than 1.");
        }
    }
}