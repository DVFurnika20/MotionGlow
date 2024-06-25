-- Insert into ESP32_Device table
INSERT INTO ESP32_Device (DeviceName, DeviceType, Location, Description)
VALUES ('Device1', 'Type1', 'Location1', 'Description1'),
       ('Device2', 'Type2', 'Location2', 'Description2'),
       ('Device3', 'Type3', 'Location3', 'Description3');

-- Insert into SoundSensor table
INSERT INTO SoundSensor (DeviceID)
VALUES (1), (2), (3);

-- Insert into PIRSensor table
INSERT INTO PIRSensor (DeviceID)
VALUES (1), (2), (3);

-- Insert into SensorActivityLog table
INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES (1, 1, 1, GETDATE(), 10, 1.5),
       (2, 2, 2, GETDATE(), 20, 2.5),
       (3, 3, 3, GETDATE(), 30, 3.5);

-- Insert into Users table
INSERT INTO [Users] (FirstName, LastName, Email, [Password], IsAdmin)
VALUES ('User1', 'Last1', 'user1@example.com', 'password1', 0),
	   ('Admin', 'Adminston', 'admin@gmail.com', 'admin123', 1),

	   -- Update ESP32_Device table
UPDATE ESP32_Device
SET DeviceName = 'UpdatedDevice', DeviceType = 'UpdatedType', Location = 'UpdatedLocation', Description = 'UpdatedDescription'
WHERE DeviceID = 1;

-- Update SoundSensor table
UPDATE SoundSensor
SET DeviceID = 1
WHERE SensorID IN (1, 2, 3);

-- Update PIRSensor table
UPDATE PIRSensor
SET DeviceID = 1
WHERE SensorID IN (1, 2, 3);

-- Update SensorActivityLog table
UPDATE SensorActivityLog
SET DeviceID = 1, SoundSensorID = 1, PIRSensorID = 1, Timestamp = GETDATE(), SoundLevel = 50, Distance = 5.5
WHERE LogID IN (1, 2, 3);
