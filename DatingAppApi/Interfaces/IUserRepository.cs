using System.Collections.Generic;
using System.Threading.Tasks;
using DatingAppApi.DTOs;
using DatingAppApi.Entities;
using DatingAppApi.Helpers;

namespace DatingAppApi.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserById(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<PagedList<MemberDTO>> GetMembersAsync(UserParams userParams);
        Task<MemberDTO> GetMemberByUsernameAsync(string username);
        Task<string> GetUserGender(string username);
    }
}