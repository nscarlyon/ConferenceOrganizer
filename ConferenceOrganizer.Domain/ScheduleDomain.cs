using ConferenceOrganizer.Data;
using System.Collections.Generic;

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
            schedule.Sessions = sessions;
            return schedule;
        }

        public void PostSchedule(Schedule schedule)
        {
            conferenceOrganizerDatabase.PostSchedule(schedule);
        }

        public void UpdateSchedule(string id, Schedule schedule)
        {
            conferenceOrganizerDatabase.PutSchedule(id, schedule);
        }

        public void DeleteSchedule()
        {
            conferenceOrganizerDatabase.DeleteSchedule();
        }
    }
}
