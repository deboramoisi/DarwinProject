using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Actions for admins: add, edit, delete devices
namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add-device")]
        public async Task<ActionResult<Device>> Add(Device device) 
        {
            if (!await _unitOfWork.DeviceRepository.AddAsync(device)) return Ok("Device already exists");

            await _unitOfWork.SaveChangesAsync();
            return device;
        }

        [HttpPut("edit-device")]
        public async Task<ActionResult> Update(DeviceDto deviceDto) 
        {
            var device = _mapper.Map<Device>(deviceDto);

            _unitOfWork.DeviceRepository.Update(device);    
            if (!await _unitOfWork.SaveChangesAsync()) return BadRequest("Error while updating");

            return NoContent();
        } 

        [HttpDelete("delete-device/{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            if (id < 0) return BadRequest("Invalid id");
            var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(id);
            
            if (device == null) return NotFound(); 

            _unitOfWork.DeviceRepository.Remove(device);
            if (await _unitOfWork.SaveChangesAsync()) 
                return Ok();

            return BadRequest("Failed to delete the device");
        }
    }
}