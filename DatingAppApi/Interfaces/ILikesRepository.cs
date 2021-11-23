using System.Collections.Generic;
using System.Threading.Tasks;
using DatingAppApi.DTOs;
using DatingAppApi.Entities;
using DatingAppApi.Helpers;

namespace DatingAppApi.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserID, int likedUserID);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDTO>> GetUserLikes(LikesParams likesParams);
    }
}