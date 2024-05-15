using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotionGlow.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Encapsulationenforce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESP32_Device",
                columns: table => new
                {
                    DeviceID = table.Column<int>(type: "int", nullable: false),
                    DeviceName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    DeviceType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Location = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ESP32_De__49E123317F6364D6", x => x.DeviceID);
                });

            migrationBuilder.CreateTable(
                name: "PIRSensor",
                columns: table => new
                {
                    SensorID = table.Column<int>(type: "int", nullable: false),
                    DeviceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PIRSenso__D809841AE0FAE2E3", x => x.SensorID);
                    table.ForeignKey(
                        name: "FK__PIRSensor__Devic__4E88ABD4",
                        column: x => x.DeviceID,
                        principalTable: "ESP32_Device",
                        principalColumn: "DeviceID");
                });

            migrationBuilder.CreateTable(
                name: "SoundSensor",
                columns: table => new
                {
                    SensorID = table.Column<int>(type: "int", nullable: false),
                    DeviceID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SoundSen__D809841A97688CF6", x => x.SensorID);
                    table.ForeignKey(
                        name: "FK__SoundSens__Devic__4BAC3F29",
                        column: x => x.DeviceID,
                        principalTable: "ESP32_Device",
                        principalColumn: "DeviceID");
                });

            migrationBuilder.CreateTable(
                name: "SensorActivityLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false),
                    DeviceID = table.Column<int>(type: "int", nullable: true),
                    SoundSensorID = table.Column<int>(type: "int", nullable: true),
                    PIRSensorID = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoundLevel = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SensorAc__5E5499A8770AC7AC", x => x.LogID);
                    table.ForeignKey(
                        name: "FK__SensorAct__Devic__5165187F",
                        column: x => x.DeviceID,
                        principalTable: "ESP32_Device",
                        principalColumn: "DeviceID");
                    table.ForeignKey(
                        name: "FK__SensorAct__PIRSe__534D60F1",
                        column: x => x.PIRSensorID,
                        principalTable: "PIRSensor",
                        principalColumn: "SensorID");
                    table.ForeignKey(
                        name: "FK__SensorAct__Sound__52593CB8",
                        column: x => x.SoundSensorID,
                        principalTable: "SoundSensor",
                        principalColumn: "SensorID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PIRSensor_DeviceID",
                table: "PIRSensor",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorActivityLog_DeviceID",
                table: "SensorActivityLog",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorActivityLog_PIRSensorID",
                table: "SensorActivityLog",
                column: "PIRSensorID");

            migrationBuilder.CreateIndex(
                name: "IX_SensorActivityLog_SoundSensorID",
                table: "SensorActivityLog",
                column: "SoundSensorID");

            migrationBuilder.CreateIndex(
                name: "IX_SoundSensor_DeviceID",
                table: "SoundSensor",
                column: "DeviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorActivityLog");

            migrationBuilder.DropTable(
                name: "PIRSensor");

            migrationBuilder.DropTable(
                name: "SoundSensor");

            migrationBuilder.DropTable(
                name: "ESP32_Device");
        }
    }
}
