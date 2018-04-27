﻿using System.Collections.Generic;

namespace ConferenceOrganizer.Data
{
    public interface IScheduleCollection
    {
        Schedule GetSchedule();
        void PostSchedule(Schedule schedule);
        void PutSchedule(string id, Schedule schedule);
        void PublishSchedule();
        void UnpublishSchedule();
        void DeleteSchedule();
    }
}