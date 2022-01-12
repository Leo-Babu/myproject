namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(string username, string password);
    }
}