using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;

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
            var proposals = proposalsCollection.GetProposals();

            return proposals.Select(proposal =>
            {
                return new Proposal
                {
                    id = proposal.id,
                    SpeakerName = proposal.SpeakerName,
                    Bio = proposal.Bio,
                    Title = proposal.Title,
                    Description = proposal.Description,
                    Email = proposal.Email
                };
            });
            
        }

        public Proposal GetProposalById(string id)
        {
            var proposal = proposalsCollection.GetProposalById(id);
            return new Proposal
            {
                id = proposal.id,
                SpeakerName = proposal.SpeakerName,
                Bio = proposal.Bio,
                Title = proposal.Title,
                Description = proposal.Description,
                Email = proposal.Email
            };
        }

        public void PostProposal(Proposal proposal)
        {
            var mongoProposal = new MongoProposal
            {
                SpeakerName = proposal.SpeakerName,
                Bio = proposal.Bio,
                Title = proposal.Title,
                Description = proposal.Description,
                Email = proposal.Email
            };

            proposalsCollection.PostProposal(mongoProposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            var mongoProposal = new MongoProposal
            {
                id = proposal.id,
                SpeakerName = proposal.SpeakerName,
                Bio = proposal.Bio,
                Title = proposal.Title,
                Description = proposal.Description,
                Email = proposal.Email
            };

            proposalsCollection.UpdateProposal(mongoProposal);
        }

        public IEnumerable<Proposal> DeleteProposalById(string id)
        {
            proposalsCollection.DeleteProposal(id);
            return GetProposals();
        }

        public void DeleteProposals()
        {
            proposalsCollection.DeleteProposals();
        }
    }
}
