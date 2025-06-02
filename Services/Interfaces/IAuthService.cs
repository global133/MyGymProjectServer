namespace MyGymProject.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string? Token, string? Role, string Message)> LoginAsync(string login, string password);
    }
}
