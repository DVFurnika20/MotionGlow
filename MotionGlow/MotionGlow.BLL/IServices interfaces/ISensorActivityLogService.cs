using System.Collections.Generic;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

namespace MotionGlow.BLL.IServices
{
    public interface ISensorActivityLogService
    {
        Task<IEnumerable<SensorActivityLog>> GetAllSensorActivityLogsAsync();
        Task<SensorActivityLog> GetSensorActivityLogByIdAsync(int id);
        Task AddSensorActivityLogAsync(SensorActivityLog sensorActivityLog);
        Task UpdateSensorActivityLogAsync(SensorActivityLog sensorActivityLog);
        Task DeleteSensorActivityLogAsync(int id);
    }
}