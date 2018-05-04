using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class ScheduleDomain
    {
        public IScheduleCollection scheduleCollection;
        public ISessionsCollection sessionsCollection;

        public ScheduleDomain(IScheduleCollection scheduleCollection, ISessionsCollection sessionsCollection)
        {
            this.scheduleCollection = scheduleCollection;
            this.sessionsCollection = sessionsCollection;
        }

        public Schedule GetSchedule()
        {
            var mongoSchedule = scheduleCollection.GetSchedule();

            if(mongoSchedule != null)
            {
                var schedule = new Schedule()
                {
                    id = mongoSchedule.id,
                    Published = mongoSchedule.Published,
                    Rooms = mongoSchedule.Rooms,
                    TimeSlots = GetSortedTimeSlots(mongoSchedule.TimeSlots),
                    Sessions = GetSessions()
                };

                return schedule;
            }
            return null;
        }

        public List<TimeSlot> GetSortedTimeSlots(List<MongoTimeSlot> mongoTimeSlots)
        {
            var timeSlots = mongoTimeSlots.Select(t => new TimeSlot
            {
                StandardTime = t.StandardTime,
                StartHour = t.StartHour,
                StartMin = t.StartMin,
                EndHour = t.EndHour,
                EndMin = t.EndMin
            });

            var sortedTimeSlots = timeSlots.OrderBy(t => t.StartHour)
                                           .ThenBy(t => t.StartMin)
                                           .ThenBy(t => t.EndHour)
                                           .ThenBy(t => t.EndMin).ToList();
            return sortedTimeSlots;
        }

        public List<Session> GetSessions()
        {
            var mongoSessions = sessionsCollection.GetSessions();
            var sessions = mongoSessions.Select(s => new Session
            {
                id = s.id,
                Bio = s.Bio,
                Break = s.Break,
                SpeakerName = s.SpeakerName,
                Email = s.Email,
                ProposalId = s.ProposalId,
                Title = s.Title,
                Description = s.Description,
                Room = s.Room,
                StandardTime = s.StandardTime
            }).ToList();

            return sessions;
        }

        public void PostSchedule(Schedule schedule)
        {
            var mongoSchedule = GetMongoSchedule(schedule);
            scheduleCollection.PostSchedule(mongoSchedule);
        }

        public MongoSchedule GetMongoSchedule(Schedule schedule)
        {
            return  new MongoSchedule
            {
                Published = schedule.Published,
                Rooms = schedule.Rooms,
                TimeSlots = GetSortedMongoTimeSlots(schedule.TimeSlots)
            };
        }

        public List<MongoTimeSlot> GetSortedMongoTimeSlots(List<TimeSlot> timeSlots)
        {
            var mongoTimeSlots = timeSlots.Select(timeSlot => new MongoTimeSlot
            {
                StandardTime = timeSlot.StandardTime,
                StartHour = timeSlot.StartHour,
                StartMin = timeSlot.StartMin,
                EndHour = timeSlot.EndHour,
                EndMin = timeSlot.EndMin
            }).ToList();

            var sortedTimeSlots = mongoTimeSlots.OrderBy(t => t.StartHour)
                                          .ThenBy(t => t.StartMin)
                                          .ThenBy(t => t.EndHour)
                                          .ThenBy(t => t.EndMin).ToList();
            return sortedTimeSlots;
        }

        public Schedule UpdateSchedule(string id, Schedule schedule)
        {
            var mongoSchedule = GetMongoSchedule(schedule);
            scheduleCollection.PutSchedule(id, mongoSchedule);
            UpdateSessions(schedule);
            return GetSchedule();
        }

        public void UpdateSessions(Schedule schedule)
        {
            var sessions = sessionsCollection.GetSessions();

            foreach (var session in sessions)
            {
                if (RoomIsDeleted(schedule, session) || TimeSlotDeleted(schedule, session))
                {
                    sessionsCollection.DeleteSession(session.id);
                }
            }
        }

        private static bool TimeSlotDeleted(Schedule schedule, MongoSession session)
        {
            return !schedule.TimeSlots.Exists(x => x.StandardTime == session.StandardTime);
        }

        private static bool RoomIsDeleted(Schedule schedule, MongoSession session)
        {
            return !schedule.Rooms.Exists(x => x == session.Room) && session.Break == false;
        }

        public void PublishSchedule()
        {
            scheduleCollection.PublishSchedule();
        }

        public void UnpublishSchedule()
        {
            scheduleCollection.UnpublishSchedule();
        }

        public void DeleteSchedule()
        {
            var schedule = GetSchedule();
            DeleteScheduleSessions(schedule);
            ClearSchedule(schedule);
        }

        public void DeleteScheduleSessions(Schedule schedule)
        {
            schedule.Sessions.ForEach(s =>
            {
                sessionsCollection.DeleteSession(s.id);
            });
        }

        public void ClearSchedule(Schedule schedule)
        {
            var newSchedule = GetMongoSchedule(schedule);
            scheduleCollection.PutSchedule(schedule.id, newSchedule);
        }
    }
}
