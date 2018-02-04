using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class ScheduleDomain
    {
        private ConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public ScheduleDomain()
        {
            conferenceOrganizerDatabase = new ConferenceOrganizerDatabase();
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
    }
}
