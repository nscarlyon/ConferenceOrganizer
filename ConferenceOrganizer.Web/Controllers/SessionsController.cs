using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("sessions")]
    public class SessionsController : Controller
    {
        private SessionsDomain sessionsDomain;

        public SessionsController(SessionsDomain sessionDomain)
        {
            this.sessionsDomain = sessionDomain;
        }

        [HttpGet]
        public IEnumerable<Session> Get()
        {
            return sessionsDomain.GetSessions();
        }

        [HttpGet("{id}")]
        public Session Get(string id)
        {
            return sessionsDomain.GetSessionById(id);
        }

        [HttpPost]
        public PostResponseMessage Post([FromBody]Session session)
        {
            sessionsDomain.PostSession(session);
            return new PostResponseMessage("Session successfully added");
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Session session)
        {
            sessionsDomain.PutSession(id, session);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            sessionsDomain.DeleteSessionById(id);
        }

        [HttpDelete]
        public void DeleteSessions()
        {
            sessionsDomain.DeleteSessions();
        }
    }
}
