using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("proposals")]
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
        public PostResponseMessage Post([FromBody]Proposal proposal)
        {
            conferenceOrganizerDatabase.PostProposal(proposal);
            return new PostResponseMessage("Proposal successfully submitted");
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            conferenceOrganizerDatabase.DeleteProposal(id);
        }

        [HttpDelete]
        public void DeleteProposals()
        {
            conferenceOrganizerDatabase.DeleteProposals();
        }
    }
}
