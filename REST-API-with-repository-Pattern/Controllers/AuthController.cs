using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using REST_API_with_repository_Pattern.Auth;
using REST_API_with_repository_Pattern.Dtos;
using REST_API_with_repository_Pattern.Repositories;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace REST_API_with_repository_Pattern.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IConfiguration config, IMapper mapper)
        {
            _authService = authService;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto userDto)
        {
            if (_authService.UserExists(userDto.UserName))
                return BadRequest("UserName already exists");

//            var userToCreate = _mapper.Map<User>(userDto);
            var userToCreate = new User
            {
                FirstName = userDto.FirstName, UserName = userDto.UserName, LastName = userDto.LastName, PasswordHash = userDto.PasswordHash
            };


            var createdUser = _authService.Register(userToCreate, userDto.PasswordHash);
            return StatusCode(201, (createdUser));
        }


        [HttpPost("login")]
        public IActionResult Login(UserDto loginDto)
        {
            var userFromRepo = _authService.Login(loginDto.UserName, loginDto.PasswordHash);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token), username = userFromRepo.UserName,
                fullname = userFromRepo.FirstName + " " + userFromRepo.LastName
            });
        }
    }
}