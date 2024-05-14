CREATE DATABASE MotionGlow;

USE MotionGlow;

-- Create the ESP32_Device table
CREATE TABLE ESP32_Device (
    DeviceID INT PRIMARY KEY,
    DeviceName VARCHAR(100),
    DeviceType VARCHAR(50),
    Location VARCHAR(100),
    Description TEXT
);

-- Create the SoundSensor table
CREATE TABLE SoundSensor (
    SensorID INT PRIMARY KEY,
    DeviceID INT,
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID)
);

-- Create the PIRSensor table
CREATE TABLE PIRSensor (
    SensorID INT PRIMARY KEY,
    DeviceID INT,
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID)
);

-- Create the SensorActivityLog table
CREATE TABLE SensorActivityLog (
    LogID INT PRIMARY KEY,
    DeviceID INT,
    SoundSensorID INT,
    PIRSensorID INT,
    Timestamp DATETIME,
    SoundLevel INT,
    Distance DECIMAL(5, 2), 
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID),
    FOREIGN KEY (SoundSensorID) REFERENCES SoundSensor(SensorID),
    FOREIGN KEY (PIRSensorID) REFERENCES PIRSensor(SensorID)
);