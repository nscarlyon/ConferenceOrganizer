using ConferenceOrganizer.Data;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class ScheduleDomain
    {
        public IConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public ScheduleDomain(IConferenceOrganizerDatabase conferenceOrganizerDatabase)
        {
            this.conferenceOrganizerDatabase = conferenceOrganizerDatabase;
        }

        public Schedule GetSchedule()
        {
            var sessions = conferenceOrganizerDatabase.GetSessions();
            var schedule = conferenceOrganizerDatabase.GetSchedule();
            var orderedSchedule = new OrderedSchedule(schedule, sessions).GetOrderedSchedule();
            return orderedSchedule;
        }

        public void PostSchedule(Schedule schedule)
        {
            conferenceOrganizerDatabase.PostSchedule(schedule);
        }

        public void DeleteSchedule()
        {
            conferenceOrganizerDatabase.DeleteSchedule();
        }

        public HttpResponseMessage SetTimeSlot(string id, TimeSlot timeSlot)
        {
            var schedule = GetSchedule();
            var timeSlots = schedule.timeSlots.Where(t => t.timeSlot == timeSlot.timeSlot);
            if(timeSlots.Count() > 0) return new HttpResponseMessage("This time slot already exists");
            if(timeSlots.Count() == 0)
            {
                foreach (var room in schedule.rooms)
                {
                    Session session = GetSession(timeSlot, room);
                    conferenceOrganizerDatabase.PostSession(session);
                }
            }
            return new HttpResponseMessage("Successfully added time slot");
        }

        public HttpResponseMessage DeleteTimeSlot(string timeSlot)
        {
            var schedule = GetSchedule();
            var timeSlotToRemove = schedule.timeSlots.Where(t => t.timeSlot == timeSlot).First();

            foreach(var session in timeSlotToRemove.sessions)
            {
                conferenceOrganizerDatabase.DeleteSession(session.id);
            }
            return new HttpResponseMessage("Successfully removed time slot");
        }

        private static Session GetSession(TimeSlot timeSlot, string room)
        {
            var splitTimeSlot = timeSlot.timeSlot.Split('-');
            int.TryParse(splitTimeSlot[0].Split(':')[0], out int startHour);
            int.TryParse(splitTimeSlot[0].Split(':')[1], out int startMin);
            int.TryParse(splitTimeSlot[1].Split(':')[0], out int endHour);
            int.TryParse(splitTimeSlot[1].Split(':')[1], out int endMin);
            var session = new Session
            {
                room = room,
                startHour = startHour,
                startMin = startMin,
                endHour = endHour,
                endMin = endMin
            };
            return session;
        }

        public void SetScheduleRooms(string id, Rooms rooms)
        {
            var schedule = GetSchedule();

            if (schedule.timeSlots.Count() == 0) { }
            else if (ShouldAddRoom(rooms, schedule))
            {
                AddSessions(rooms, schedule);
            }
            else if (ShouldRemoveRoom(rooms, schedule))
            {
                RemoveSessions(rooms, schedule);
            }
            conferenceOrganizerDatabase.SetScheduleRooms(id, rooms);
        }

        private void RemoveSessions(Rooms rooms, Schedule schedule)
        {
            var roomToDelete = schedule.rooms.Except(rooms.rooms).First();
            var sessionsToDelete = conferenceOrganizerDatabase.GetSessions()
                                                              .Where((s) =>
                                                              {
                                                                  return s.room == roomToDelete;
                                                              });
            foreach (var session in sessionsToDelete)
            {
                conferenceOrganizerDatabase.DeleteSession(session.id);
            }
        }

        private static bool ShouldRemoveRoom(Rooms rooms, Schedule schedule)
        {
            return schedule.rooms.Count() > rooms.rooms.Count();
        }

        private static bool ShouldAddRoom(Rooms rooms, Schedule schedule)
        {
            return schedule.rooms.Count() < rooms.rooms.Count();
        }

        private void AddSessions(Rooms rooms, Schedule schedule)
        {
            var newRoom = rooms.rooms.Except(schedule.rooms).First();
            foreach (var timeSlot in schedule.timeSlots)
            {
                Session session = GetSession(timeSlot, newRoom);
                conferenceOrganizerDatabase.PostSession(session);
            }
        }
    }
}
