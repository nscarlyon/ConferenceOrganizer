using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class ProposalsDomain
    {
        private ProposalsCollection proposalsCollection;
        private SessionsCollection sessionsCollection;

        public ProposalsDomain()
        {
            proposalsCollection = new ProposalsCollection();
            sessionsCollection = new SessionsCollection();
        }

        public IEnumerable<MongoProposal> GetProposals()
        {
            return proposalsCollection.GetProposals();
        }

        public MongoProposal GetProposalById(string id)
        {
            return proposalsCollection.GetProposalById(id);
        }

        public void PostProposal(MongoProposal proposal)
        {
            proposalsCollection.PostProposal(proposal);
        }

        public void UpdateProposal(MongoProposal proposal)
        {
            proposalsCollection.UpdateProposal(proposal);
        }

        public IEnumerable<MongoProposal> DeleteProposalById(string id)
        {
            DeleteScheduledSessions(id);
            proposalsCollection.DeleteProposal(id);
            return GetProposals();
        }

        private void DeleteScheduledSessions(string id)
        {
            var proposal = GetProposalById(id);
            if (proposal.ScheduledTimes != null)
            {
                proposal.ScheduledTimes.ForEach(scheduledTime =>
                {
                    sessionsCollection.DeleteSession(scheduledTime.SessionId);
                });
            }
        }

        public void DeleteProposals()
        {
            proposalsCollection.DeleteProposals();
        }
    }
}
