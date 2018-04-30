using ConferenceOrganizer.Data;

namespace ConferenceOrganizer.Domain
{
    public class CFPDomain
    {
        private CFPCollection cfpCollection;

        public CFPDomain()
        {
            cfpCollection = new CFPCollection();
        }

        public MongoCFP GetCfp()
        {
            return cfpCollection.GetCFPStatus();
        }

        public void PutCfp(string id, MongoCFP cfp)
        {
            cfpCollection.PutCFP(id, cfp);
        } 
    }
}
