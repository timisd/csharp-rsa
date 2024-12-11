using System.Numerics;
using System.Security.Cryptography;

namespace RSA.Logic;

/// <summary>
/// Hilfsklasse für Operationen mit großen Ganzzahlen.
/// </summary>
public static class BigIntHelper
{
    /// <summary>
    /// Überprüft, ob eine Zahl wahrscheinlich eine Primzahl ist.
    /// </summary>
    /// <param name="number">Die zu überprüfende Zahl.</param>
    /// <param name="certainty">Die Anzahl der Iterationen für den Miller-Rabin-Test (Standardwert: 10).</param>
    /// <returns>True, wenn die Zahl wahrscheinlich eine Primzahl ist, andernfalls false.</returns>
    public static bool IsProbablePrime(this BigInteger number, int certainty = 10)
    {
        if (number < 2) return false;
        if (number != 2 && number % 2 == 0) return false;

        var d = number - 1;
        var r = 0;
        while (d % 2 == 0)
        {
            d /= 2;
            r++;
        }

        var a = 2 + new Random().Next() % (number - 4);
        for (var i = 0; i < certainty; i++)
        {
            var x = BigInteger.ModPow(a, d, number);
            if (x == 1 || x == number - 1) continue;

            var composite = true;
            for (var j = 0; j < r - 1; j++)
            {
                x = BigInteger.ModPow(x, 2, number);
                if (x != number - 1) continue;
                composite = false;
                break;
            }

            if (composite) return false;
        }

        return true;
    }

    /// <summary>
    /// Berechnet den größten gemeinsamen Teiler (ggT) von zwei Zahlen.
    /// </summary>
    /// <param name="a">Die erste Zahl.</param>
    /// <param name="b">Die zweite Zahl.</param>
    /// <returns>Der größte gemeinsame Teiler von a und b.</returns>
    public static BigInteger Gcd(BigInteger a, BigInteger b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    /// <summary>
    /// Berechnet das multiplikative Inverse von a modulo m.
    /// </summary>
    /// <param name="a">Die Zahl, deren Inverses berechnet werden soll.</param>
    /// <param name="m">Der Modulus.</param>
    /// <returns>Das multiplikative Inverse von a modulo m.</returns>
    public static BigInteger ModInverse(BigInteger a, BigInteger m)
    {
        var m0 = m;
        BigInteger y = 0, x = 1;
        if (m == 1)
            return 0;

        while (a > 1)
        {
            var q = a / m;
            var t = m;

            m = a % m;
            a = t;
            t = y;

            y = x - q * y;
            x = t;
        }

        if (x < 0)
            x += m0;

        return x;
    }

    /// <summary>
    /// Generiert eine große Primzahl mit der angegebenen Bitlänge.
    /// </summary>
    /// <param name="bitLength">Die Bitlänge der zu generierenden Primzahl.</param>
    /// <param name="rng">Der Zufallszahlengenerator.</param>
    /// <returns>Eine große Primzahl.</returns>
    public static BigInteger GenerateLargePrime(int bitLength, RandomNumberGenerator rng)
    {
        BigInteger number;

        do
        {
            number = GenerateRandomBigInteger(bitLength, rng);
        } while (!number.IsProbablePrime());

        return number;
    }

    /// <summary>
    /// Generiert eine zufällige große Ganzzahl mit der angegebenen Bitlänge.
    /// </summary>
    /// <param name="bitLength">Die Bitlänge der zu generierenden Zahl.</param>
    /// <param name="rng">Der Zufallszahlengenerator.</param>
    /// <returns>Eine zufällige große Ganzzahl.</returns>
    public static BigInteger GenerateRandomBigInteger(int bitLength, RandomNumberGenerator rng)
    {
        var bytes = new byte[bitLength / 8];

        rng.GetBytes(bytes);
        bytes[^1] &= 0x7F;
        return new BigInteger(bytes);
    }
}