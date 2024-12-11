using System.Numerics;

namespace RSA.Logic;

/// <summary>
/// Statische Klasse für die Verschlüsselung und Entschlüsselung von Daten mit RSA.
/// </summary>
public static class Encryption
{
    /// <summary>
    /// Verschlüsselt die angegebenen Daten mit dem öffentlichen Schlüssel (e, n).
    /// </summary>
    /// <param name="data">Die zu verschlüsselnden Daten als Byte-Array.</param>
    /// <param name="e">Der öffentliche Exponent.</param>
    /// <param name="n">Der Modulus.</param>
    /// <returns>Das verschlüsselte Daten-Byte-Array.</returns>
    public static byte[] Encrypt(byte[] data, BigInteger e, BigInteger n)
    {
        var m = new BigInteger(data);
        var c = BigInteger.ModPow(m, e, n);
        return c.ToByteArray();
    }

    /// <summary>
    /// Entschlüsselt die angegebenen Daten mit dem privaten Schlüssel (d, n).
    /// </summary>
    /// <param name="data">Die zu entschlüsselnden Daten als Byte-Array.</param>
    /// <param name="d">Der private Exponent.</param>
    /// <param name="n">Der Modulus.</param>
    /// <returns>Das entschlüsselte Daten-Byte-Array.</returns>
    public static byte[] Decrypt(byte[] data, BigInteger d, BigInteger n)
    {
        var c = new BigInteger(data);
        var m = BigInteger.ModPow(c, d, n);
        return m.ToByteArray();
    }
}