using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;

namespace MotionGlow.BLL.Services
{
    public class ESP32_DeviceService : IESP32_DeviceService
    {
        private readonly IESP32_DeviceService _deviceService;

        public ESP32_DeviceService(IESP32_DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IEnumerable<ESP32_Device>> GetAllDevicesAsync()
        {
            return await _deviceService.GetAllDevicesAsync();
        }

        public async Task<ESP32_Device> GetDeviceByIdAsync(int id)
        {
            return await _deviceService.GetDeviceByIdAsync(id);
        }

        public async Task AddDeviceAsync(ESP32_Device device)
        {
            await _deviceService.AddDeviceAsync(device);
        }

        public async Task UpdateDeviceAsync(ESP32_Device device)
        {
            await _deviceService.UpdateDeviceAsync(device);
        }

        public async Task DeleteDeviceAsync(int id)
        {
            await _deviceService.DeleteDeviceAsync(id);
        }
    }
}