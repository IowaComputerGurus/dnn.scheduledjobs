// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    public class JobScheduleHistoryInfo
    {
        public int JobScheduleHistoryId { get; set; }
        public int JobTypeId { get; set; }
        public DateTime ExecuteTime { get; set; }
        public bool Successful { get; set; }
        public string Detail { get; set; }
    }
}
