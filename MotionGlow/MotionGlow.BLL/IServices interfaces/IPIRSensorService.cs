using System.Collections.Generic;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

namespace MMotionGlow.BLL.IServices
{
    public interface IPIRSensorRepository
    {
        Task<IEnumerable<PIRSensor>> GetAllPIRSensorsAsync();
        Task<PIRSensor> GetPIRSensorByIdAsync(int id);
        Task AddPIRSensorAsync(PIRSensor pirSensor);
        Task UpdatePIRSensorAsync(PIRSensor pirSensor);
        Task DeletePIRSensorAsync(int id);
    }
}