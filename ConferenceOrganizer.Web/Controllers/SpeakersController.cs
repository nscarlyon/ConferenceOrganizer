using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{

    [Route("[controller]")]
    public class SpeakersController : Controller
    {
        IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public SpeakersController(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return conferenceOrganizerDatabase.GetSpeakers();
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public IEnumerable<Proposal> Get(string name)
        {
            return conferenceOrganizerDatabase.GetProposalsBySpeaker(name);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
