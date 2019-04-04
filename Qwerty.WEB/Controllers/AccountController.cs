﻿using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.Net.Http;
using Microsoft.AspNet.Identity.Owin;
using Qwerty.WEB.Models;
using Qwerty.BLL.Interfaces;
using Qwerty.BLL.DTO;
using Qwerty.BLL.Infrastructure;
using System.Security.Claims;
using AutoMapper;

namespace UIWebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        public IUserService UserService => Request.GetOwinContext().GetUserManager<IUserService>();
        private IAuthenticationManager AuthenticationManager => Request.GetOwinContext().Authentication;
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           // return Ok(new OperationDetails(true,"Test message","prop")); //TEST DELETE THIS!!!!!!!

            UserDTO userDto = new UserDTO
            {
                Password = model.Password,
                UserName = model.UserName,
                Role = "user"
            };
            OperationDetails operationDetails = await UserService.Create(userDto);
            if (!operationDetails.Succedeed)
                return GetErrorResult(operationDetails);
            return Ok(operationDetails);
        }

        [Route("{Name}/{Surname}")]
        [HttpGet]
        public async Task<IHttpActionResult> FindFriendsByFullName([FromUri]string Name, [FromUri]string Surname)
        {
            IEnumerable<UserDTO> users = await UserService.GetUsersByFullName(Name, Surname);
            if (users == null) return NotFound();
            else return Ok(users);
        }
        //[Route("DeleteUser")]
        [HttpDelete]
        [AllowAnonymous]
        [Route("{UserId}")]
        public async Task<IHttpActionResult> DeleteUser([FromUri] string UserId)
        {
            OperationDetails operationDetails =  await UserService.DeleteUser(UserId);
            if (operationDetails.Succedeed) return Ok();
            else return BadRequest(operationDetails.Message);
        }

        [AllowAnonymous]
        [HttpPut]
        public async Task<IHttpActionResult> ChangeUser([FromBody] UserDTO user)
        {
            OperationDetails operationDetails = await UserService.ChangeProfileInformation(user);
            if (operationDetails.Succedeed) return Ok();
            else return BadRequest(operationDetails.Message);
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IHttpActionResult> GetUser()
        {
            var IdentityClaims = (ClaimsIdentity)User.Identity;
            var UserName = IdentityClaims.FindFirst("sub").Value;
            if (UserName != null)
            {
                var user = await UserService.FindUserByUsername(UserName);
                return Ok(/*Mapper.Map<UserDTO,UserProfileViewModel>(user)*/user);
            }
            else return BadRequest();
        }

        private IHttpActionResult GetErrorResult(OperationDetails result)
        {
            if (result == null)
            {
                return InternalServerError();
            }   
        
            if (!result.Succedeed)
            {
                ModelState.AddModelError(result.Property, result.Message);

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }
                return BadRequest(ModelState);
            }

            return null;
        }
    }
}