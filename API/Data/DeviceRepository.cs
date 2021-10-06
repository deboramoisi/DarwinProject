using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DeviceRepository : IDeviceRepository
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;

        public DeviceRepository(DataContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Device> GetDeviceByIdAsync(int id)
        {
            return await _context.Devices
                .FindAsync(id);
        }

        public async Task<Device> GetDeviceByNameAsync(string name)
        {
            return await _context.Devices
                .Include(u => u.AppUser)
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Device>> GetDevicesAsync()
        {
            return await _context.Devices
                .Include(u => u.AppUser)
                .OrderBy(x => x.AppUserId)
                    .ThenBy(x => x.Type)
                .ToListAsync();
        }

        public void Update(Device device)
        {
            _context.Entry(device).State = EntityState.Modified;
        }

        public async Task<bool> AddAsync(Device device) 
        {
            if (await DeviceExistsAsync(device)) return false;

            _context.Entry(device).State = EntityState.Added;
            return true;
        }

        public async Task<bool> DeviceExistsAsync(Device device) 
        {
            var deviceFromDb = await GetDeviceByNameAsync(device.Name);
            
            return deviceFromDb != null;
        }

        public void Remove(Device device)
        {
            _context.Entry(device).State = EntityState.Deleted;
        }

        public async Task AssignDeviceAsync(Device device, string username) 
        {
            var user = await _userManager.FindByNameAsync(username);
            user.Devices.Add(device);
            device.AppUserId = user.Id;
            Update(device);
        }

        public void UnassignDevice(Device device) 
        {
            var user = _userManager.FindByIdAsync(device.AppUserId.Value.ToString()).Result;
            user.Devices.Remove(device);
            device.AppUserId = null;
            Update(device);
        }

        public async Task<IEnumerable<Device>> GetAssignedDevicesAsync() 
        {
            return await _context.Devices.Where(x => x.AppUserId != null)
                            // .Include(u => u.AppUser)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Device>> GetUnassignedDevicesAsync() 
        {
            return await _context.Devices.Where(x => x.AppUserId == null)
                            //.Include(u => u.AppUser)
                            .ToListAsync();
        }

        public async Task<Device> GetDeviceForUserAsync(string username) 
        {
            var user = await _userManager.FindByNameAsync(username);
            return _context.Devices.Where(u => u.AppUserId == user.Id).Include(x => x.AppUser).FirstOrDefault();
        } 
    }
}