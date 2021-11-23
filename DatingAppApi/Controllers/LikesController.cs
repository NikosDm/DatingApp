using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingAppApi.DTOs;
using DatingAppApi.Entities;
using DatingAppApi.Extensions;
using DatingAppApi.Helpers;
using DatingAppApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingAppApi.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username) 
        {
            var sourceUserID = User.GetUserID();
            var LikedUser = await _unitOfWork.UserRepository.GetMemberByUsernameAsync(username);
            var sourceUser = await _unitOfWork.LikesRepository.GetUserWithLikes(sourceUserID);

            if (LikedUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself");

            var userLike = await _unitOfWork.LikesRepository.GetUserLike(sourceUserID, LikedUser.Id);

            if (userLike != null) return BadRequest("You already like this user");

            userLike = new UserLike 
            {
                SourceUserID = sourceUserID,
                LikedUserID = LikedUser.Id
            };

            sourceUser.LikedUsers.Add(userLike);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to like user");            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDTO>>> GetUserLikes([FromQuery]LikesParams likesParams) 
        {
            likesParams.UserId = User.GetUserID();
            var users = await _unitOfWork.LikesRepository.GetUserLikes(likesParams);

            Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(users);
        }
    }
}