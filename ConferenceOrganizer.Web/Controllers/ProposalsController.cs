using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Domain;
using ConferenceOrganizer.Domain.DomainModels;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("proposals")]
    public class ProposalsController : Controller
    {
        ProposalsDomain proposalsDomain;

        public ProposalsController(ProposalsDomain proposalsDomain)
        {
            this.proposalsDomain = proposalsDomain;
        }

        [HttpGet]
        public IEnumerable<Proposal> Get()
        {
            return proposalsDomain.GetProposals();
        }

        [HttpGet("{id}", Name = "Get")]
        public Proposal Get(string id)
        {
            return proposalsDomain.GetProposalById(id);
        }

        [HttpPut]
        public void Put([FromBody] Proposal proposal)
        {
            proposalsDomain.UpdateProposal(proposal);
        }
        
        [HttpPost]
        public void Post([FromBody]Proposal proposal)
        {
            proposalsDomain.PostProposal(proposal);
        }
        
        [HttpDelete("{id}")]
        public IEnumerable<Proposal> Delete(string id)
        {
            return proposalsDomain.DeleteProposalById(id);
        }

        [HttpDelete]
        public void DeleteProposals()
        {
            proposalsDomain.DeleteProposals();
        }
    }
}
