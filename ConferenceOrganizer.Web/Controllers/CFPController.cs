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

        //[HttpGet]
        //public CFP Get()
        //{
        //    Request.Headers.Add("Access-Control-Allow-Origin", "*");
        //    return conferenceOrganizerDatabase.GetCFPStatus();
        //}    
        
        //[HttpPut("{id}")]
        //public void Put(string id, [FromBody]CFP value)
        //{
        //    conferenceOrganizerDatabase.PutCFP(id, value);
        //}
    }
}
