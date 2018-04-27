using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface ICFPCollection
    {
        CFP GetCFPStatus();
        void PutCFP(string id, CFP cfp);
    }
}
