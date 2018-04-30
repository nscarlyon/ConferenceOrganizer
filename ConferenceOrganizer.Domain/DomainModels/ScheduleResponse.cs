using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Domain.DomainModels
{
    public class ScheduleResponse
    {
        public List<string> Rooms { get; set; }
        public List<TimeSlotResponse> TimeSlots { get; set; }
        public bool Published { get; set; }
        public IEnumerable<SessionResponse> Sessions { get; set; }
    }

    public class TimeSlotResponse
    {
        public string StandardTime { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
    }
}
