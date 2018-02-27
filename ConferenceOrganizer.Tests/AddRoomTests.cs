//using ConferenceOrganizer.Data;
//using ConferenceOrganizer.Domain;
//using Moq;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Linq;

//namespace ConferenceOrganizer.Tests
//{
//    [TestFixture]
//    public class AddRoomTests
//    {
//        public static Mock<IConferenceOrganizerDatabase> mockConferenceOrganizerDatabase = new Mock<IConferenceOrganizerDatabase>();

//        [Test]
//        public void AddRoom()
//        {
//            var scheduleDomain = new ScheduleDomain(mockConferenceOrganizerDatabase.Object);
//            var rooms = new Rooms { rooms = new []{"Room A" } };
//            mockConferenceOrganizerDatabase.Setup(x => x.GetSchedule()).Returns(() => new Schedule
//            {
//                id = "123",
//                Rooms = new List<string> { },
//                TimeSlots = new List<TimeSlot>()
//                {
//                    new TimeSlot {timeSlot="8:00-9:00", sessions = new Session[]{ } }
//                }
//            });

//            scheduleDomain.SetScheduleRooms("123", rooms);
//            var schedule = scheduleDomain.GetSchedule();
//            Assert.AreEqual(schedule.Rooms.First(), "Room C");
//        }
//    }
//}
