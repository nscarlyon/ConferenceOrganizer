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

        [HttpPut("rooms/{id}")]
        public void SetRooms(string id, [FromBody] Rooms rooms)
        {
            scheduleDomain.SetScheduleRooms(id, rooms);
        }

        [HttpPut("timeslots/{id}")]
        public HttpResponseMessage SetTimeSlot(string id, [FromBody] TimeSlot timeSlot)
        {
            return scheduleDomain.SetTimeSlot(id, timeSlot);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("timeslots")]
        public HttpResponseMessage DeleteTimeSlot([FromBody] string timeSlot)
        {
            return scheduleDomain.DeleteTimeSlot(timeSlot);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public void Delete()
        {
            scheduleDomain.DeleteSchedule();
        }
    }
}
