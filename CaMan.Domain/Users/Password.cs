namespace CaMan.Domain.Users;

public sealed record Password
{
    public byte[] Hash { get; init; } = null!;
    public byte[] Salt { get; init; } = null!;
    public int HashSize { get; init; }
    public int Iterations { get; init; }
    public string HashAlgorithmName { get; init; } = null!;

    internal Password()
    {
    }

    public static Password Create(string password)
    {
        return PasswordHashingService.Hash(password);
    }
}