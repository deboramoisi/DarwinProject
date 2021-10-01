using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class DevicesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DevicesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return Ok(await _unitOfWork.DeviceRepository.GetDevicesAsync());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Device>> GetDevices(string name) 
        {
            return await _unitOfWork.DeviceRepository.GetDeviceByNameAsync(name);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> Add(Device device) {
            if (!await _unitOfWork.DeviceRepository.Add(device)) return Ok("Device already exists");

            await _unitOfWork.SaveChangesAsync();
            return device;
        }

        [HttpPut]
        public async Task<ActionResult> Update(Device device) 
        {
            _unitOfWork.DeviceRepository.Update(device);    
            if (!await _unitOfWork.SaveChangesAsync()) return BadRequest("Error while updating");

            return Ok("Device updated successfully");
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            if (id < 0) return BadRequest("Invalid id");
            var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(id);
            
            if (device == null) return NotFound("Device not found"); 

            _unitOfWork.DeviceRepository.Remove(device);
            if (await _unitOfWork.SaveChangesAsync()) 
                return Ok("Device deleted successfully");

            return BadRequest("Failed to delete the device");
        }

    }
}