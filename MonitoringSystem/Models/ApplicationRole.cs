using System;
using Microsoft.AspNetCore.Identity;

namespace MonitoringSystem.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}