﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Models
{
    public class Fan
    {
        public int FanId { get; set; }
        public string FanCode { get; set; }
        public string FanName { get; set; }
        public bool IsOn { get; set; }
        public int Capacity { get; set; }
        public bool IsDeleted { get; set; }
        public Room Room { get; set; }
        public ICollection<Log> Logs { get; set; }
        public ICollection<FanStatus> FanStatuses { get; set; }
        public Fan()
        {
            IsDeleted = false;
            Logs = new Collection<Log>();
            FanStatuses = new Collection<FanStatus>();
        }
    }
}
