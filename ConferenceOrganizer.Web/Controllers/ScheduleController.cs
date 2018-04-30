using Microsoft.AspNetCore.Mvc;
using ConferenceOrganizer.Domain;
using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Web.Controllers
{
    [Produces("application/json")]
    [Route("Schedule")]
    public class ScheduleController : Controller
    {
        private ScheduleDomain scheduleDomain;

        public ScheduleController(ScheduleDomain scheduleDomain)
        {
            this.scheduleDomain = scheduleDomain;
        }

        // GET: api/Schedule
        [HttpGet]
        public Schedule Get()
        {
            return scheduleDomain.GetSchedule();
        }
        
        // POST: api/Schedule
        [HttpPost]
        public void Post([FromBody]Schedule schedule)
        {
            scheduleDomain.PostSchedule(schedule);
        }

        [HttpPut("{id}")]
        public Schedule Put(string id, [FromBody] Schedule schedule)
        {
            return scheduleDomain.UpdateSchedule(id, schedule);
        }

        [HttpPut("publish")]
        public void PublishSchedule()
        {
            scheduleDomain.PublishSchedule();
        }

        [HttpPut("unpublish")]
        public void UnpublishSchedule()
        {
            scheduleDomain.UnpublishSchedule();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete()
        {
            scheduleDomain.DeleteSchedule();
        }
    }
}
