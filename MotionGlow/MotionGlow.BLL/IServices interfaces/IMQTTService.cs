using Microsoft.Extensions.Hosting;
using MotionGlow.DAL.Models;
using System.Threading.Tasks;

public interface IMqttService
{
    Task StartAsync(string brokerUrl, string clientId, string username, string password);
    Task<SensorData> GetLatestSensorDataAsync();
    Task DisconnectAsync();
}
