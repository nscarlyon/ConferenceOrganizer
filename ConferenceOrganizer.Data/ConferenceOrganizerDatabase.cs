using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Data
{
    public class ScheduleCollection : IScheduleCollection
    {
        IMongoCollection<MongoSchedule> collection;
        IMongoDatabase database;

        public ScheduleCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<MongoSchedule>("schedule");
        }

        public MongoSchedule GetSchedule()
        {
            IEnumerable<MongoSchedule> schedules = collection.Find(x => true).ToListAsync().Result;
            if(schedules.Any()) return schedules.First();
            return null;
        }

        public void DeleteSchedule()
        {
            MongoSchedule schedule = collection.Find(x => true).ToListAsync().Result.First();
            collection.DeleteOne(s => s.id == schedule.id);
        }

        public void PostSchedule(MongoSchedule schedule)
        {
            collection.InsertOne(schedule);
        }

        public void PutSchedule(string id, MongoSchedule newSchedule)
        {
            MongoSchedule schedule = collection.Find(x => true).ToListAsync().Result.First();
            var filter = Builders<MongoSchedule>.Filter.Eq("id", id);
            var update = Builders<MongoSchedule>.Update
                                            .Set("Rooms", newSchedule.Rooms)
                                            .Set("TimeSlots", newSchedule.TimeSlots);
            collection.UpdateOne(filter, update);
        }

        public void PublishSchedule()
        {
            MongoSchedule unpublishedSchedule = collection.Find(x => x.Published == false).ToListAsync().Result.First();
            unpublishedSchedule.Published = true;
            var filter = Builders<MongoSchedule>.Filter.Eq("id", unpublishedSchedule.id);
            collection.ReplaceOne(filter, unpublishedSchedule);
        }

        public void UnpublishSchedule()
        {
            MongoSchedule publishedSchedule = collection.Find(x => x.Published == true).ToListAsync().Result.First();
            publishedSchedule.Published = false;
            var filter = Builders<MongoSchedule>.Filter.Eq("id", publishedSchedule.id);
            collection.ReplaceOne(filter, publishedSchedule);
        }
    }
}
