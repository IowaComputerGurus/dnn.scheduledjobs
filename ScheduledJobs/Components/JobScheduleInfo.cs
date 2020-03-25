// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    /// <summary>
    /// This class holds information on a specific job schedule item.
    /// </summary>
    [DebuggerDisplay("JobScheduleId: {JobScheduleId}, JobTitle: {JobTitle}")]
    public class JobScheduleInfo
    {
        public int JobScheduleId { get; set; }
        public int JobTypeId { get; set; }
        public string JobTitle { get; set; }
        public string JobScript { get; set; }
        public int JobFrequencyValue { get; set; }
        public string JobFrequencyType { get; set; }
        public DateTime NextJobRun { get; set; }
        public DateTime LastJobRun { get; set; }

        public string Schedule
        {
            get { return JobFrequencyValue.ToString() + " " + JobFrequencyType; }
        }
    }
}
