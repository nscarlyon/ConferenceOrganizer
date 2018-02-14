using ConferenceOrganizer.Data;
using System;
using System.Collections.Generic;
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


        public void AddRoom(string id, Rooms rooms)
        {
            var schedule = GetSchedule();
            if (schedule.timeSlots.Count() == 0)
            {
            }
            else if(schedule.rooms.Count() != rooms.rooms.Count())
            {
                var newRoom = rooms.rooms.Except(schedule.rooms).First();

                foreach (var timeSlot in schedule.timeSlots)
                {
                    var splitTimeSlot = timeSlot.timeSlot.Split('-');
                    int.TryParse(splitTimeSlot[0].Split(':')[0], out int startHour);
                    int.TryParse(splitTimeSlot[0].Split(':')[1], out int startMin);
                    int.TryParse(splitTimeSlot[1].Split(':')[0], out int endHour);
                    int.TryParse(splitTimeSlot[1].Split(':')[1], out int endMin);
                    var session = new Session
                    {
                        room = newRoom,
                        startHour = startHour,
                        startMin = startMin,
                        endHour = endHour,
                        endMin = endMin
                    };
                    Console.WriteLine(session.startHour);
                    conferenceOrganizerDatabase.PostSession(session);
                }
            }
            conferenceOrganizerDatabase.AddRoomsToSchedule(id, rooms);
        }
    }
}
