using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    public class DevicesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public DevicesController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        // Get all devices for the list
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDevices()
        {
            var devices = await _unitOfWork.DeviceRepository.GetDevicesAsync();
            var devicesToReturn = _mapper.Map<IEnumerable<DeviceDto>>(devices);
            return Ok(devicesToReturn);
        }

        [HttpGet("assigned")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAssignedDevices()
        {
            return Ok(await _unitOfWork.DeviceRepository.GetAssignedDevicesAsync());
        }

        [HttpGet("unassigned")]
        public async Task<ActionResult<IEnumerable<Device>>> GetUnassignedDevices()
        {
            return Ok(await _unitOfWork.DeviceRepository.GetUnassignedDevicesAsync());
        }

        // Return device by id
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceDto>> GetDevices(int id) 
        {
            var device = _mapper.Map<DeviceDto>(await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(id));
            return device;
        }

        // Assign the device with the id "id" to the logged user
        [HttpGet("assign-device/{deviceId}")]
        public async Task<ActionResult> AssignDevice(int deviceId) 
        {
            var username = _userManager.GetUserName(User);
            if (username == null) return NotFound();

            var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(deviceId);

            if (device == null) return NotFound();
            if (device.AppUserId != null) return BadRequest("Device already assigned to user");

            await _unitOfWork.DeviceRepository.AssignDeviceAsync(device, username);  
            if (!await _unitOfWork.SaveChangesAsync()) return BadRequest("Error while updating");

            return Ok();
        }

        // Unassign the device
        [HttpDelete("unassign-device/{deviceId}")]
        public async Task<ActionResult> UnassignDevice(int deviceId) 
        {
            var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(deviceId);

            if (device.AppUserId == null) return NotFound();
            
            _unitOfWork.DeviceRepository.UnassignDevice(device);
            if (!await _unitOfWork.SaveChangesAsync()) return BadRequest("Error while unassigning device");
            return Ok();
        }  

    }
}