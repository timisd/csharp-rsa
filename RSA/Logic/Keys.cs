using System.Numerics;
using System.Security.Cryptography;

namespace RSA.Logic;

public class Keys
{
    private const int BitLength = 256;
    private static Keys _instance = null!;

    private Keys()
    {
    }

    public BigInteger P { get; private set; }
    public BigInteger Q { get; private set; }
    public BigInteger N { get; private set; }
    public BigInteger E { get; private set; }
    public BigInteger D { get; private set; }
    public BigInteger Phi { get; private set; }

    public static Keys Generate()
    {
        _instance = new Keys();

        var rng = RandomNumberGenerator.Create();
        _instance.P = BigIntHelper.GenerateLargePrime(BitLength / 2, rng);
        _instance.Q = BigIntHelper.GenerateLargePrime(BitLength / 2, rng);
        CalculateN();
        CalculatePhi();
        GenerateE(BitLength, rng);
        GenerateD();

        return _instance;
    }

    public static Keys Generate(BigInteger p, BigInteger q, BigInteger e)
    {
        _instance = new Keys
        {
            P = p,
            Q = q,
            E = e
        };

        CalculateN();
        CalculatePhi();
        GenerateD();

        return _instance;
    }

    private static void CalculateN()
    {
        _instance.N = _instance.P * _instance.Q;
    }

    private static void CalculatePhi()
    {
        _instance.Phi = (_instance.P - 1) * (_instance.Q - 1);
    }

    private static void GenerateE(int bitLength, RandomNumberGenerator rng)
    {
        BigInteger e;
        var attempts = 0;

        do
        {
            e = BigIntHelper.GenerateRandomBigInteger(bitLength, rng);
            attempts++;
        } while ((e % 2 == 0 || e <= 1 || BigIntHelper.Gcd(e, _instance.Phi) != 1) && attempts < 10);

        if (attempts >= 10) e = 65537;

        _instance.E = e;
    }

    private static void GenerateD()
    {
        _instance.D = BigIntHelper.ModInverse(_instance.E, _instance.Phi);
    }
}