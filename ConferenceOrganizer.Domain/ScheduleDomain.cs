using ConferenceOrganizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class ScheduleDomain
    {
        public IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public ScheduleDomain(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        public Schedule GetSchedule()
        {
            var sessions = conferenceOrganizerDatabase.GetSessions();
            var schedule = conferenceOrganizerDatabase.GetSchedule();
            var orderedSchedule = new OrderedSchedule(schedule, sessions);

            return conferenceOrganizerDatabase.GetSchedule();
        }

        public void PostSchedule(Schedule schedule)
        {
            conferenceOrganizerDatabase.PostSchedule(schedule);
        }

        public void DeleteSchedule()
        {
            conferenceOrganizerDatabase.DeleteSchedule();
        }

    }
}
