using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain;
using ConferenceOrganizer.Domain.DomainModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Tests
{
    [TestFixture]
    public class ProposalsDomainTests
    {
        public static Mock<IProposalsCollection> mockProposalsCollection = new Mock<IProposalsCollection>();
        public static Mock<ISessionsCollection> mockSessionsCollection = new Mock<ISessionsCollection>();
        public ProposalsDomain proposalDomain = new ProposalsDomain(mockProposalsCollection.Object, mockSessionsCollection.Object);

        [Test]
        public void GetProposalsWithScheduledSessions()
        {
            var proposalOne = new MongoProposal
            {
                id = "proposal-1",
                SpeakerName = "speaker-1",
                Bio = "bio-1",
                Title = "title-1",
                Description = "description-1",
                Email = "email-1"
            };
            var sessionOne = new MongoSession
            {
                id = "session-1",
                ProposalId = "proposal-1",
                StandardTime = "10:00-11:00 A.M",
                Room = "Room A"
            };
            var sessionTwo = new MongoSession
            {
                id = "session-2",
                ProposalId = "proposal-1",
                StandardTime = "9:00-10:00 A.M",
                Room = "Room B"
            };
            var proposalTwo = new MongoProposal
            {
                id = "proposal-2",
                SpeakerName = "speaker-2",
                Bio = "bio-2",
                Title = "title-2",
                Description = "description-2",
                Email = "email-2"
            };
            var sessionThree = new MongoSession
            {
                id = "session-3",
                ProposalId = "proposal-2",
                StandardTime = "9:00-10:00 A.M",
                Room = "Room B"
            };
            var expectedSessionsOne = new List<ScheduledSession>
                {
                    new ScheduledSession
                    {
                        SessionId = sessionOne.id,
                        StandardTime = sessionOne.StandardTime,
                        Room = sessionOne.Room
                    },
                    new ScheduledSession
                    {
                        SessionId = sessionTwo.id,
                        StandardTime = sessionTwo.StandardTime,
                        Room = sessionTwo.Room
                    }
                };
            var expectedSessionsTwo = new List<ScheduledSession>
                {
                    new ScheduledSession
                    {
                        SessionId = sessionThree.id,
                        StandardTime = sessionThree.StandardTime,
                        Room = sessionThree.Room
                    }
                };
            var proposals = new List<MongoProposal> { proposalOne, proposalTwo };
            var sessions = new List<MongoSession> { sessionThree, sessionOne, sessionTwo};

            mockProposalsCollection.Setup(x => x.GetProposals()).Returns(() => proposals);
            mockSessionsCollection.Setup(x => x.GetSessions()).Returns(() => sessions);
            var result = proposalDomain.GetProposals();
            Assert.AreEqual(expectedSessionsOne, result.First().ScheduledSessions);
            Assert.AreEqual(expectedSessionsTwo, result.Last().ScheduledSessions);
        }
    }
}
