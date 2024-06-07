using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;
using MotionGlow.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MotionGlow.BLL.Services
{
    public class SoundSensorService : ISoundSensorService
    {
        private readonly MotionGlowDbContext _dbContext;

        public SoundSensorService(MotionGlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<SoundSensor>> GetAllSoundSensorsAsync()
        {
            return await _dbContext.SoundSensor.ToListAsync();
        }

        public async Task<SoundSensor> GetSoundSensorByIdAsync(int id)
        {
            return await _dbContext.SoundSensor.FindAsync(id);
        }

        public async Task AddSoundSensorAsync(SoundSensor soundSensor)
        {
            _dbContext.SoundSensor.Add(soundSensor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSoundSensorAsync(SoundSensor soundSensor)
        {
            _dbContext.SoundSensor.Update(soundSensor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSoundSensorAsync(int id)
        {
            var soundSensor = await _dbContext.SoundSensor.FindAsync(id);
            if (soundSensor != null)
            {
                _dbContext.SoundSensor.Remove(soundSensor);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}