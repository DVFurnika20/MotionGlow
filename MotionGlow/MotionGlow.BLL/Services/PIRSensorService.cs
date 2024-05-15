using System.Collections.Generic;
using System.Threading.Tasks;
using MotionGlow.DAL.Models;
using MotionGlow.BLL.IServices;

namespace MotionGlow.BLL.Services
{
    public class PIRSensorService : IPIRSensorService
    {
        private readonly IPIRSensorService _pirSensorRepository;

        public PIRSensorService(IPIRSensorService pirSensorRepository)
        {
            _pirSensorRepository = pirSensorRepository;
        }

        public async Task<IEnumerable<PIRSensor>> GetAllPIRSensorsAsync()
        {
            return await _pirSensorRepository.GetAllPIRSensorsAsync();
        }

        public async Task<PIRSensor> GetPIRSensorByIdAsync(int id)
        {
            return await _pirSensorRepository.GetPIRSensorByIdAsync(id);
        }

        public async Task AddPIRSensorAsync(PIRSensor pirSensor)
        {
            await _pirSensorRepository.AddPIRSensorAsync(pirSensor);
        }

        public async Task UpdatePIRSensorAsync(PIRSensor pirSensor)
        {
            await _pirSensorRepository.UpdatePIRSensorAsync(pirSensor);
        }

        public async Task DeletePIRSensorAsync(int id)
        {
            await _pirSensorRepository.DeletePIRSensorAsync(id);
        }
    }
}