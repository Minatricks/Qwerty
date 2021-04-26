﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Qwerty.WEB.Models;
using Qwerty.BLL.Interfaces;
using Qwerty.BLL.DTO;
using Qwerty.BLL.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using Qwerty.DAL.Identity;
using Qwerty.WebApi.Filters;
using Serilog;

namespace Qwerty.WEB.Controllers
{
    [Authorize(Roles = QwertyRoles.User, AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [ModelValidationFilter]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            UserDTO userDto = new UserDTO
            {
                Password = model.Password,
                UserName = model.UserName,
                Name = model.Name,
                Surname = model.SurName,
                Roles = new string[] {QwertyRoles.User}
            };

            await _userService.CreateUserAsync(userDto);
            return Ok();
        }

        [ModelValidationFilter]
        [HttpGet]
        public async Task<ActionResult> GetAllAccounts([FromQuery] FindUserViewModel findUser)
        {
            IEnumerable<UserDTO> users = await _userService
                .GetUsers(findUser?.Name, findUser?.Surname, findUser?.Country, findUser?.City);
            var user = await _userService.FindUserByIdAsync(User.Identity.Name);
            return Ok(users.Where(x => x.Id != user.Id));
        }

        [ModelValidationFilter]
        [HttpGet("without-friends/{userId}")]
        public async Task<IActionResult> GetAllAccountsWithoutFriends(string userId,
            [FromQuery] FindUserViewModel findUser)
        {
            var result = await _userService.GetUsersWithoutFriends(new UserSearchParametersDto
            {
                City = findUser.City,
                Country = findUser.Country,
                Name = findUser.Name,
                Surname = findUser.Surname,
                ExceptFriends = true,
                CurrentUserId = userId
            });

            return Ok(result);
        }

        [HttpDelete]
        [CheckCurrentUserFilter]
        [Route("{userId}")]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUser(userId);
            Log.Information($"User {userId} has been deleted");
            return Ok();
        }

        [HttpPost]
        [CheckCurrentUserFilter]
        [Route("{userId}/uploadImage")]
        public async Task<ActionResult> UploadImage(string userId)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            var postedFile = Request.Form.Files["Image"];
            if (postedFile == null)
            {
                Log.Warning($"User {userId} did not attach the file. 404 returned");
                return BadRequest("File has not been attached");
            }

            if ((postedFile.ContentType.Contains("jpg") || postedFile.ContentType.Contains("png") ||
                 postedFile.ContentType.Contains("jpeg")) == false)
            {
                Log.Warning(
                    $"User {userId} attached the file with uncorrect type of file. Type of file was {postedFile.ContentType}");
                return BadRequest("The file has the wrong format.");
            }

            var imageUrl = $"{Directory.GetCurrentDirectory()}/ProfileImages/{postedFile.FileName}";
            using (var stream = new FileStream(imageUrl, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            await _userService.UploadImage(imageUrl, user.UserName);
            return Ok();
        }

        [ModelValidationFilter]
        [HttpPut]
        public async Task<ActionResult> ChangeUser([FromBody] UserProfileViewModel userModel)
        {
            UserDTO user = new UserDTO
            {
                AboutUrl = userModel.AboutUrl,
                City = userModel.City,
                Country = userModel.Country,
                Email = userModel.Email,
                Name = userModel.Name,
                Surname = userModel.Surname,
                Phone = userModel.Phone,
                Id = userModel.UserId
            };

            await _userService.ChangeProfileInformation(user);
            return Ok();
        }

        [HttpGet]
        [Route("{userId}")]
        [CheckCurrentUserFilter]
        public async Task<ActionResult> GetUser(string userId)
        {
            var user = await _userService.FindUserByIdAsync(userId);
            return Ok(user);
        }
    }
}