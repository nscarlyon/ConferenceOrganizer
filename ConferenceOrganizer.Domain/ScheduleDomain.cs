using ConferenceOrganizer.Data;
using System.Collections.Generic;
using System;
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
            if(schedule != null)
            {
                var sortedTimeSlots = SortTimeSlots(schedule.TimeSlots);
                schedule.TimeSlots = sortedTimeSlots;
                schedule.Sessions = sessions;
                return schedule;
            }
            return null;
        }

        public List<TimeSlot> SortTimeSlots(List<TimeSlot> timeSlots)
        {
            var sortedTimeSlots = timeSlots.OrderBy(t => t.StartHour)
                                           .ThenBy(t => t.StartMin)
                                           .ToList<TimeSlot>();
            return sortedTimeSlots;
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

                else if (!schedule.TimeSlots.Exists(x => x.StandardTime == session.StandardTime)) 
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
