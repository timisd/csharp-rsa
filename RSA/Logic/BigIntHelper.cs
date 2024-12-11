using System.Numerics;
using System.Security.Cryptography;

namespace RSA.Logic;

public static class BigIntHelper
{
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

    public static BigInteger GenerateLargePrime(int bitLength, RandomNumberGenerator rng)
    {
        BigInteger number;

        do
        {
            number = GenerateRandomBigInteger(bitLength, rng);
        } while (!number.IsProbablePrime());

        return number;
    }

    public static BigInteger GenerateRandomBigInteger(int bitLength, RandomNumberGenerator rng)
    {
        var bytes = new byte[bitLength / 8];

        rng.GetBytes(bytes);
        bytes[^1] &= 0x7F;
        return new BigInteger(bytes);
    }
}