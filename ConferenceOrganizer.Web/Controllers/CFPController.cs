using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Data;
using Microsoft.AspNetCore.Cors;
using ConferenceOrganizer.Domain;

namespace ConferenceOrganizer.Web.Controllers
{
    [Route("cfp")]
    [EnableCors("AllowSpecificOrigin")]
    public class CFPController : Controller
    {
        CFPDomain cfpDomain;

        public CFPController(CFPDomain cfpDomain)
        {
            this.cfpDomain = cfpDomain;
        }

        [HttpGet]
        public MongoCFP Get()
        {
            Request.Headers.Add("Access-Control-Allow-Origin", "*");
            return cfpDomain.GetCfp();
        }    
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]MongoCFP value)
        {
            cfpDomain.PutCfp(id, value);
        }
    }
}
