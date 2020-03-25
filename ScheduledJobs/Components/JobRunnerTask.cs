// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
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
            var oJobs = oController.GetJobScheduleItemsToRun();

            if (oJobs != null && oJobs.Count > 0)
            {
                InsertLogNote($"A total of {oJobs.Count} jobs to process");
                foreach (JobScheduleInfo oCurrentJob in oJobs)
                {
                    InsertLogNote($"Processing {oCurrentJob.JobTitle}");

                    var isSuccess = true;
                    var result = "";
                    var oJobInfo = oController.GetJobTypeById(oCurrentJob.JobTypeId);
                    try
                    {
                        var rows = oController.ExecuteJob(oCurrentJob.JobScript, oJobInfo.IsCannedJob);
                        result =$"Success<br />{rows} rows affected";
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        result = $"Error: {ex.Message}";
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