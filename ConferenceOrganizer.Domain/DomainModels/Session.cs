namespace ConferenceOrganizer.Domain.DomainModels
{
    public class SessionResponse
    {
        public string id { get; set; }
        public bool Break { get; set; }
        public string SpeakerName { get; set; }
        public string Email { get; set; }
        public string ProposalId { get; set; }
        public string Title { get; set; }
        public string Bio { get; set; }
        public string Description { get; set; }
        public string Room { get; set; }
        public string StandardTime { get; set; }
    }
}