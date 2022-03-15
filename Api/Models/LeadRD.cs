using System;

namespace Api.Models
{
    public class LeadRD
    {
        public int Id { get; set; }
        public LeadForm LeadForm { get; set; }
        public DateTime DataEntrada { get; set; }
        public string EventType { get; set; }
        public string EventFamily { get; set; }
        public string ConversionIdentifier { get; set; }
        public string ClientTrackingId { get; set; }
        public string TrafficSource { get; set; }
        public string TrafficMedium { get; set; }
        public string TrafficCampaign { get; set; }
        public string TrafficValue { get; set; }

    }
}
