using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;
        
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles() 
        {
            var users = await  _userManager.Users
                .Include(roles => roles.UserRoles)
                    .ThenInclude(role => role.Role)
                .OrderBy(user => user.UserName)
                .Select(user => new 
                {
                    user.Id,
                    UserName = user.UserName,
                    Roles = user.UserRoles.Select(rn => rn.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles) 
        {
            var selectedRoles = roles.Split(",").ToArray();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return NotFound("Could not find user!");
            
            var userRoles = await _userManager.GetRolesAsync(user);
            // var result = await _userManager.AddToRoleAsync(user, selectedRoles.Except(userRoles));
            // if (!result.Succeeded) return BadRequest("Failed to edit roles of users");

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            if (!result.Succeeded) return BadRequest("Failed to remove user from roles");
            return Ok(await _userManager.GetRolesAsync(user));
        }
    }
}