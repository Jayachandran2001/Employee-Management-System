namespace EmployeeManagment.API.Services.Interface
{
    public interface ITokenBlacklistService
    {
        void AddToBlacklist(string token);
        bool IsTokenBlacklisted(string token);
    }
}