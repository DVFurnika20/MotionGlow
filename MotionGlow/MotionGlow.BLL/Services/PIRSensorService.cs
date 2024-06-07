using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;
using MotionGlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.BLL.Services
{
    public class PIRSensorService : IPIRSensorService
    {
        private readonly MotionGlowDbContext _dbContext;

        public PIRSensorService(MotionGlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PIRSensor>> GetAllPIRSensorsAsync()
        {
            return await _dbContext.PIRSensor.ToListAsync();
        }

        public async Task<PIRSensor> GetPIRSensorByIdAsync(int id)
        {
            return await _dbContext.PIRSensor.FindAsync(id);
        }

        public async Task AddPIRSensorAsync(PIRSensor pirSensor)
        {
            _dbContext.PIRSensor.Add(pirSensor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePIRSensorAsync(PIRSensor pirSensor)
        {
            _dbContext.PIRSensor.Update(pirSensor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePIRSensorAsync(int id)
        {
            var pirSensor = await _dbContext.PIRSensor.FindAsync(id);
            if (pirSensor != null)
            {
                _dbContext.PIRSensor.Remove(pirSensor);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}