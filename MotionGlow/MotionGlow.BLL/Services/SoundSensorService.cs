using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;

namespace MotionGlow.BLL.Services
{
    public class SoundSensorService : ISoundSensorService
    {
        private readonly ISoundSensorService _soundSensorRepository;

        public SoundSensorService(ISoundSensorService soundSensorRepository)
        {
            _soundSensorRepository = soundSensorRepository;
        }

        public async Task<IEnumerable<SoundSensor>> GetAllSoundSensorsAsync()
        {
            return await _soundSensorRepository.GetAllSoundSensorsAsync();
        }

        public async Task<SoundSensor> GetSoundSensorByIdAsync(int id)
        {
            return await _soundSensorRepository.GetSoundSensorByIdAsync(id);
        }

        public async Task AddSoundSensorAsync(SoundSensor soundSensor)
        {
            await _soundSensorRepository.AddSoundSensorAsync(soundSensor);
        }

        public async Task UpdateSoundSensorAsync(SoundSensor soundSensor)
        {
            await _soundSensorRepository.UpdateSoundSensorAsync(soundSensor);
        }

        public async Task DeleteSoundSensorAsync(int id)
        {
            await _soundSensorRepository.DeleteSoundSensorAsync(id);
        }
    }
}