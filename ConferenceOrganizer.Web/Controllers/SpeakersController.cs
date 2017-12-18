using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{

    [Produces("application/json")]
    [Route("speakers/proposals")]
    public class SpeakersController : Controller
    {
        ISpeakersApi speakersApi;

        public SpeakersController(ISpeakersApi speakersApi)
        {
            this.speakersApi = speakersApi;
        }

        [HttpGet]
        public IEnumerable<Proposal> Get()
        {
            return speakersApi.GetProposals();
        }

        [HttpGet("{id}", Name = "Get")]
        public Proposal Get(string id)
        {
            return speakersApi.FindProposal(id);
        }

        [HttpPost]
        public PostResponseMessage Post([FromBody]Proposal proposal)
        {
            speakersApi.PostProposal(proposal);
            return new PostResponseMessage("Proposal successfully submitted");
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            speakersApi.DeleteProposal(id);
        }

        [HttpDelete]
        public void DeleteProposals()
        {
            speakersApi.DeleteProposals();
        }
    }
}
