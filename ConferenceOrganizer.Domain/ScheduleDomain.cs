using ConferenceOrganizer.Data;
using ConferenceOrganizer.Data.MongoModels;
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

        public ScheduleResponse GetSchedule()
        {
            var mongoSessions = sessionsCollection.GetSessions();
            var mongoSchedule = scheduleCollection.GetSchedule();
            if(mongoSchedule != null)
            {
                var sortedTimeSlots = SortTimeSlots(mongoSchedule.TimeSlots);
                var sessionResponse = GetSessionResponse(mongoSessions);
                var scheduleResponse = new ScheduleResponse()
                {
                    Published = mongoSchedule.Published,
                    TimeSlots = sortedTimeSlots,
                    Sessions = sessionResponse,
                };

                return scheduleResponse;
            }
            return null;
        }

        private List<SessionResponse> GetSessionResponse(IEnumerable<MongoSession> mongoSessions)
        {
            var sessionResponse = mongoSessions.Select(s => new SessionResponse
            {
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

            return sessionResponse;
        }

        public List<TimeSlotResponse> SortTimeSlots(List<TimeSlot> timeSlots)
        {
            var timeSlotResponse = timeSlots.Select(t => new TimeSlotResponse {
                StandardTime = t.StandardTime,
                StartHour = t.StartHour,
                StartMin = t.StartMin,
                EndHour = t.EndHour,
                EndMin = t.EndMin
            });
            var sortedTimeSlots = timeSlotResponse.OrderBy(t => t.StartHour)
                                           .ThenBy(t => t.StartMin)
                                           .ToList();
            return sortedTimeSlots;
        }

        public void PostSchedule(MongoSchedule schedule)
        {
            scheduleCollection.PostSchedule(schedule);
        }

        public ScheduleResponse UpdateSchedule(string id, MongoSchedule schedule)
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
                if (!schedule.Rooms.Exists(x => x == session.Room) && session.Break == false)
                {
                    var proposal = proposalsCollection.FindProposal(session.ProposalId);
                    var scheduledTimes = proposal.scheduledTimes.Where(x => x.room != session.Room);
                    proposal.scheduledTimes = scheduledTimes.ToList();
                    proposalsCollection.UpdateProposal(proposal);
                    sessionsCollection.DeleteSession(session.id);
                }

                else if (!schedule.TimeSlots.Exists(x => x.StandardTime == session.StandardTime)) 
                {
                    var proposal = proposalsCollection.FindProposal(session.ProposalId);
                    if (proposal != null)
                    {
                        var scheduledTimes = proposal.scheduledTimes.Where(x => x.standardTime != session.StandardTime);
                        proposal.scheduledTimes = scheduledTimes.ToList();
                        proposalsCollection.UpdateProposal(proposal);
                    }
                    sessionsCollection.DeleteSession(session.id);
                }
            }
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
