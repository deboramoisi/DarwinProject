using System.Collections.Generic;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DeviceRepository : IDeviceRepository
    {

        private readonly DataContext _context;

        public DeviceRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices.FindAsync(id);
        }

        public async Task<Device> GetDeviceByNameAsync(string name)
        {
            return await _context.Devices.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Device>> GetDevicesAsync()
        {
            return await _context.Devices.ToListAsync();
        }

        public void Update(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
        }

        public async Task<bool> Add(Device device) 
        {
            if (await DeviceExists(device)) return false;

            _context.Entry(device).State = EntityState.Added;
            return true;
        }

        public async Task<bool> DeviceExists(Device device) 
        {
            var deviceFromDb = await GetDeviceByNameAsync(device.Name);
            
            return deviceFromDb != null;
        }

        public void Remove(Device device)
        {
            _context.Entry(device).State = EntityState.Deleted;
        }
    }
}