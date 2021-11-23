using System.Threading.Tasks;
using DatingAppApi.Entities;

namespace DatingAppApi.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}