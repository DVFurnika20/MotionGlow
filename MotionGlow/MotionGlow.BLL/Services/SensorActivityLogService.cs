using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;
using MotionGlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.BLL.Services
{
    public class SensorActivityLogService : ISensorActivityLogService
    {
        private readonly MotionGlowDbContext _dbContext;

        public SensorActivityLogService(MotionGlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SensorActivityLog>> GetAllSensorActivityLogsAsync()
        {
            return await _dbContext.SensorActivityLog.ToListAsync();
        }

        public async Task<SensorActivityLog> GetSensorActivityLogByIdAsync(int id)
        {
            return await _dbContext.SensorActivityLog.FindAsync(id);
        }

        public async Task AddSensorActivityLogAsync(SensorActivityLog sensorActivityLog)
        {
            _dbContext.SensorActivityLog.Add(sensorActivityLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSensorActivityLogAsync(SensorActivityLog sensorActivityLog)
        {
            _dbContext.SensorActivityLog.Update(sensorActivityLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSensorActivityLogAsync(int id)
        {
            var sensorActivityLog = await _dbContext.SensorActivityLog.FindAsync(id);
            if (sensorActivityLog != null)
            {
                _dbContext.SensorActivityLog.Remove(sensorActivityLog);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}