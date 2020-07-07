﻿using AutoMapper;
using CoreApi.Domain;
using CoreApi.Domain.Response;
using CoreApi.Domain.Service;
using CoreApi.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;

            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            IEnumerable<Claim> claims = User.Claims;

            string userId = claims.Where(a => a.Type == ClaimTypes.NameIdentifier).First().Value;

            BaseResponse<User> userResponse = userService.FindById(int.Parse(userId));

            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser(UserResource userResource)
        {
            User user = mapper.Map<UserResource, User>(userResource);

            BaseResponse<User> userResponse = userService.AddUser(user);

            if (userResponse.Success)
            {
                return Ok(userResponse.Extra);
            }
            else
            {
                return BadRequest(userResponse.ErrorMessage);
            }
        }
    }
}