namespace CaMan.Api.Services;

public sealed class IdentityScope
{
    public string EmailHeader { get; private set; } = string.Empty;
    public string KeyHeader { get; private set; } = string.Empty;
    public bool IsTest { get; private set; } = true;

    public void Setup(string email, string key)
    {
        EmailHeader = email;
        KeyHeader = key;

        IsTest = email == "testUser";
    }
}