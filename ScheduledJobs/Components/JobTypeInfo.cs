// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    public class JobTypeInfo
    {
	    public int JobTypeId {get; set;}
	    public string JobTitle {get; set;}
	    public string JobDescription {get; set;}
	    public bool IsCannedJob {get; set;}
        public string CannedProcedure { get; set; }
    }
}
