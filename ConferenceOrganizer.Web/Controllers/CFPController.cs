using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;
using Microsoft.AspNetCore.Cors;

namespace ConferenceOrganizer.Web.Controllers
{
    [Route("cfp")]
    [EnableCors("AllowSpecificOrigin")]
    public class CFPController : Controller
    {
        IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public CFPController(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        [HttpGet]
        public string Get()
        {
            return conferenceOrganizerDatabase.GetCFPStatus();
        }    
        
        // PUT: api/CFP/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
