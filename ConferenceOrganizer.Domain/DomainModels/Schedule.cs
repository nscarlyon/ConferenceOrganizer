using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Domain.DomainModels
{
   public class Schedule
    {
        public string id { get; set; }
        public bool Published { get; set; }
        public List<string> Rooms { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }

    public class TimeSlot
    {
        public string StandardTime { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }

        public override bool Equals(object obj)
        {
            var slot = obj as TimeSlot;
            return slot != null &&
                   StandardTime == slot.StandardTime &&
                   StartHour == slot.StartHour &&
                   StartMin == slot.StartMin &&
                   EndHour == slot.EndHour &&
                   EndMin == slot.EndMin;
        }
    }

    public class Session
    {
        public string id { get; set; }
        public bool Break { get; set; }
        public string SpeakerName { get; set; }
        public string Email { get; set; }
        public string ProposalId { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string StandardTime { get; set; }
    }
}
