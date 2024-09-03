namespace CaMan.ApiOld.Services;

public sealed class IdentityScope
{
    public string EmailHeader { get; private set; } = string.Empty;
    public string KeyHeader { get; private set; } = string.Empty;
    // public User? LoggedInUser { get; private set; } = null;
    public bool IsTest { get; private set; } = true;

    public void Setup(string email, string key)
    {
        EmailHeader = email;
        KeyHeader = key;

        IsTest = email == "testUser";
    }
    
    // public void LogInUser(User loggedInUser)
    // {
    //     if (loggedInUser.Email.Value.Equals(EmailHeader))
    //     {
    //         return;
    //     }
    //     
    //     IsTest = false;
    //     LoggedInUser = loggedInUser;
    // }
}