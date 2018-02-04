using ConferenceOrganizer.Data;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class OrderedSchedule
    {
        private Schedule _schedule;
        public IEnumerable<TimeSlot> _timeSlots;
        public IEnumerable<string> _rooms;
        private IEnumerable<Session> _sessions;

        public OrderedSchedule(Schedule schedule, IEnumerable<Session> sessions)
        {
            _schedule = schedule;
            _rooms = schedule.rooms;
            _sessions = sessions;
        }

        public Schedule GetOrderedSchedule()
        {
            if (_sessions.Count() == 0) return _schedule;
            var orderedSessionsByTime = _sessions
                                            .OrderBy(session => session.startHour)
                                            .ThenBy(s => s.startMin);
            var numberOfRooms = _rooms.Count();
            var numberOfTimeSlots = _sessions.Where((s, i) => i % numberOfRooms == 0).Count();
            var newTimeSlots = new List<TimeSlot>();
            var skip = 0;

            for (var i = 0; i < numberOfTimeSlots; i++)
            {
                var timeSlotSessions = orderedSessionsByTime.Skip(skip).Take(numberOfRooms);
                timeSlotSessions = GetOrderedSessionsByRoom(timeSlotSessions);
                var time = GetTime(timeSlotSessions.First());
                newTimeSlots.Add(new TimeSlot{
                    timeSlot = time,
                    sessions = timeSlotSessions
                });
                skip += numberOfRooms;
            }
            
            _schedule.timeSlots = newTimeSlots;

            return _schedule;
        }

        private IEnumerable<Session> GetOrderedSessionsByRoom(IEnumerable<Session> sessions)
        {
            SessionRoomComparer sessionRoomComparer = new SessionRoomComparer(_schedule.rooms);
            var orderedSessions = sessions;
            orderedSessions = orderedSessions.OrderBy(s => s, sessionRoomComparer);
            return orderedSessions;
        }

        private string GetTime(Session session)
        {
            string startMin = GetStartMin(session);
            string endMin = GetEndMin(session);

            return session.startHour.ToString()
                + ":"
                + startMin
                + "-"
                + session.endHour.ToString()
                + ":"
                + endMin;
        }

        private static string GetEndMin(Session session)
        {
            return session.endMin < 2
                            ? "0" + session.endMin
                            : session.endMin.ToString();
        }

        private static string GetStartMin(Session session)
        {
            return session.startMin < 2
                ? "0" + session.startMin
                : session.startMin.ToString();
        }
    }
}