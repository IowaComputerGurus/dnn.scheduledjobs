/*
 * Copyright (c) 2008-2010 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
 * Copyright Contact: webmaster@iowacomputergurus.com
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights to use, 
 * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software. 
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT 
 * NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE
 * */

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DotNetNuke;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

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
