namespace ConferenceOrganizer.Data
{
    public class MongoTimeSlot
    {
        public string StandardTime { get; set; }
        public int StartHour { get; set; }
        public int StartMin { get; set; }
        public int EndHour { get; set; }
        public int EndMin { get; set; }
    }
}