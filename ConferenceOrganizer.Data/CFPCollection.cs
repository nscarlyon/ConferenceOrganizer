using MongoDB.Driver;

namespace ConferenceOrganizer.Data
{
    public class CFPCollection : ICFPCollection
    {
        IMongoCollection<CFP> collection;
        IMongoDatabase database;

        public CFPCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<CFP>("cfp");
        }

        public CFP GetCFPStatus()
        {
            return collection.Find(x => true).ToListAsync().Result[0];
        }

        public void PutCFP(string id, CFP cfp)
        {
            var filter = Builders<CFP>.Filter.Eq("id", id);
            collection.FindOneAndReplace(filter, cfp);
        }
    }
}
