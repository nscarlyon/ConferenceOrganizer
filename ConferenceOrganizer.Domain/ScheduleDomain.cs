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
        public IProposalsCollection proposalsCollection;

        public ScheduleDomain(IScheduleCollection scheduleCollection, ISessionsCollection sessionsCollection, IProposalsCollection proposalsCollection)
        {
            this.scheduleCollection = scheduleCollection;
            this.sessionsCollection = sessionsCollection;
            this.proposalsCollection = proposalsCollection;
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
            UpdateSessionsAndProposals(schedule);
            return GetSchedule();
        }

        public void UpdateSessionsAndProposals(MongoSchedule schedule)
        {
            var sessions = sessionsCollection.GetSessions();

            foreach (var session in sessions)
            {
                if (RoomIsDeleted(schedule, session) || TimeSlotDeleted(schedule, session))
                {
                    UpdateProposal(session);
                    sessionsCollection.DeleteSession(session.id);
                }
            }
        }

        private void UpdateProposal(MongoSession session)
        {
            var proposal = proposalsCollection.GetProposalById(session.ProposalId);

            if (IsNotBreak(proposal))
            {
                var scheduledTimes = proposal.ScheduledTimes.Where(x => x.StandardTime != session.StandardTime);
                proposal.ScheduledTimes = scheduledTimes.ToList();
                proposalsCollection.UpdateProposal(proposal);
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
            scheduleCollection.DeleteSchedule();
        }
    }
}
