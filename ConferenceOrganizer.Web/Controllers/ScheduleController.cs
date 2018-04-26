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
        [HttpGet("published")]
        public Schedule GetRoughSchedule()
        {
            return scheduleDomain.GetPublishedSchedule();
        }

        [HttpGet("rough")]
        public Schedule GetPublishedSchedule()
        {
            return scheduleDomain.GetRoughSchedule();
        }

        // POST: api/Schedule
        [HttpPost]
        public void Post([FromBody]Schedule schedule)
        {
            scheduleDomain.PostSchedule(schedule);
        }

        [HttpPut("{id}")]
        public void Put([FromBody] Schedule schedule)
        {
            scheduleDomain.UpdateSchedule(schedule);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete()
        {
            scheduleDomain.DeleteSchedule();
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
    }
}
