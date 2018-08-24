using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MonitoringSystem.Resources
{
    public class UserResource
    {
        public UserResource()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Id { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
