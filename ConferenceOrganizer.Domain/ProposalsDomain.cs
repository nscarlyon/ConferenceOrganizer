using ConferenceOrganizer.Data;
using System.Collections.Generic;

namespace ConferenceOrganizer.Domain
{
    public class ProposalsDomain
    {
        private ProposalsCollection proposalsCollection;

        public ProposalsDomain()
        {
            proposalsCollection = new ProposalsCollection(); 
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return proposalsCollection.GetProposals();
        }

        public Proposal GetProposalById(string id)
        {
            return proposalsCollection.FindProposal(id);
        }

        public void PostProposal(Proposal proposal)
        {
            proposalsCollection.PostProposal(proposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            proposalsCollection.UpdateProposal(proposal);
        }

        public void DeleteProposalById(string id)
        {
            proposalsCollection.DeleteProposal(id);
        }

        public void DeleteProposals()
        {
            proposalsCollection.DeleteProposals();
        }
    }
}
