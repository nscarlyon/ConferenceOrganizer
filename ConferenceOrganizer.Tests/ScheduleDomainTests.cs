using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;
using ConferenceOrganizer.Domain.DomainModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ConferenceOrganizer.Tests
{
    [TestFixture]
    public class ScheduleDomainTests
    {
        public static Mock<IScheduleCollection> mockScheduleCollection = new Mock<IScheduleCollection>();
        public static Mock<ISessionsCollection> mockSessionsCollection = new Mock<ISessionsCollection>();
        public ScheduleDomain scheduleDomain = new ScheduleDomain(mockScheduleCollection.Object, mockSessionsCollection.Object);

        [Test]
        public void TimeSlotsSortByHour()
        {
            var firstTimeSlot = new TimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };

            var mongoFirstTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };

            var secondTimeSlot = new TimeSlot
            {
                StandardTime = "8:00-9:00 A.M",
                StartHour = 8,
                StartMin = 0,
                EndHour = 9,
                EndMin = 0
            };

            var mongoSecondTimeSlot = new MongoTimeSlot
            {
                StandardTime = "8:00-9:00 A.M",
                StartHour = 8,
                StartMin = 0,
                EndHour = 9,
                EndMin = 0
            };

            var mongoTimeSlots = new List<MongoTimeSlot> { mongoFirstTimeSlot, mongoSecondTimeSlot };
            var expected = new List<TimeSlot> { secondTimeSlot, firstTimeSlot};
            var result = scheduleDomain.GetSortedTimeSlots(mongoTimeSlots);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSlotsSortByHourAndMin()
        {
            var firstTimeSlot = new TimeSlot
            {
                StandardTime = "9:30-10:00 A.M",
                StartHour = 9,
                StartMin = 30,
                EndHour = 10,
                EndMin = 0
            };

            var mongoFirstTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:30-10:00 A.M",
                StartHour = 9,
                StartMin = 30,
                EndHour = 10,
                EndMin = 0
            };

            var secondTimeSlot = new TimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };

            var mongoSecondTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };


            var mongoTimeSlots = new List<MongoTimeSlot> { mongoFirstTimeSlot, mongoSecondTimeSlot};
            var result = scheduleDomain.GetSortedTimeSlots(mongoTimeSlots);
            var expected = new List<TimeSlot> { secondTimeSlot, firstTimeSlot };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteSessionsForDeletedRoom()
        {
            var schedule = new MongoSchedule
            {
                Rooms = new List<string> { "Room A" },
                TimeSlots = new List<MongoTimeSlot>
                {
                    new MongoTimeSlot
                    {
                        StandardTime = "9:00-10:00 A.M"
                    }
                }
            };

            var sessions = new List<MongoSession>
            {
                new MongoSession
                {
                    id = "1",
                    Room = "Room A",
                    StandardTime = "9:00-10:00 A.M",
                    Break = false
                },
                new MongoSession
                {
                    id = "2",
                    ProposalId = "3",
                    Room = "Room B",
                    StandardTime = "9:00-10:00 A.M",
                    Break = false
                }
            };

            mockSessionsCollection.Setup(x => x.GetSessions()).Returns(() => sessions);
            scheduleDomain.UpdateSessions(schedule);
            mockSessionsCollection.Verify(x => x.DeleteSession("2"));
        }

        [Test]
        public void DeleteSessionsForDeletedTimeSlot()
        {
            var schedule = new MongoSchedule
            {
                Rooms = new List<string> { "Room A" },
                TimeSlots = new List<MongoTimeSlot>
                {
                    new MongoTimeSlot
                    {
                        StandardTime = "9:00-10:00 A.M"
                    }
                }
            };

            var sessions = new List<MongoSession>
            {
                new MongoSession
                {
                    id = "1",
                    ProposalId = "3",
                    Room = "Room A",
                    StandardTime = "8:00-9:00 A.M",
                    Break = false
                },
                new MongoSession
                {
                    id = "2",
                    Room = "Room A",
                    StandardTime = "9:00-10:00 A.M",
                    Break = false
                }
            };

            mockSessionsCollection.Setup(x => x.GetSessions()).Returns(() => sessions);
            
            var updatedProposal = new MongoProposal
            {
                id = "3"
            };
            scheduleDomain.UpdateSessions(schedule);
            mockSessionsCollection.Verify(x => x.DeleteSession("1"));
        }

        [Test]
        public void DeleteBreak()
        {
            var schedule = new MongoSchedule
            {
                Rooms = new List<string> { "Room A" },
                TimeSlots = new List<MongoTimeSlot>
                {
                    new MongoTimeSlot
                    {
                        StandardTime = "9:00-10:00 A.M"
                    }
                }
            };

            var sessions = new List<MongoSession>
            {
                new MongoSession
                {
                    id = "1",
                    ProposalId = null,
                    Room = null,
                    StandardTime = "8:00-9:00 A.M",
                    Break = true
                },
                new MongoSession
                {
                    id = "2",
                    Room = "Room A",
                    StandardTime = "9:00-10:00 A.M",
                    Break = false
                }
            };

            mockSessionsCollection.Setup(x => x.GetSessions()).Returns(() => sessions);

            scheduleDomain.UpdateSessions(schedule);
            mockSessionsCollection.Verify(x => x.DeleteSession("1"));
        }
    }
}
