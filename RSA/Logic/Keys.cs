using System.Numerics;
using System.Security.Cryptography;

namespace RSA.Logic;

/// <summary>
/// Klasse zur Generierung und Verwaltung von RSA-Schlüsseln.
/// </summary>
public class Keys
{
    private const int BitLength = 256;
    private static Keys _instance = null!;

    /// <summary>
    /// Privater Konstruktor zur Verhinderung der direkten Instanziierung.
    /// </summary>
    private Keys()
    {
    }

    /// <summary>
    /// Die Primzahl P.
    /// </summary>
    public BigInteger P { get; private set; }

    /// <summary>
    /// Die Primzahl Q.
    /// </summary>
    public BigInteger Q { get; private set; }

    /// <summary>
    /// Der Modulus N, berechnet als P * Q.
    /// </summary>
    public BigInteger N { get; private set; }

    /// <summary>
    /// Der öffentliche Exponent E.
    /// </summary>
    public BigInteger E { get; private set; }

    /// <summary>
    /// Der private Exponent D.
    /// </summary>
    public BigInteger D { get; private set; }

    /// <summary>
    /// Der Wert von Phi, berechnet als (P - 1) * (Q - 1).
    /// </summary>
    public BigInteger Phi { get; private set; }

    /// <summary>
    /// Generiert ein neues Schlüsselpaar mit zufälligen Primzahlen P und Q.
    /// </summary>
    /// <returns>Ein neues <see cref="Keys"/>-Objekt mit generierten Werten.</returns>
    public static Keys Generate()
    {
        _instance = new Keys();

        var rng = RandomNumberGenerator.Create();
        _instance.P = BigIntHelper.GenerateLargePrime(BitLength / 2, rng);
        _instance.Q = BigIntHelper.GenerateLargePrime(BitLength / 2, rng);
        CalculateN();
        CalculatePhi();
        GenerateE(BitLength, rng);
        CalculateD();

        return _instance;
    }

    /// <summary>
    /// Generiert ein Schlüsselpaar mit den angegebenen Primzahlen P, Q und dem öffentlichen Exponenten E.
    /// </summary>
    /// <param name="p">Die Primzahl P.</param>
    /// <param name="q">Die Primzahl Q.</param>
    /// <param name="e">Der öffentliche Exponent E.</param>
    /// <returns>Ein neues <see cref="Keys"/>-Objekt mit den angegebenen Werten.</returns>
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
        CalculateD();

        return _instance;
    }

    /// <summary>
    /// Berechnet den Modulus N als Produkt von P und Q.
    /// </summary>
    private static void CalculateN()
    {
        _instance.N = _instance.P * _instance.Q;
    }

    /// <summary>
    /// Berechnet den Wert von Phi als (P - 1) * (Q - 1).
    /// </summary>
    private static void CalculatePhi()
    {
        _instance.Phi = (_instance.P - 1) * (_instance.Q - 1);
    }

    /// <summary>
    /// Generiert den öffentlichen Exponenten E, der relativ prim zu Phi ist.
    /// </summary>
    /// <param name="bitLength">Die Bitlänge für die Generierung von E.</param>
    /// <param name="rng">Der Zufallszahlengenerator.</param>
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

    /// <summary>
    /// Berechnet den privaten Exponenten D als das multiplikative Inverse von E modulo Phi.
    /// </summary>
    private static void CalculateD()
    {
        _instance.D = BigIntHelper.ModInverse(_instance.E, _instance.Phi);
    }
}