// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    public class ScheduledSqlJobsController
    {

        #region Job Types Methods
        public List<JobTypeInfo> GetJobTypes()
        {
            return CBO.FillCollection<JobTypeInfo>(DataProvider.Instance().GetJobTypes());
        }
        public JobTypeInfo GetJobTypeById(int jobTypeId)
        {
            return CBO.FillObject<JobTypeInfo>(DataProvider.Instance().GetJobTypeById(jobTypeId));
        }
        public List<JobTypeInfo> GetEditableJobTypes()
        {
            return CBO.FillCollection<JobTypeInfo>(DataProvider.Instance().GetEditableJobTypes());
        }
        public void SaveJobType(JobTypeInfo oInfo)
        {
            DataProvider.Instance().SaveJobType(oInfo.JobTypeId, oInfo.JobTitle, oInfo.JobDescription, oInfo.IsCannedJob, oInfo.CannedProcedure);
        }
        public void DeleteJobType(int jobTypeId)
        {
            DataProvider.Instance().DeleteJobType(jobTypeId);
        }
        #endregion

        #region Job Schedule Methods
        public List<JobScheduleInfo> GetJobSchedule()
        {
            return CBO.FillCollection<JobScheduleInfo>(DataProvider.Instance().GetJobSchedule());
        }
        public JobScheduleInfo GetJobScheduleItemById(int jobScheduleId)
        {
            return CBO.FillObject<JobScheduleInfo>(DataProvider.Instance().GetJobScheduleItemById(jobScheduleId));
        }
        public void SaveJobScheduleItem(JobScheduleInfo oInfo)
        {
            DataProvider.Instance().SaveJobScheduleItem(oInfo.JobScheduleId, oInfo.JobTypeId, oInfo.JobScript, oInfo.JobFrequencyValue, oInfo.JobFrequencyType, oInfo.NextJobRun, oInfo.LastJobRun);
        }
        public List<JobScheduleInfo> GetJobScheduleItemsToRun()
        {
            return CBO.FillCollection<JobScheduleInfo>(DataProvider.Instance().GetJobScheduleItemsToRun());
        }
        public void DeleteJobScheduleItem(int jobScheduleId)
        {
            DataProvider.Instance().DeleteJobScheduleItem(jobScheduleId);
        }
        #endregion

        #region Job Schedule History Methods
        public List<JobScheduleHistoryInfo> GetJobScheduleHistory(int jobScheduleId)
        {
            return CBO.FillCollection<JobScheduleHistoryInfo>(DataProvider.Instance().GetJobScheduleHistory(jobScheduleId));
        }
        public void InsertJobScheduleHistory(int jobSchedleId, DateTime ExecuteTime, bool successful, string Detail)
        {
            DataProvider.Instance().InsertJobScheduleHistory(jobSchedleId, ExecuteTime, successful, Detail);
        }
        #endregion

        #region Job Execution
        /// <summary>
        /// This method executes a job, returning the rows affected
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public int ExecuteJob(string storedProcedure, bool isCanned)
        {
            return DataProvider.Instance().ExecuteJob(storedProcedure, isCanned);
        }
        #endregion
    }
}
