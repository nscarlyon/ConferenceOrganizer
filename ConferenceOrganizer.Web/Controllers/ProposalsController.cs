using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;

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
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Proposal proposal)
        {
            proposalsDomain.PostProposal(proposal);
            return new HttpResponseMessage("Proposal successfully submitted");
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            proposalsDomain.DeleteProposalById(id);
        }

        [HttpDelete]
        public void DeleteProposals()
        {
            proposalsDomain.DeleteProposals();
        }
    }
}
