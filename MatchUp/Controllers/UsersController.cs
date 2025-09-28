using AutoMapper;
using MatchUp.DTOs;
using MatchUp.Entities;
using MatchUp.Extensions;
using MatchUp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchUp.Controllers
{
    [Authorize]
    public class UsersController(IUserRepo userRepo, IMapper mapper, IPhotoService photoService) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var usersDto = await userRepo.GetMembers();
            return Ok(usersDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var userDto = await userRepo.GetMember(username);
            if (userDto == null) return NotFound();
            return Ok(userDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMember(MemberUpdateDto dto)
        {
            var user = await userRepo.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return BadRequest("User not found");
            mapper.Map(dto, user);
            if (await userRepo.SaveAllAsync()) return NoContent();
            return BadRequest("Couldn't able to update the user. Please try again.");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await userRepo.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return BadRequest("Can't update user");
            var result = await photoService.AddPhotoAsync(file);
            if (result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            user.Photos.Add(photo);
            if (await userRepo.SaveAllAsync())
            {
                return CreatedAtAction(
                    nameof(GetUser),
                    new { username = user.UserName },
                    mapper.Map<PhotoDto>(photo));
            }
            return BadRequest("Error while saving photo. Please try again.");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            var user = await userRepo.GetUserByUsernameAsync(User.GetUsername());
            if (user == null) return BadRequest("No user found");
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if (photo == null || photo.IsMain) return BadRequest("Can't use this as main photo");
            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
            if (currentMain != null) currentMain.IsMain = false;
            photo.IsMain = true;
            if (await userRepo.SaveAllAsync()) return NoContent();
            return BadRequest("Got an error. Please try again");
        }
    }
}
