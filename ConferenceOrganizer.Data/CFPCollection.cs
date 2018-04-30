using MongoDB.Driver;

namespace ConferenceOrganizer.Data
{
    public class CFPCollection : ICFPCollection
    {
        IMongoCollection<MongoCFP> collection;
        IMongoDatabase database;

        public CFPCollection()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<MongoCFP>("cfp");
        }

        public MongoCFP GetCFPStatus()
        {
            return collection.Find(x => true).ToListAsync().Result[0];
        }

        public void PutCFP(string id, MongoCFP cfp)
        {
            var filter = Builders<MongoCFP>.Filter.Eq("id", id);
            collection.FindOneAndReplace(filter, cfp);
        }
    }
}
