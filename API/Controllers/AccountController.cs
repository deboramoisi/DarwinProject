using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper; 
        private readonly ITokenService _tokenService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            IMapper mapper, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;            
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Username is already taken!");            
            
            var user = _mapper.Map<AppUser>(registerDto);
            user.UserName = registerDto.Email;

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);
            
            return new UserDto(user.Id, user.Name, user.Email, await _tokenService.CreateToken(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) 
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null) return Unauthorized("");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Login credentials are not valid");

            return new UserDto(user.Id, user.Name, user.Email, await _tokenService.CreateToken(user));
        }

        private async Task<bool> UserExists(string email) 
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }
    }
}