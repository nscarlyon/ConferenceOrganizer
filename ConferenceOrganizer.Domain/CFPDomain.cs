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

        public CFP GetCfp()
        {
            return cfpCollection.GetCFPStatus();
        }

        public void PutCfp(string id, CFP cfp)
        {
            cfpCollection.PutCFP(id, cfp);
        } 
    }
}
