using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceOrganizer.Data
{
    public interface ICFPCollection
    {
        MongoCFP GetCFPStatus();
        void PutCFP(string id, MongoCFP cfp);
    }
}
