# MotionGlow

MotionGlow is a project designed to utilize an ESP32 to send data to an MQTT Broker. This project subscribes to the broker, receiving messages in real-time, saving them to a database, and displaying the data on the main page using graphs.

## Table of Contents
- [Overview](#overview)
- [Used Programs and Languages](#used-programs-and-languages)
- [Diagrams](#diagrams)
- [Setup Instructions](#setup-instructions)
- [Authors](#authors)

## Overview
MotionGlow integrates several technologies to create a seamless data flow from an ESP32 device to a web interface:
1. **ESP32**: Collects and sends data.
2. **MQTT Broker**: HiveMQ is used to handle real-time messaging.
3. **Database**: SQL Database managed via SSMS stores the received data.
4. **Web Interface**: An ASP.NET webpage displays the data using graphs.

## Used Programs and Languages
- **IDEs**:
  - Visual Studio
  - JetBrains Rider
- **Presentation**:
  - PowerPoint
- **Programming Languages**:
  - C# for the project
  - C++ for the ESP32
- **MQTT Broker**:
  - HiveMQ
- **Web Development**:
  - ASP.NET
- **Database Management**:
  - SQL Server Management Studio (SSMS)

## Diagrams
For detailed diagrams of the project, refer to the [Lucid Diagrams](https://lucid.app/folder/invitations/accept/inv_d20c8052-fea4-40bc-ac61-9d39c80f7374) we have.

## Setup Instructions
1. **ESP32 Setup**:
   - Open the Arduino IDE.
   - In the ESP32 code, modify the following definitions with your own WiFi credentials and client ID (Client ID is used to set an ESP ID):
     ```cpp
     #define CLIENT_ID "your_client_id"
     #define WiFiName "your_wifi_name"
     #define WiFiPassword "your_wifi_password"
     ```
   - Example:
     ```cpp
     #define CLIENT_ID "5"
     #define WiFiName "S24Ultra"
     #define WiFiPassword "777888999"
     ```
   - Upload the code to your ESP32.

2. **MQTT Broker Setup**:
   - You can use the one provided in the code.

   - If you prefer your own, sign up for free on [HiveMQ](https://console.hivemq.cloud/?utm_source=hivemq-com&utm_medium=download-page&utm_campaign=cloud&_gl=1*9mc9aa*_gcl_aw*R0NMLjE3MTk5NTMwMTEuRUFJYUlRb2JDaE1JajhhWG9KeUpod01WbjFCQkFoMXd3QUZBRUFBWUFTQUFFZ0p4RVBEX0J3RQ..*_gcl_au*MTk0MjcyNTEwOS4xNzE5OTUzMDEx*_ga*MTQxNDM0MjEzOS4xNzE5OTUzMDEx*_ga_P96XGQCLE4*MTcxOTk1MzAwOS4xLjEuMTcxOTk1MzAzMi4zNy4wLjA.).
   - Make sure to create access credentials with publish permissions for the ESP and subscribe permissions for the PC.
   - In the Arduino code, replace mqtt_server with the link to the cloud server.
   - Replace mqtt_user and mqtt_password with the access credentials you created.
   - Upload the code to the ESP.


3. **Database Setup**:
   - Set up your SQL Database using SSMS and the provided initial query in the DAL folder.

4. **Web Interface Setup**:
   - Open the ASP.NET project in your preferred IDE (Visual Studio or JetBrains Rider).
   - Configure the connection string to your SQL Database.
   - Build and run the ASP.NET project.

5. **Receiving Messages From The ESP**
   - Plug in your ESP, run the ASP.NET project.
   - Blow into the sound sensor or put your hand in front of the PIR sensor, then refresh the page.
   - If all is connected and running as intended, a new entry will show up on the graphs.

6. **Fixing ESP Issues**
   - Connect the ESP to a USB port on your PC.
   - Open Arduino IDE, Tools > Serial Monitor.
   - Press the reset button on the ESP (Normally on the left side of the port)
   - If it is stuck on WiFi connecting, check the credentials you've entered or get closer to the router/hotspot.
   - If it is stuck on MQTT Broker, it will eventually print an error code. Google it.

## Authors
- Daniil Furnika
- Kaloyan Lambov