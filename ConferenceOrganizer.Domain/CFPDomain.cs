using ConferenceOrganizer.Data;
using ConferenceOrganizer.Domain.DomainModels;

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
            var cfp = cfpCollection.GetCFPStatus();

            return new CFP {
                id =cfp.id,
                status = cfp.status
            };
        }

        public void PutCfp(string id, CFP cfp)
        {
            var mongoCFP = new MongoCFP
            {
                id = cfp.id,
                status = cfp.status
            };

            cfpCollection.PutCFP(id, mongoCFP);
        } 
    }
}
