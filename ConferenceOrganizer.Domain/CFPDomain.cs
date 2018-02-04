using ConferenceOrganizer.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Domain
{
    public class CFPDomain
    {
        private ConferenceOrganizerDatabase conferenceOrganizerDatabase;

        public CFPDomain()
        {
            conferenceOrganizerDatabase = new ConferenceOrganizerDatabase();
        }

        public CFP GetCfp()
        {
            return conferenceOrganizerDatabase.GetCFPStatus();
        }

        public void PutCfp(string id, CFP cfp)
        {
            conferenceOrganizerDatabase.PutCFP(id, cfp);
        } 
    }
}
