using System.Collections.Generic;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

namespace MotionGlow.BLL.IServices
{
    public interface IPIRSensorService
    {
        Task<IEnumerable<PIRSensor>> GetAllPIRSensorsAsync();
        Task<PIRSensor> GetPIRSensorByIdAsync(int id);
        Task AddPIRSensorAsync(PIRSensor pirSensor);
        Task UpdatePIRSensorAsync(PIRSensor pirSensor);
        Task DeletePIRSensorAsync(int id);
    }
}