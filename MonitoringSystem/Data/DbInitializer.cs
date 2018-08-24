using MonitoringSystem.Models;
using System;
using System.Linq;

namespace MonitoringSystem.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any temperatures.
            if (context.Rooms.Any())
            {
                return;   // DB has been seeded
            }
            var rooms = new Room[]
            {
                new Room{IsDeleted = false, RoomCode="R1", RoomName="Room 1"},
                new Room{IsDeleted = false, RoomCode="R2", RoomName="Room 2"},
                new Room{IsDeleted = false, RoomCode="R3", RoomName="Room 3"},
            };
            foreach (var room in rooms)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();

            var sensors = new Sensor[]
            {
                new Sensor{IsDeleted = false, SensorCode="R1.1", SensorName="Sensor 1", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="R1.2", SensorName="Sensor 2", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="R1.3", SensorName="Sensor 3", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="R1.4", SensorName="Sensor 4", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="R1.5", SensorName="Sensor 5", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="R1.6", SensorName="Sensor 6", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)}
            };
            foreach (var sensor in sensors)
            {
                context.Sensors.Add(sensor);
            }
            context.SaveChanges();

            var fans = new Fan[]
            {
                new Fan{IsDeleted = false, FanCode="FI1", FanName="Fan 1",IsOn=true,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Fan{IsDeleted = false, FanCode="FI2", FanName="Fan 2",IsOn=false,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Fan{IsDeleted = false, FanCode="FI3", FanName="Fan 3",IsOn=false,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
            };
            foreach (var fan in fans)
            {
                context.Fans.Add(fan);
            }
            context.SaveChanges();

            var racks = new Rack[]
            {
                new Rack{IsDeleted = false, RackCode="S1",RackName="Rack 1",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1),Location = 0},
                new Rack{IsDeleted = false, RackCode="S2",RackName="Rack 2",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1),Location = 0},
                new Rack{IsDeleted = false, RackCode="S3",RackName="Rack 3",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2),Location = 1},
                new Rack{IsDeleted = false, RackCode="S4",RackName="Rack 4",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2),Location = 1},
                new Rack{IsDeleted = false, RackCode="S5",RackName="Rack 5",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3),Location = 2},
                new Rack{IsDeleted = false, RackCode="S6",RackName="Rack 6",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3),Location = 2},
                new Rack{IsDeleted = false, RackCode="S7",RackName="Rack 7",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4),Location = 3},
                new Rack{IsDeleted = false, RackCode="S8",RackName="Rack 8",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4),Location = 3},
                new Rack{IsDeleted = false, RackCode="S9",RackName="Rack 9",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5),Location = 4},
                new Rack{IsDeleted = false, RackCode="S10",RackName="Rack 10",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5),Location = 4},
                new Rack{IsDeleted = false, RackCode="S11",RackName="Rack 11",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6),Location = 5},
                new Rack{IsDeleted = false, RackCode="S12",RackName="Rack 12",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6),Location = 5},
                new Rack{IsDeleted = false, RackCode="S13",RackName="Rack 13",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 6},
                new Rack{IsDeleted = false, RackCode="S14",RackName="Rack 14",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 6},
                new Rack{IsDeleted = false, RackCode="S15",RackName="Rack 15",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 6},
                new Rack{IsDeleted = false, RackCode="S16",RackName="Rack 16",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 7},
                new Rack{IsDeleted = false, RackCode="S17",RackName="Rack 17",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 7},
                new Rack{IsDeleted = false, RackCode="S18",RackName="Rack 18",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 8},
                new Rack{IsDeleted = false, RackCode="S19",RackName="Rack 19",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 8},
                new Rack{IsDeleted = false, RackCode="S20",RackName="Rack 20",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 8},
                new Rack{IsDeleted = false, RackCode="S21",RackName="Rack 21",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 9},
                new Rack{IsDeleted = false, RackCode="S22",RackName="Rack 22",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 9},
                new Rack{IsDeleted = false, RackCode="S22",RackName="Rack 23",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 10},
                new Rack{IsDeleted = false, RackCode="S24",RackName="Rack 24",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 10},
                new Rack{IsDeleted = false, RackCode="S25",RackName="Rack 25",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 11},
                new Rack{IsDeleted = false, RackCode="S26",RackName="Rack 26",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 11},
                new Rack{IsDeleted = false, RackCode="S27",RackName="Rack 27",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 11},
                new Rack{IsDeleted = false, RackCode="S28",RackName="Rack 28",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 12},
                new Rack{IsDeleted = false, RackCode="S29",RackName="Rack 29",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 12},
                new Rack{IsDeleted = false, RackCode="S30",RackName="Rack 30",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 13},
                new Rack{IsDeleted = false, RackCode="S31",RackName="Rack 31",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 13},
                new Rack{IsDeleted = false, RackCode="S32",RackName="Rack 32",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 13},
                new Rack{IsDeleted = false, RackCode="S33",RackName="Rack 33",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 14},
                new Rack{IsDeleted = false, RackCode="S34",RackName="Rack 34",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 14},
                new Rack{IsDeleted = false, RackCode="S35",RackName="Rack 35",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 15},
                new Rack{IsDeleted = false, RackCode="S36",RackName="Rack 36",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 15},
                new Rack{IsDeleted = false, RackCode="S37",RackName="Rack 37",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 15},
                new Rack{IsDeleted = false, RackCode="S38",RackName="Rack 38",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 16},
                new Rack{IsDeleted = false, RackCode="S39",RackName="Rack 39",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 16},
                new Rack{IsDeleted = false, RackCode="S40",RackName="Rack 40",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 17},
                new Rack{IsDeleted = false, RackCode="S41",RackName="Rack 41",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 17},
                new Rack{IsDeleted = false, RackCode="S42",RackName="Rack 42",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 17},
                new Rack{IsDeleted = false, RackCode="S43",RackName="Rack 43",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 18},
                new Rack{IsDeleted = false, RackCode="S44",RackName="Rack 44",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 18},
                new Rack{IsDeleted = false, RackCode="S45",RackName="Rack 45",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 19},
                new Rack{IsDeleted = false, RackCode="S46",RackName="Rack 46",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 19},
                new Rack{IsDeleted = false, RackCode="S47",RackName="Rack 47",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 20},
                new Rack{IsDeleted = false, RackCode="S48",RackName="Rack 48",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 21},
                new Rack{IsDeleted = false, RackCode="S49",RackName="Rack 49",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 21},
                new Rack{IsDeleted = false, RackCode="S50",RackName="Rack 50",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 22},
                new Rack{IsDeleted = false, RackCode="S51",RackName="Rack 51",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 22},
                new Rack{IsDeleted = false, RackCode="S52",RackName="Rack 52",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 23},
                new Rack{IsDeleted = false, RackCode="S53",RackName="Rack 53",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 23},
                new Rack{IsDeleted = false, RackCode="S54",RackName="Rack 54",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 23},
                new Rack{IsDeleted = false, RackCode="S55",RackName="Rack 55",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 24},
                new Rack{IsDeleted = false, RackCode="S56",RackName="Rack 56",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 24},
                new Rack{IsDeleted = false, RackCode="S57",RackName="Rack 57",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 24},
                new Rack{IsDeleted = false, RackCode="S58",RackName="Rack 58",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 25},
                new Rack{IsDeleted = false, RackCode="S59",RackName="Rack 59",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 25},
                new Rack{IsDeleted = false, RackCode="S60",RackName="Rack 60",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 26},
                new Rack{IsDeleted = false, RackCode="S61",RackName="Rack 61",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 26},
                new Rack{IsDeleted = false, RackCode="S62",RackName="Rack 62",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 26},
                new Rack{IsDeleted = false, RackCode="S63",RackName="Rack 63",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 27},
                new Rack{IsDeleted = false, RackCode="S64",RackName="Rack 64",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 27},
                new Rack{IsDeleted = false, RackCode="S65",RackName="Rack 65",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 28},
                new Rack{IsDeleted = false, RackCode="S66",RackName="Rack 66",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 28},
                new Rack{IsDeleted = false, RackCode="S67",RackName="Rack 67",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 28},
                new Rack{IsDeleted = false, RackCode="S68",RackName="Rack 68",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 29},
                new Rack{IsDeleted = false, RackCode="S69",RackName="Rack 69",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 29},
                new Rack{IsDeleted = false, RackCode="S70",RackName="Rack 70",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 30},
                new Rack{IsDeleted = false, RackCode="S71",RackName="Rack 71",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 30},
                new Rack{IsDeleted = false, RackCode="S72",RackName="Rack 72",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 30},
                new Rack{IsDeleted = false, RackCode="S73",RackName="Rack 73",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 31},
                new Rack{IsDeleted = false, RackCode="S74",RackName="Rack 74",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 31},
                new Rack{IsDeleted = false, RackCode="S75",RackName="Rack 75",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 32},
                new Rack{IsDeleted = false, RackCode="S76",RackName="Rack 76",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 33},
                new Rack{IsDeleted = false, RackCode="S77",RackName="Rack 77",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 33},
                new Rack{IsDeleted = false, RackCode="S78",RackName="Rack 78",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 34},
                new Rack{IsDeleted = false, RackCode="S79",RackName="Rack 79",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 34},
                new Rack{IsDeleted = false, RackCode="S80",RackName="Rack 80",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 35},
                new Rack{IsDeleted = false, RackCode="S81",RackName="Rack 81",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 35},
                new Rack{IsDeleted = false, RackCode="S82",RackName="Rack 82",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 35},
                new Rack{IsDeleted = false, RackCode="S83",RackName="Rack 83",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 36},
                new Rack{IsDeleted = false, RackCode="S84",RackName="Rack 84",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 36},
                new Rack{IsDeleted = false, RackCode="S85",RackName="Rack 85",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 37},
                new Rack{IsDeleted = false, RackCode="S86",RackName="Rack 86",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 37},
                new Rack{IsDeleted = false, RackCode="S87",RackName="Rack 87",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 37},
                new Rack{IsDeleted = false, RackCode="S88",RackName="Rack 88",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 38},
                new Rack{IsDeleted = false, RackCode="S89",RackName="Rack 89",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 38},
                new Rack{IsDeleted = false, RackCode="S90",RackName="Rack 90",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 39},
                new Rack{IsDeleted = false, RackCode="S91",RackName="Rack 91",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 39},
                new Rack{IsDeleted = false, RackCode="S92",RackName="Rack 92",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 40},
                new Rack{IsDeleted = false, RackCode="S93",RackName="Rack 93",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 40},
                new Rack{IsDeleted = false, RackCode="S94",RackName="Rack 94",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 41},
                new Rack{IsDeleted = false, RackCode="S95",RackName="Rack 95",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 41},
                new Rack{IsDeleted = false, RackCode="S96",RackName="Rack 96",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 42},
                new Rack{IsDeleted = false, RackCode="S97",RackName="Rack 97",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 42},
                new Rack{IsDeleted = false, RackCode="S98",RackName="Rack 98",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 42},
                new Rack{IsDeleted = false, RackCode="S99",RackName="Rack 99",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 43},
                new Rack{IsDeleted = false, RackCode="S100",RackName="Rack 100",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 43},
                new Rack{IsDeleted = false, RackCode="S101",RackName="Rack 101",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 44},
                new Rack{IsDeleted = false, RackCode="S102",RackName="Rack 102",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 44},
                new Rack{IsDeleted = false, RackCode="S103",RackName="Rack 103",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 44},
                new Rack{IsDeleted = false, RackCode="S104",RackName="Rack 104",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 45},
                new Rack{IsDeleted = false, RackCode="S105",RackName="Rack 105",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 45},
                new Rack{IsDeleted = false, RackCode="S106",RackName="Rack 106",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 45},
                new Rack{IsDeleted = false, RackCode="S107",RackName="Rack 107",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 46},
                new Rack{IsDeleted = false, RackCode="S108",RackName="Rack 108",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 46},
                new Rack{IsDeleted = false, RackCode="S109",RackName="Rack 109",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 47},
                new Rack{IsDeleted = false, RackCode="S110",RackName="Rack 110",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1),Location = 47}
            };
            foreach (var rack in racks)
            {
                context.Racks.Add(rack);
            }
            context.SaveChanges();


            // var statuses = new Status[]
            // {
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=44}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=40}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},

            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},


            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=46}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},

            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=46}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},


            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=59}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=59}, Temperature = new Temperature{IsDeleted=false, Value=18 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=19 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=44}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=46}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=46}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},


            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:10:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:20:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:30:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:40:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 20:50:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:10:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:30:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:40:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 21:50:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:10:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:20:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:30:00"), Humidity = new Humidity{IsDeleted=false, Value=43}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:40:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 22:50:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=43}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=65}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:10:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:20:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:30:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:40:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            //     new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-8 00:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},



            // };
            // foreach (var status in statuses)
            // {
            //     context.Statuses.Add(status);
            // }
            // context.SaveChanges();


            //////
            //var temperatures = new Temperature[]
            //{
            //    new Temperature{Value = 80},
            //    new Temperature{Value = 70},
            //    new Temperature{Value = 75}
            //};
            //foreach (var temperature in temperatures)
            //{
            //    context.Temperatures.Add(temperature);
            //}
            //context.SaveChanges();

            //var humidities = new Humidity[]
            //{
            //    new Humidity{Value=20 },
            //    new Humidity{Value=26},
            //    new Humidity{Value=15}
            //};
            //foreach (var humidity in humidities)
            //{
            //    context.Humidities.Add(humidity);
            //}
            //context.SaveChanges();
        }
    }
}
