using ConferenceOrganizer.Data;
using System.Collections.Generic;
using System;

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
            if(schedule != null)
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

        public Schedule UpdateSchedule(string id, Schedule schedule)
        {
            conferenceOrganizerDatabase.PutSchedule(id, schedule);
            UpdateSessions(schedule);
            return GetSchedule();
        }

        public void UpdateSessions(Schedule schedule)
        {
            var sessions = conferenceOrganizerDatabase.GetSessions();
            foreach (var session in sessions)
            {
                if (!schedule.Rooms.Exists(x => x == session.Room))
                {
                    conferenceOrganizerDatabase.DeleteSession(session.id);
                }
            }
        }

        public void PublishSchedule()
        {
            conferenceOrganizerDatabase.PublishSchedule();
        }

        public void UnpublishSchedule()
        {
            conferenceOrganizerDatabase.UnpublishSchedule();
        }

        public void DeleteSchedule()
        {
            conferenceOrganizerDatabase.DeleteSchedule();
        }
    }
}
