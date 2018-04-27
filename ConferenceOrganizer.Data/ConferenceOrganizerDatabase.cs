using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceOrganizer.Data
{
    public class ConferenceOrganizerDatabase : IConferenceOrganizerDatabase
    {
        IMongoCollection<Proposal> collection;
        IMongoDatabase database;

        public ConferenceOrganizerDatabase()
        {
            database = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("conferenceOrganizer");
            collection = database.GetCollection<Proposal>("proposals");
        }

        public IEnumerable<string> GetSpeakers()
        {
            var result = collection.AsQueryable<Proposal>()
                                   .Select(p => p.speakerName)
                                   .Distinct();
            return result;
        }

        public IEnumerable<Proposal> GetProposalsBySpeaker(string name)
        {
            var filterName = name.Replace("-", " ");
            var filter = Builders<Proposal>.Filter.Eq("speakerName", filterName);
            return collection.Find(filter).ToListAsync().Result;
        }

        public void PutCFP(string id, CFP cfp)
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            var filter = Builders<CFP>.Filter.Eq("id", id);
            cfpCollection.FindOneAndReplace(filter, cfp);
        }

        public void DeleteSessions()
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            sessionsCollection.DeleteMany(X => true);
        }

        public CFP GetCFPStatus()
        {
            var cfpCollection = database.GetCollection<CFP>("cfp");
            return cfpCollection.Find(x => true).ToListAsync().Result[0];
        }

        public IEnumerable<Proposal> GetProposals()
        {
            return collection.Find(x => true).ToListAsync().Result;
        }

        public Proposal FindProposal(string id)
        {
            return collection.Find(x => x.id == id).First();
        }

        public void PostProposal(Proposal proposal)
        {
            collection.InsertOne(proposal);
        }

        public void UpdateProposal(Proposal proposal)
        {
            var filter = Builders<Proposal>.Filter.Eq("id", proposal.id);
            collection.ReplaceOne(filter, proposal);
        }

        public void DeleteProposal(string id)
        {
            collection.DeleteOne(X=> X.id == id);
        }

        public void DeleteProposals()
        {
            collection.DeleteMany(X => true);
        }

        public IEnumerable<Session> GetSessions()
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            return sessionsCollection.Find(x => true).ToListAsync().Result;
        }

        public Session GetSession(string id)
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            return sessionsCollection.Find(x => x.id == id).First();
        }

        public void PostSession(Session session)
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            sessionsCollection.InsertOne(session);
        }

        public void PutSession(string id, Session session)
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            var filter = Builders<Session>.Filter.Eq("id", id);
            session.id = id;
            sessionsCollection.FindOneAndReplace(filter, session);
        }

        public void DeleteSession(string id)
        {
            var sessionsCollection = database.GetCollection<Session>("sessions");
            sessionsCollection.DeleteOne(X => X.id == id);
        }

        public Schedule GetSchedule()
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            IEnumerable<Schedule> schedules = scheduleCollection.Find(x => true).ToListAsync().Result;
            if(schedules.Any()) return schedules.First();
            return null;
        }

        public void DeleteSchedule()
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            Schedule schedule = scheduleCollection.Find(x => true).ToListAsync().Result.First();
            scheduleCollection.DeleteOne(s => s.id == schedule.id);
        }

        public void PostSchedule(Schedule schedule)
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            scheduleCollection.InsertOne(schedule);
        }

        public void PutSchedule(string id, Schedule newSchedule)
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            Schedule schedule = scheduleCollection.Find(x => true).ToListAsync().Result.First();
            var filter = Builders<Schedule>.Filter.Eq("id", id);
            var update = Builders<Schedule>.Update
                                            .Set("Rooms", newSchedule.Rooms)
                                            .Set("TimeSlots", newSchedule.TimeSlots);
            scheduleCollection.UpdateOne(filter, update);
        }

        public void PublishSchedule()
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            Schedule unpublishedSchedule = scheduleCollection.Find(x => x.Published == false).ToListAsync().Result.First();
            unpublishedSchedule.Published = true;
            var filter = Builders<Schedule>.Filter.Eq("id", unpublishedSchedule.id);
            scheduleCollection.ReplaceOne(filter, unpublishedSchedule);
        }

        public void UnpublishSchedule()
        {
            var scheduleCollection = database.GetCollection<Schedule>("schedule");
            Schedule publishedSchedule = scheduleCollection.Find(x => x.Published == true).ToListAsync().Result.First();
            publishedSchedule.Published = false;
            var filter = Builders<Schedule>.Filter.Eq("id", publishedSchedule.id);
            scheduleCollection.ReplaceOne(filter, publishedSchedule);
        }
    }
}
