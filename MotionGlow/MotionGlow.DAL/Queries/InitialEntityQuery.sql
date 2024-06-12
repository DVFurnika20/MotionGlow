CREATE DATABASE MotionGlow;

USE MotionGlow;

-- Create the ESP32_Device table
CREATE TABLE ESP32_Device (
    DeviceID INT PRIMARY KEY IDENTITY(1,1),
    DeviceName VARCHAR(100) NOT NULL,
    DeviceType VARCHAR(50) NOT NULL,
    Location VARCHAR(100) NOT NULL,
    Description TEXT
);

-- Create the SoundSensor table
CREATE TABLE SoundSensor (
    SensorID INT PRIMARY KEY IDENTITY(1,1),
    DeviceID INT NOT NULL,
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID)
);

-- Create the PIRSensor table
CREATE TABLE PIRSensor (
    SensorID INT PRIMARY KEY IDENTITY(1,1),
    DeviceID INT NOT NULL,
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID)
);

-- Create the SensorActivityLog table
CREATE TABLE SensorActivityLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    DeviceID INT NOT NULL,
    SoundSensorID INT NOT NULL,
    PIRSensorID INT NOT NULL,
    Timestamp DATETIME NOT NULL,
    SoundLevel INT,
    Distance DECIMAL(5, 2), 
    FOREIGN KEY (DeviceID) REFERENCES ESP32_Device(DeviceID),
    FOREIGN KEY (SoundSensorID) REFERENCES SoundSensor(SensorID),
    FOREIGN KEY (PIRSensorID) REFERENCES PIRSensor(SensorID)
);

-- Create the User table
CREATE TABLE [Users] (
    UsersID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    IsAdmin BIT NOT NULL
);


-- Add indexes
CREATE INDEX idx_DeviceName ON ESP32_Device(DeviceName);
CREATE INDEX idx_DeviceType ON ESP32_Device(DeviceType);
CREATE INDEX idx_Location ON ESP32_Device(Location);
CREATE INDEX idx_Email ON Users(Email);