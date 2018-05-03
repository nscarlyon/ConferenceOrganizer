﻿using ConferenceOrganizer.Data;
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
                var sessions = GetSessions();
                var sortedTimeSlots = GetSortedTimeSlots(mongoSchedule.TimeSlots);

                var schedule = new Schedule()
                {
                    id = mongoSchedule.id,
                    Published = mongoSchedule.Published,
                    Rooms = mongoSchedule.Rooms,
                    TimeSlots = sortedTimeSlots,
                    Sessions = sessions
                };

                return schedule;

            }
            return null;
        }

        private List<Session> GetSessions()
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
                                           .ToList();
            return sortedTimeSlots;
        }

        public void PostSchedule(MongoSchedule schedule)
        {
            scheduleCollection.PostSchedule(schedule);
        }

        public Schedule UpdateSchedule(string id, MongoSchedule schedule)
        {
            scheduleCollection.PutSchedule(id, schedule);
            UpdateSessions(schedule);
            return GetSchedule();
        }

        public void UpdateSessions(MongoSchedule schedule)
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

        private static bool IsNotBreak(MongoProposal proposal)
        {
            return proposal != null;
        }

        private static bool TimeSlotDeleted(MongoSchedule schedule, MongoSession session)
        {
            return !schedule.TimeSlots.Exists(x => x.StandardTime == session.StandardTime);
        }

        private static bool RoomIsDeleted(MongoSchedule schedule, MongoSession session)
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
            DeleteScheduleSessions();
            scheduleCollection.DeleteSchedule();
            CreateNewSchedule();
        }

        private void DeleteScheduleSessions()
        {
            var schedule = GetSchedule();
            schedule.Sessions.ForEach(s =>
            {
                sessionsCollection.DeleteSession(s.id);
            });
        }

        private void CreateNewSchedule()
        {
            var newSchedule = new MongoSchedule
            {
                Published = false,
                TimeSlots = new List<MongoTimeSlot>(),
                Rooms = new List<string>()
            };
            scheduleCollection.PostSchedule(newSchedule);
        }
    }
}
