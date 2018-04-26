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

        public Schedule GetRoughSchedule()
        {
            var sessions = conferenceOrganizerDatabase.GetSessions();
            var schedule = conferenceOrganizerDatabase.GetRoughSchedule();
            if(schedule != null)
            {
                schedule.Sessions = sessions;
                return schedule;
            }
            return null;
        }

        public Schedule GetPublishedSchedule()
        {
            var sessions = conferenceOrganizerDatabase.GetSessions();
            var schedule = conferenceOrganizerDatabase.GetPublishedSchedule();
            if (schedule != null)
            {
                schedule.Sessions = sessions;
                return schedule;
            }
            return null;
        }

        public void PostSchedule(Schedule schedule)
        {
            conferenceOrganizerDatabase.PostSchedule(schedule);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            conferenceOrganizerDatabase.PutSchedule(schedule);
        }

        public void DeleteSchedule()
        {
            conferenceOrganizerDatabase.DeleteSchedule();
        }

        public void PublishSchedule()
        {
            conferenceOrganizerDatabase.PublishSchedule();
        }

        public void UnpublishSchedule()
        {
            conferenceOrganizerDatabase.UnpublishSchedule();
        }
    }
}
