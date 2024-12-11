using System.Numerics;

namespace RSA.Logic;

public static class Encryption
{
    public static byte[] Encrypt(byte[] data, BigInteger e, BigInteger n)
    {
        var m = new BigInteger(data);
        var c = BigInteger.ModPow(m, e, n);
        return c.ToByteArray();
    }

    public static byte[] Decrypt(byte[] data, BigInteger d, BigInteger n)
    {
        var c = new BigInteger(data);
        var m = BigInteger.ModPow(c, d, n);
        return m.ToByteArray();
    }
}