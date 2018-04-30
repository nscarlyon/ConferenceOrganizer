using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;
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
        public static Mock<IProposalsCollection> mockProposalsCollection = new Mock<IProposalsCollection>();
        public ScheduleDomain scheduleDomain = new ScheduleDomain(mockScheduleCollection.Object, mockSessionsCollection.Object, mockProposalsCollection.Object);

        [Test]
        public void TimeSlotsSortByHour()
        {
            var firstTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };
            var secondTimeSlot = new MongoTimeSlot
            {
                StandardTime = "8:00-9:00 A.M",
                StartHour = 8,
                StartMin = 0,
                EndHour = 9,
                EndMin = 0
            };

            var timeSlots = new List<MongoTimeSlot> { firstTimeSlot, secondTimeSlot };
            var result = scheduleDomain.GetSortedTimeSlots(timeSlots);
            var expected = new List<MongoTimeSlot> { secondTimeSlot, firstTimeSlot };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TimeSlotsSortByHourAndMin()
        {
            var firstTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:30-10:00 A.M",
                StartHour = 9,
                StartMin = 30,
                EndHour = 10,
                EndMin = 0
            };
            var secondTimeSlot = new MongoTimeSlot
            {
                StandardTime = "9:00-10:00 A.M",
                StartHour = 9,
                StartMin = 0,
                EndHour = 10,
                EndMin = 0
            };

            var timeSlots = new List<MongoTimeSlot> { firstTimeSlot, secondTimeSlot };
            var result = scheduleDomain.GetSortedTimeSlots(timeSlots);
            var expected = new List<MongoTimeSlot> { secondTimeSlot, firstTimeSlot };
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

            IEnumerable<MongoSession> sessions = new List<MongoSession>
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
            mockProposalsCollection.Setup(x => x.FindProposal(It.IsAny<string>())).Returns(() =>
            new MongoProposal()
            {
                id = "3",
                scheduledTimes = new List<MongoScheduleTime>
                {
                    new MongoScheduleTime
                    {
                        room = "Room B"
                    }
                }
            });
            scheduleDomain.UpdateSessionsAndProposals(schedule);
            mockProposalsCollection.Verify(p => p.FindProposal("3"));
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

            IEnumerable<MongoSession> sessions = new List<MongoSession>
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
            mockProposalsCollection.Setup(x => x.FindProposal("3")).Returns(() =>
            new MongoProposal()
            {
                id = "3",
                scheduledTimes = new List<MongoScheduleTime>
                {
                    new MongoScheduleTime
                    {
                        standardTime = "8:00-9:00 A.M"
                    }
                }
            });

            var updatedProposal = new MongoProposal
            {
                id = "3"
            };
            scheduleDomain.UpdateSessionsAndProposals(schedule);
            mockProposalsCollection.Verify(p => p.FindProposal("3"));
            mockSessionsCollection.Verify(x => x.DeleteSession("1"));
        }
    }
}
