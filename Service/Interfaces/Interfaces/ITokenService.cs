using TODOLIST.Domain.Models;

namespace Service.Interfaces.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
    
}
