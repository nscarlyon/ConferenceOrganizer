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
        public IEnumerable<MongoProposal> Get()
        {
            return proposalsDomain.GetProposals();
        }

        [HttpGet("{id}", Name = "Get")]
        public MongoProposal Get(string id)
        {
            return proposalsDomain.GetProposalById(id);
        }

        [HttpPut]
        public void Put([FromBody] MongoProposal proposal)
        {
            proposalsDomain.UpdateProposal(proposal);
        }
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody]MongoProposal proposal)
        {
            proposalsDomain.PostProposal(proposal);
            return new HttpResponseMessage("Proposal successfully submitted");
        }
        
        [HttpDelete("{id}")]
        public IEnumerable<MongoProposal> Delete(string id)
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
