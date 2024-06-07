using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;
using MotionGlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.BLL.Services
{
    public class ESP32_DeviceService : IESP32_DeviceService
    {
        private readonly MotionGlowDbContext _dbContext;

        public ESP32_DeviceService(MotionGlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ESP32_Device>> GetAllDevicesAsync()
        {
            return await _dbContext.ESP32_Device.ToListAsync();
        }

        public async Task<ESP32_Device> GetDeviceByIdAsync(int id)
        {
            return await _dbContext.ESP32_Device.FindAsync(id);
        }

        public async Task AddDeviceAsync(ESP32_Device device)
        {
            _dbContext.ESP32_Device.Add(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDeviceAsync(ESP32_Device device)
        {
            _dbContext.ESP32_Device.Update(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var device = await _dbContext.ESP32_Device.FindAsync(id);
            if (device != null)
            {
                _dbContext.ESP32_Device.Remove(device);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}