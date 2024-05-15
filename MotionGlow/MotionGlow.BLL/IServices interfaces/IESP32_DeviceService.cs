using System.Collections.Generic;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

namespace MotionGlow.BLL.IServices
{
    public interface IESP32_DeviceRepository
    {
        Task<IEnumerable<ESP32_Device>> GetAllDevicesAsync();
        Task<ESP32_Device> GetDeviceByIdAsync(int id);
        Task AddDeviceAsync(ESP32_Device device);
        Task UpdateDeviceAsync(ESP32_Device device);
        Task DeleteDeviceAsync(int id);
    }
}