using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Domain;
using ConferenceOrganizer.Domain.DomainModels;

namespace ConferenceOrganizer.Web.Controllers
{
    [Route("cfp")]
    public class CFPController : Controller
    {
        CFPDomain cfpDomain;

        public CFPController(CFPDomain cfpDomain)
        {
            this.cfpDomain = cfpDomain;
        }

        [HttpGet]
        public CFP Get()
        {
            return cfpDomain.GetCfp();
        }    
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]CFP value)
        {
            cfpDomain.PutCfp(id, value);
        }
    }
}
