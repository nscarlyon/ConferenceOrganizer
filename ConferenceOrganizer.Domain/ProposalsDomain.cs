using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class ProposalsDomain
    {
        public IProposalsCollection proposalsCollection;
        public ISessionsCollection sessionsCollection;

        public ProposalsDomain(IProposalsCollection proposalsCollection, ISessionsCollection sessionsCollection)
        {
            this.proposalsCollection = proposalsCollection;
            this.sessionsCollection = sessionsCollection;
        }

        public IEnumerable<Proposal> GetProposals()
        {
            var mongoProposals = proposalsCollection.GetProposals();

            var proposals =  mongoProposals.Select(proposal =>
            {
                return new Proposal
                {
                    Id = proposal.id,
                    SpeakerName = proposal.SpeakerName,
                    Bio = proposal.Bio,
                    Title = proposal.Title,
                    Description = proposal.Description,
                    Email = proposal.Email,
                    ScheduledSessions = GetScheduledSessions(proposal)
                };
            });

            return proposals;
        }

        public IEnumerable<ScheduledSession> GetScheduledSessions(MongoProposal proposal)
        {
            var filteredSessions = sessionsCollection.GetSessions().Where(s => s.ProposalId == proposal.id);
            return filteredSessions.Select(s => new ScheduledSession
            {
                SessionId = s.id,
                StandardTime = s.StandardTime,
                Room = s.Room
            });
        }

        public Proposal GetProposalById(string id)
        {
            var proposal = proposalsCollection.GetProposalById(id);
            return new Proposal
            {
                Id = proposal.id,
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
                id = proposal.Id,
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
