﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringSystem.Resources
{
    public class QueryResource
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int? Page { get; set; }
        public byte PageSize { get; set; }
        public int? SensorId { get; set; }
        public int? RackId { get; set; }
        public int? RoomId { get; set; }
    }
}
