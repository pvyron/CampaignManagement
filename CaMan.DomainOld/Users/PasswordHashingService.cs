using System.Security.Cryptography;

namespace CaMan.DomainOld.Users;

internal static class PasswordHashingService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA512;

    public static Password Hash(string rawPassword)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(rawPassword, salt, Iterations, HashAlgorithmName, HashSize);

        return new Password
        {
            Hash = hash, 
            Salt = salt ,
            Iterations = Iterations,
            HashAlgorithmName = HashAlgorithmName.Name!,
            HashSize = HashSize
        };
    }

    public static bool Verify(string password, Password expected)
    {
        var hasAlgorithmName = new HashAlgorithmName(expected.HashAlgorithmName);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(password, expected.Salt, expected.Iterations,
            hasAlgorithmName, expected.HashSize);

        return CryptographicOperations.FixedTimeEquals(expected.Hash, inputHash);
    }
}