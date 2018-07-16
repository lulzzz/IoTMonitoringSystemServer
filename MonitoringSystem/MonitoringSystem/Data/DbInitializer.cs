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
            if (context.Temperatures.Any())
            {
                return;   // DB has been seeded
            }
            var rooms = new Room[]
            {
                new Room{IsDeleted = false, RoomCode="001", RoomName="Room 1"},
                new Room{IsDeleted = false, RoomCode="002", RoomName="Room 2"},
                new Room{IsDeleted = false, RoomCode="003", RoomName="Room 3"},
            };
            foreach (var room in rooms)
            {
                context.Rooms.Add(room);
            }
            context.SaveChanges();

            var sensors = new Sensor[]
            {
                new Sensor{IsDeleted = false, SensorCode="001", SensorName="Sensor 1", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="002",SensorName="Sensor 2", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Sensor{IsDeleted = false, SensorCode="003", SensorName="Sensor 3", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2)},
                new Sensor{IsDeleted = false, SensorCode="004", SensorName="Sensor 4", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2)},
                new Sensor{IsDeleted = false, SensorCode="005", SensorName="Sensor 5", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3)},
                new Sensor{IsDeleted = false, SensorCode="006", SensorName="Sensor 6", Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3)}

            };
            foreach (var sensor in sensors)
            {
                context.Sensors.Add(sensor);
            }
            context.SaveChanges();

            var fans = new Fan[]
            {
                new Fan{IsDeleted = false, FanCode="001", FanName="Fan 1",IsOn=false,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Fan{IsDeleted = false, FanCode="002", FanName="Fan 2",IsOn=false,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Fan{IsDeleted = false, FanCode="003", FanName="Fan 3",IsOn=false,Capacity=0,Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2)},
            };
            foreach (var fan in fans)
            {
                context.Fans.Add(fan);
            }
            context.SaveChanges();

            var racks = new Rack[]
            {
                new Rack{IsDeleted = false, RackCode="001",RackName="Rack 1",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Rack{IsDeleted = false, RackCode="002",RackName="Rack 2",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Rack{IsDeleted = false, RackCode="003",RackName="Rack 3",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Rack{IsDeleted = false, RackCode="004",RackName="Rack 4",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Rack{IsDeleted = false, RackCode="005",RackName="Rack 5",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Rack{IsDeleted = false, RackCode="006",RackName="Rack 6",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3), Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            };
            foreach (var rack in racks)
            {
                context.Racks.Add(rack);
            }
            context.SaveChanges();


            var statuses = new Status[]
            {
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=44}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=40}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==1)},

                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==2)},
                
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==3)},
            
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=46}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==4)},

                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=59}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=17 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=59}, Temperature = new Temperature{IsDeleted=false, Value=18 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=19 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=53}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=48}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=47}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=44}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==5)},

                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:10:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:12:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:14:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:16:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:18:00"), Humidity = new Humidity{IsDeleted=false, Value=58}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:20:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:22:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=10 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:24:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:26:00"), Humidity = new Humidity{IsDeleted=false, Value=55}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:28:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:30:00"), Humidity = new Humidity{IsDeleted=false, Value=57}, Temperature = new Temperature{IsDeleted=false, Value=16 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:32:00"), Humidity = new Humidity{IsDeleted=false, Value=49}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:34:00"), Humidity = new Humidity{IsDeleted=false, Value=43}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:36:00"), Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:38:00"), Humidity = new Humidity{IsDeleted=false, Value=56}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:40:00"), Humidity = new Humidity{IsDeleted=false, Value=54}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:42:00"), Humidity = new Humidity{IsDeleted=false, Value=43}, Temperature = new Temperature{IsDeleted=false, Value=15 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:44:00"), Humidity = new Humidity{IsDeleted=false, Value=65}, Temperature = new Temperature{IsDeleted=false, Value=14 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:46:00"), Humidity = new Humidity{IsDeleted=false, Value=50}, Temperature = new Temperature{IsDeleted=false, Value=13 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:48:00"), Humidity = new Humidity{IsDeleted=false, Value=51}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
                new Status{IsDeleted = false, DateTime =DateTime.Parse("2018-7-7 23:50:00"), Humidity = new Humidity{IsDeleted=false, Value=52}, Temperature = new Temperature{IsDeleted=false, Value=11 }, Sensor = context.Sensors.FirstOrDefault(r=>r.SensorId==6)},
            };
            foreach (var status in statuses)
            {
                context.Statuses.Add(status);
            }
            context.SaveChanges();


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
