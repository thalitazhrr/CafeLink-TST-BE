using CafeLinkAPI.Entities;

namespace CafeLinkAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Account account);
    }
}