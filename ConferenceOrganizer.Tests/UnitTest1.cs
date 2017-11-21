using ConferenceOrganizer.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConferenceOrganizer.Tests
{
    [TestFixture]
    public class MockTests
    {
        public static Mock<IConferenceOrganizerDatabase> conferenceOrganizerDatabase = new Mock<IConferenceOrganizerDatabase>();

        //[Test]
        //public void GetAllProposals()
        //{
        //    conferenceOrganizerDatabase.Setup(x => x.GetProposals()).Returns(() => new List<Proposal>
        //    {
        //      new Proposal {speakerName = "Wonder Woman", bio = "She is a wonder."}
        //    });

        //    conferenceOrganizerDatabase.Verify(x => x.GetProposals());
        //}
    }
}
