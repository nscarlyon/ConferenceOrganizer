using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("Proposals")]
    public class ProposalsController : Controller
    {
        IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public ProposalsController(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        [HttpGet]
        public IEnumerable<Proposal> Get()
        {
            return conferenceOrganizerDatabase.GetProposals();
        }

        [HttpGet("{id}", Name = "Get")]
        public Proposal Get(string id)
        {
            return conferenceOrganizerDatabase.FindProposal(id);
        }
        
        [HttpPost]
        public void Post([FromBody]Proposal proposal)
        {
            conferenceOrganizerDatabase.PostProposal(proposal);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Proposal value)
        {
            conferenceOrganizerDatabase.PutProposal(id, value);
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            conferenceOrganizerDatabase.DeleteProposal(id);
        }
    }
}
