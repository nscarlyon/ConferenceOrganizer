using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class ProposalsDomain
    {
        private ConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public ProposalsDomain()
        {
            conferenceOrganizerDatabase = new ConferenceOrganizerDatabase(); 
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return conferenceOrganizerDatabase.GetProposals();
        }

        public Proposal GetProposalById(string id)
        {
            return conferenceOrganizerDatabase.FindProposal(id);
        }

        public void PostProposal(Proposal proposal)
        {
            conferenceOrganizerDatabase.PostProposal(proposal);
        }

        public void DeleteProposalById(string id)
        {
            conferenceOrganizerDatabase.DeleteProposal(id);
        }

        public void DeleteProposals()
        {
            conferenceOrganizerDatabase.DeleteProposals();
        }
    }
}
