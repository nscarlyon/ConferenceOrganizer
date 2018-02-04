using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Tests
{
    [TestFixture]
    public class OrderedScheduleTests
    {
        public static Mock<IConferenceOrganizerDatabase> conferenceOrganizerDatabase = new Mock<IConferenceOrganizerDatabase>();
        private Schedule schedule;
        private IEnumerable<Session> availableSessions;

        [SetUp]
        public void SetUp()
        {
            schedule = new Schedule();

            var sessionOne = new Session()
            {
                room = "Room B",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };

            var sessionTwo = new Session()
            {
                room = "Room C",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };

            var sessionThree = new Session()
            {
                room = "Room A",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };
            availableSessions = new List<Session> { sessionOne, sessionTwo, sessionThree };
        }

        [Test]
        public void OrderEmptySchedule()
        {
            IEnumerable<Session> sessions = new List<Session> { };
            var orderedSchedule = new OrderedSchedule(schedule, sessions);
            var expectedSchedule = new Schedule();
            Assert.AreEqual(expectedSchedule.rooms, orderedSchedule.GetOrderedSchedule().rooms);
        }

        [Test]
        public void OrderOneSession()
        {
            schedule.rooms = new string[] { "Room A" };
            IEnumerable<Session> sessions = new List<Session> { availableSessions.ElementAt(2) };
            var orderedSchedule = new OrderedSchedule(schedule, sessions).GetOrderedSchedule();

            Assert.AreEqual("Room A", orderedSchedule.rooms.First());
            Assert.AreEqual("9:00-10:00", orderedSchedule.timeSlots.First().timeSlot);
            Assert.AreEqual("Room A", orderedSchedule.timeSlots.First().sessions.First().room);
        }

        [Test]
        public void OrderSessionsByRoom()
        {
            schedule.rooms = new string[] { "Room A", "Room B", "Room C" };
            IEnumerable<Session> sessions = new List<Session> { availableSessions.ElementAt(0), availableSessions.ElementAt(1), availableSessions.ElementAt(2) };
            var orderedSchedule = new OrderedSchedule(schedule, sessions).GetOrderedSchedule();

            Assert.AreEqual("Room A", orderedSchedule.timeSlots.First().sessions.ElementAt(0).room);
            Assert.AreEqual("Room B", orderedSchedule.timeSlots.First().sessions.ElementAt(1).room);
            Assert.AreEqual("Room C", orderedSchedule.timeSlots.First().sessions.ElementAt(2).room);
        }

        [Test]
        public void CreateTimeSlotForNewTime()
        {
            var sessionWithDiffTime = new Session()
            {
                room = "Room A",
                startHour = 8,
                startMin = 0,
                endHour = 9,
                endMin = 0
            };
            schedule.rooms = new string[] { "Room A" };
            IEnumerable<Session> sessions = new List<Session> { availableSessions.ElementAt(0), sessionWithDiffTime };
            var orderedSchedule = new OrderedSchedule(schedule, sessions).GetOrderedSchedule();

            Assert.AreEqual(orderedSchedule.timeSlots.Count(), 2);
            Assert.AreEqual(orderedSchedule.timeSlots.First().timeSlot, "8:00-9:00");
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(1).timeSlot, "9:00-10:00");
        }

        [Test]
        public void OrderTimeSlotSessions()
        {
            schedule.rooms = new string[] { "Room A", "Room B", "Room C" };
            var sessionRoomATime1 = new Session()
            {
                room = "Room A",
                startHour = 8,
                startMin = 0,
                endHour = 9,
                endMin = 0
            };
            var sessionRoomATime2 = new Session()
            {
                room = "Room A",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };
            var sessionRoomATime3 = new Session()
            {
                room = "Room A",
                startHour = 10,
                startMin = 0,
                endHour = 11,
                endMin = 0
            };
            var sessionRoomBTime1 = new Session()
            {
                room = "Room B",
                startHour = 8,
                startMin = 0,
                endHour = 9,
                endMin = 0
            };
            var sessionRoomBTime2 = new Session()
            {
                room = "Room B",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };
            var sessionRoomBTime3 = new Session()
            {
                room = "Room B",
                startHour = 10,
                startMin = 0,
                endHour = 11,
                endMin = 0
            };
            var sessionRoomCTime1 = new Session()
            {
                room = "Room C",
                startHour = 8,
                startMin = 0,
                endHour = 9,
                endMin = 0
            };
            var sessionRoomCTime2 = new Session()
            {
                room = "Room C",
                startHour = 9,
                startMin = 0,
                endHour = 10,
                endMin = 0
            };
            var sessionRoomCTime3 = new Session()
            {
                room = "Room C",
                startHour = 10,
                startMin = 0,
                endHour = 11,
                endMin = 0
            };

            schedule.rooms = new List<string> { "Room A", "Room B", "Room C" };
            IEnumerable<Session> sessions = new List<Session> { sessionRoomBTime3, sessionRoomBTime1, sessionRoomATime1, sessionRoomATime3, sessionRoomCTime3, sessionRoomCTime2, sessionRoomATime2, sessionRoomBTime2, sessionRoomCTime1 };
            var orderedSchedule = new OrderedSchedule(schedule, sessions).GetOrderedSchedule();

            Assert.AreEqual(orderedSchedule.timeSlots.First().timeSlot, "8:00-9:00");
            Assert.AreEqual(orderedSchedule.timeSlots.First().sessions.First(), sessionRoomATime1);
            Assert.AreEqual(orderedSchedule.timeSlots.First().sessions.ElementAt(1), sessionRoomBTime1);
            Assert.AreEqual(orderedSchedule.timeSlots.First().sessions.ElementAt(2), sessionRoomCTime1);

            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(1).timeSlot, "9:00-10:00");
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(1).sessions.First(), sessionRoomATime2);
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(1).sessions.ElementAt(1), sessionRoomBTime2);
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(1).sessions.ElementAt(2), sessionRoomCTime2);

            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(2).timeSlot, "10:00-11:00");
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(2).sessions.First(), sessionRoomATime3);
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(2).sessions.ElementAt(1), sessionRoomBTime3);
            Assert.AreEqual(orderedSchedule.timeSlots.ElementAt(2).sessions.ElementAt(2), sessionRoomCTime3);
        }
    }
}
