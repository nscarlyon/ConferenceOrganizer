using ConferenceOrganizer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Domain
{
    public class SessionRoomComparer : IComparer<Session>
    {
        private IEnumerable<string> _rooms;
        private Dictionary<string, int> orderedRoomsMap;

        public SessionRoomComparer(IEnumerable<string> rooms)
        {
            _rooms = rooms;
            orderedRoomsMap = new Dictionary<string, int> { };
            for (var i = 0; i < rooms.Count(); i++)
            {
                orderedRoomsMap.Add(rooms.ElementAt(i), i);
            }
        }

        public int Compare(Session sessionOne, Session sessionTwo)
        {
            if (orderedRoomsMap[sessionOne.room] > orderedRoomsMap[sessionTwo.room])
            {
                return 1;
            }
            else if (orderedRoomsMap[sessionOne.room] == orderedRoomsMap[sessionTwo.room])
            {
                return 0;
            }
            return -1;
        }
    }
}
