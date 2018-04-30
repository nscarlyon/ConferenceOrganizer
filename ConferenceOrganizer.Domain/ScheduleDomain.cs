using ConferenceOrganizer.Data;
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
            var sessions = sessionsCollection.GetSessions();
            var schedule = scheduleCollection.GetSchedule();
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
            scheduleCollection.PostSchedule(schedule);
        }

        public Schedule UpdateSchedule(string id, Schedule schedule)
        {
            scheduleCollection.PutSchedule(id, schedule);
            UpdateSessionsAndProposals(schedule);
            return GetSchedule();
        }

        public void UpdateSessionsAndProposals(Schedule schedule)
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
