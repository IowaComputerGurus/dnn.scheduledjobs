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
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Scheduling;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    public class JobRunnerTask : SchedulerClient
    {
        #region Default Constructor

        /// <summary>
        /// Constructor needed to obtain the Schedule History item
        /// </summary>
        /// <param name="oItem"></param>
        public JobRunnerTask(ScheduleHistoryItem oItem)
        {
            ScheduleHistoryItem = oItem;
        }

        #endregion

        #region Working Methods

        public override void DoWork()
        {
            try
            {
                //Perform required items for logging
                Progressing();

                //Call our process method
                RunJobs();

                //Show success
                ScheduleHistoryItem.Succeeded = true;
                InsertLogNote("Task completed 100%");
            }
            catch (Exception ex)
            {
                ScheduleHistoryItem.Succeeded = false;
                InsertLogNote("Exception= " + ex);
                Errored(ref ex);
                Exceptions.LogException(ex);
            }
        }

        public void RunJobs()
        {
            var oController = new ScheduledSqlJobsController();
            List<JobScheduleInfo> oJobs = oController.GetJobScheduleItemsToRun();

            if (oJobs != null && oJobs.Count > 0)
            {
                InsertLogNote("A total of " + oJobs.Count.ToString() + " jobs to process");
                foreach (JobScheduleInfo oCurrentJob in oJobs)
                {
                    InsertLogNote("Processing " + oCurrentJob.JobTitle);

                    bool isSuccess = true;
                    string result = "";
                    JobTypeInfo oJobInfo = oController.GetJobTypeById(oCurrentJob.JobTypeId);
                    try
                    {
                        int rows = oController.ExecuteJob(oCurrentJob.JobScript, oJobInfo.IsCannedJob);
                        result = "Success<br />" + rows.ToString() + " rows afected";
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        result = "Error: " + ex.Message;
                    }

                    //Update the entry and insert history
                    oController.InsertJobScheduleHistory(oCurrentJob.JobScheduleId, DateTime.Now, isSuccess, result);
                    oCurrentJob.NextJobRun = CalculateNextRun(oCurrentJob.JobFrequencyValue,
                                                              oCurrentJob.JobFrequencyType);
                    oCurrentJob.LastJobRun = DateTime.Now;
                    oController.SaveJobScheduleItem(oCurrentJob);
                }
            }
            else
            {
                InsertLogNote("No Jobs to run");
            }
        }

        /// <summary>
        /// Determines the next time a job is to run!
        /// </summary>
        /// <param name="frequency"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private DateTime CalculateNextRun(int frequency, string scale)
        {
            switch (scale)
            {
                case "Hours":
                    return DateTime.Now.AddHours(frequency);
                case "Days":
                    return DateTime.Now.AddDays(frequency);
                case "Weeks":
                    return DateTime.Now.AddDays(frequency*7);
                default:
                    return DateTime.Now.AddDays(2);
            }
        }

        #endregion

        #region Log Helpers

        /// <summary>
        /// Helper method to keep the code uncluttered
        /// </summary>
        /// <param name="message"></param>
        private void InsertLogNote(string message)
        {
            ScheduleHistoryItem.AddLogNote(message + "<br />");
        }

        #endregion
    }
}