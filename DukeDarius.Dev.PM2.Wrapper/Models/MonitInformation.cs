using Newtonsoft.Json;

namespace DukeDarius.Dev.PM2.Wrapper.Models
{
    public class MonitInformation
    {
        [JsonProperty("memory")]
        public long MemoryUsage { get; set; }

        [JsonProperty("cpu")]
        public double CpuUsage { get; set; }
    }
}