using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;

namespace MotionGlow.BLL.Services
{
    public class SensorActivityLogService : ISensorActivityLogService
    {
        private readonly ISensorActivityLogService _sensorActivityLogRepository;

        public SensorActivityLogService(ISensorActivityLogService sensorActivityLogRepository)
        {
            _sensorActivityLogRepository = sensorActivityLogRepository;
        }

        public async Task<IEnumerable<SensorActivityLog>> GetAllSensorActivityLogsAsync()
        {
            return await _sensorActivityLogRepository.GetAllSensorActivityLogsAsync();
        }

        public async Task<SensorActivityLog> GetSensorActivityLogByIdAsync(int id)
        {
            return await _sensorActivityLogRepository.GetSensorActivityLogByIdAsync(id);
        }

        public async Task AddSensorActivityLogAsync(SensorActivityLog sensorActivityLog)
        {
            await _sensorActivityLogRepository.AddSensorActivityLogAsync(sensorActivityLog);
        }

        public async Task UpdateSensorActivityLogAsync(SensorActivityLog sensorActivityLog)
        {
            await _sensorActivityLogRepository.UpdateSensorActivityLogAsync(sensorActivityLog);
        }

        public async Task DeleteSensorActivityLogAsync(int id)
        {
            await _sensorActivityLogRepository.DeleteSensorActivityLogAsync(id);
        }
    }
}