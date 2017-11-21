using ConferenceOrganizer.Data;
using Moq;
using NUnit.Framework;

namespace ConferenceOrganizer.Tests
{
    [TestFixture]
    public class MockTests
    {
        public static Mock<IConferenceOrganizerDatabase> conferenceOrganizerDatabase = new Mock<IConferenceOrganizerDatabase>();
    }
}
