using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;
using Microsoft.AspNetCore.Cors;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("admin/sessions")]
    [EnableCors("AllowSpecificOrigin")]

    public class SessionsController : Controller
    {
        IAdminApi adminApi;

        public SessionsController(IAdminApi adminApi)
        {
            this.adminApi = adminApi;
        }

        [Route("cfp")]
        [HttpGet]
        public CFP GetCFPStatus()
        {
            Request.Headers.Add("Access-Control-Allow-Origin", "*");
            return adminApi.GetCFPStatus();
        }

        [Route("cfp")]
        [HttpPut("{id}")]
        public void PutCFPStatus(string id, [FromBody]CFP value)
        {
            adminApi.PutCFP(id, value);
        }

        [Route("speakers")]
        [HttpGet]
        public IEnumerable<string> GetSpeakers()
        {
            return adminApi.GetSpeakers();
        }

        [HttpGet]
        public IEnumerable<Session> Get()
        {
            return adminApi.GetSessions();
        }

        [HttpGet("{id}")]
        public Session Get(string id)
        {
            return adminApi.GetSession(id);
        }

        [HttpPost]
        public PostResponseMessage Post([FromBody]Session session)
        {
            adminApi.PostSession(session);
            return new PostResponseMessage("Session successfully added");
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Session session)
        {
            adminApi.PutSession(id, session);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            adminApi.DeleteSession(id);
        }
    }
}
