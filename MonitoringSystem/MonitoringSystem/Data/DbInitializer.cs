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
                new Rack{IsDeleted = false, RackCode="001",RackName="Rack 1",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Rack{IsDeleted = false, RackCode="002",RackName="Rack 2",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==1)},
                new Rack{IsDeleted = false, RackCode="003",RackName="Rack 3",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2)},
                new Rack{IsDeleted = false, RackCode="004",RackName="Rack 4",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==2)},
                new Rack{IsDeleted = false, RackCode="005",RackName="Rack 5",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3)},
                new Rack{IsDeleted = false, RackCode="006",RackName="Rack 6",Room = context.Rooms.FirstOrDefault(r=>r.RoomId==3)},
            };
            foreach (var rack in racks)
            {
                context.Racks.Add(rack);
            }
            context.SaveChanges();

            var statuses = new Status[]
            {
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=80}, Temperature = new Temperature{IsDeleted=false, Value=90 }, Room = context.Rooms.FirstOrDefault(r => r.RoomId ==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=76}, Temperature = new Temperature{IsDeleted=false, Value=56 }, Room = context.Rooms.FirstOrDefault(r => r.RoomId ==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=90}, Temperature = new Temperature{IsDeleted=false, Value=48 }, Room = context.Rooms.FirstOrDefault(r => r.RoomId ==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=89 }, Room = context.Rooms.FirstOrDefault(r => r.RoomId ==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=87}, Temperature = new Temperature{IsDeleted=false, Value=98 }, Rack = context.Racks.FirstOrDefault(r => r.RackId ==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=67}, Temperature = new Temperature{IsDeleted=false, Value=34 }, Rack = context.Racks.FirstOrDefault(r => r.RackId ==1)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=45}, Temperature = new Temperature{IsDeleted=false, Value=54 }, Rack = context.Racks.FirstOrDefault(r => r.RackId ==2)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=23}, Temperature = new Temperature{IsDeleted=false, Value=23 }, Rack = context.Racks.FirstOrDefault(r => r.RackId ==3)},
                new Status{IsDeleted = false, DateTime =DateTime.Now, Humidity = new Humidity{IsDeleted=false, Value=98}, Temperature = new Temperature{IsDeleted=false, Value=12 }, Rack = context.Racks.FirstOrDefault(r => r.RackId ==3)},
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
