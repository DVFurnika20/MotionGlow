using System.Collections.Generic;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

namespace MotionGlow.BLL.IServices
{
    public interface ISoundSensorService
    {
        Task<IEnumerable<SoundSensor>> GetAllSoundSensorsAsync();
        Task<SoundSensor> GetSoundSensorByIdAsync(int id);
        Task AddSoundSensorAsync(SoundSensor soundSensor);
        Task UpdateSoundSensorAsync(SoundSensor soundSensor);
        Task DeleteSoundSensorAsync(int id);
    }
}