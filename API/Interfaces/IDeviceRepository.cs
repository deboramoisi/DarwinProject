using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IDeviceRepository
    {
        Task<bool> Add(Device device);
        void Remove(Device device);
        void Update(Device device);
        Task<IEnumerable<Device>> GetDevicesAsync();
        Task<Device> GetDeviceByIdAsync(int id);
        Task<Device> GetDeviceByNameAsync(string name);
    }
}