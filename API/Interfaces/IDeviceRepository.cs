using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IDeviceRepository
    {
        Task<bool> AddAsync(Device device);
        void Remove(Device device);
        void Update(Device device);
        Task<IEnumerable<Device>> GetDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task<Device> GetDeviceByNameAsync(string name);
        Task<Device> GetDeviceForUserAsync(string username);
        Task AssignDeviceAsync(Device device, string username);
        void UnassignDevice(Device device);
        Task<IEnumerable<Device>> GetAssignedDevicesAsync();
        Task<IEnumerable<Device>> GetUnassignedDevicesAsync();
    }
}