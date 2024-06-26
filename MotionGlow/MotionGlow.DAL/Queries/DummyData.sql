-- Insert into ESP32_Device table
INSERT INTO ESP32_Device (DeviceName, DeviceType, Location, Description)
VALUES 
	   ('Device4', 'Type4', 'Location4', 'Description4');

-- Insert into SoundSensor table
INSERT INTO SoundSensor (DeviceID)
VALUES (1), (2), (3);

-- Insert into PIRSensor table
INSERT INTO PIRSensor (DeviceID)
VALUES (1), (2), (3);

-- Insert into PIRSensor table
INSERT INTO PIRSensor (DeviceID)
VALUES (4);

-- Insert into SensorActivityLog table
INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES (1, 1, 1, GETDATE(), 10, 1.5),
       (1, 1, 1, GETDATE(), 20, 2.5),
       (1, 1, 1, GETDATE(), 30, 3.5);

	   -- Insert into SensorActivityLog table
INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES (2, 2, 2, GETDATE(), 10, 1.5),
       (2, 2, 2, GETDATE(), 20, 2.5),
       (2, 2, 2, GETDATE(), 30, 3.5),
	   (2, 2, 2, GETDATE(), 25, 5.5);

	   -- Insert into SensorActivityLog table
INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES (3, 3, 3, GETDATE(), 10, 1.5),
       (3, 3, 3, GETDATE(), 20, 2.5),
       (3, 3, 3, GETDATE(), 30, 3.5);

	   INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES (4, 3, 2, GETDATE(), 10, 1.5),
       (4, 3, 2, GETDATE(), 20, 2.5),
	   (4, 3, 2, GETDATE(), 20, 2.5),
	   (4, 3, 2, GETDATE(), 20, 2.5),
       (4, 3, 2, GETDATE(), 30, 3.5);

	   -- Insert into SensorActivityLog table
INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES 
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+1, GETDATE()), 100, 1.5), -- Monday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+2, GETDATE()), 50, 2.5), -- Tuesday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+3, GETDATE()), 75, 3.5), -- Wednesday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+4, GETDATE()), 120, 4.5), -- Thursday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+5, GETDATE()), 80, 5.5), -- Friday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+6, GETDATE()), 40, 6.5), -- Saturday
(1, 1, 1, DATEADD(day, -DATEPART(dw, GETDATE())+7, GETDATE()), 200, 7.5); -- Sunday

INSERT INTO SensorActivityLog (DeviceID, SoundSensorID, PIRSensorID, Timestamp, SoundLevel, Distance)
VALUES 
(1, 1, 1, DATEADD(hour, 0, GETDATE()), 100, 1.5), -- 00:00
(1, 1, 1, DATEADD(hour, 1, GETDATE()), 50, 2.5), -- 01:00
(1, 1, 1, DATEADD(hour, 2, GETDATE()), 75, 3.5), -- 02:00
(1, 1, 1, DATEADD(hour, 3, GETDATE()), 120, 4.5), -- 03:00
(1, 1, 1, DATEADD(hour, 4, GETDATE()), 80, 5.5), -- 04:00
(1, 1, 1, DATEADD(hour, 5, GETDATE()), 40, 6.5), -- 05:00
(1, 1, 1, DATEADD(hour, 6, GETDATE()), 200, 7.5), -- 06:00
(1, 1, 1, DATEADD(hour, 7, GETDATE()), 150, 8.5), -- 07:00
(1, 1, 1, DATEADD(hour, 8, GETDATE()), 90, 9.5), -- 08:00
(1, 1, 1, DATEADD(hour, 9, GETDATE()), 60, 10.5), -- 09:00
(1, 1, 1, DATEADD(hour, 10, GETDATE()), 70, 11.5), -- 10:00
(1, 1, 1, DATEADD(hour, 11, GETDATE()), 80, 12.5), -- 11:00
(1, 1, 1, DATEADD(hour, 12, GETDATE()), 90, 13.5), -- 12:00
(1, 1, 1, DATEADD(hour, 13, GETDATE()), 100, 14.5), -- 13:00
(1, 1, 1, DATEADD(hour, 14, GETDATE()), 110, 15.5), -- 14:00
(1, 1, 1, DATEADD(hour, 15, GETDATE()), 120, 16.5), -- 15:00
(1, 1, 1, DATEADD(hour, 16, GETDATE()), 130, 17.5), -- 16:00
(1, 1, 1, DATEADD(hour, 17, GETDATE()), 140, 18.5), -- 17:00
(1, 1, 1, DATEADD(hour, 18, GETDATE()), 150, 19.5), -- 18:00
(1, 1, 1, DATEADD(hour, 19, GETDATE()), 160, 20.5), -- 19:00
(1, 1, 1, DATEADD(hour, 20, GETDATE()), 170, 21.5), -- 20:00
(1, 1, 1, DATEADD(hour, 21, GETDATE()), 180, 22.5), -- 21:00
(1, 1, 1, DATEADD(hour, 22, GETDATE()), 190, 23.5), -- 22:00
(1, 1, 1, DATEADD(hour, 23, GETDATE()), 200, 24.5); -- 23:00

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
