using System.Numerics;

namespace RSA.Logic;

public static class InputValueHandling
{
    public static BigInteger GetPrimeNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (BigInteger.TryParse(input, out var number) && number.IsProbablePrime()) return number;

            Console.WriteLine("Invalid input. Please enter a valid prime number.");
        }
    }

    public static BigInteger GetBigInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (BigInteger.TryParse(input, out var number) && number > 1) return number;

            Console.WriteLine("Invalid input. The value must be greater than 1.");
        }
    }
}