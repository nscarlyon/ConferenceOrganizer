using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IScheduleCollection
    {
        MongoSchedule GetSchedule();
        void PostSchedule(MongoSchedule schedule);
        void PutSchedule(string id, MongoSchedule schedule);
        void PublishSchedule();
        void UnpublishSchedule();
        void DeleteSchedule();
    }
}