﻿using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IConferenceOrganizerDatabase
    {
        CFP GetCFPStatus();
        void PutCFP(string id, CFP cfp);

        IEnumerable<string> GetSpeakers();
        IEnumerable<Session> GetSessions();
        void PostSession(Session session);
        void DeleteSession(string id);

        IEnumerable<Proposal> GetProposals();
        IEnumerable<Proposal> GetProposalsBySpeaker(string name);
        Proposal FindProposal(string id);
        void PostProposal(Proposal proposal);
        void PutProposal(string id, Proposal proposal);
        void DeleteProposal(string id);
        void DeleteProposals();
    }
}