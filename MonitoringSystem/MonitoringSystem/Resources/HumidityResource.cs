namespace MonitoringSystem.Resources
{
    public class HumidityResource
    {
        public int HumidityId { get; set; }
        public double Value { get; set; }
        public bool IsDeleted { get; set; }
        public int? StatusId { get; set; }
        public StatusResource Status { get; set; }
    }
}
