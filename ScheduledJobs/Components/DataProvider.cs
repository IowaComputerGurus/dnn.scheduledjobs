// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Data;
using DotNetNuke.Framework;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    /// <summary>
    /// This class provides all data access methods
    /// </summary>
    public abstract class DataProvider
    {

        #region common methods

        /// <summary>
        /// var that is returned in the this singleton
        /// pattern
        /// </summary>
        private static DataProvider instance = null;

        /// <summary>
        /// private static cstor that is used to init an
        /// instance of this class as a singleton
        /// </summary>
        static DataProvider()
        {
            instance = (DataProvider)Reflection.CreateObject("data", "ICG.Modules.ScheduledSqlJobs.Components", "");
        }

        /// <summary>
        /// Exposes the singleton object used to access the database with
        /// the conrete dataprovider
        /// </summary>
        /// <returns></returns>
        public static DataProvider Instance()
        {
            return instance;
        }

        #endregion



        #region Job Types Methods
        public abstract IDataReader GetJobTypes();
        public abstract IDataReader GetJobTypeById(int jobTypeId);
        public abstract IDataReader GetEditableJobTypes();
        public abstract void SaveJobType(int jobTypeId, string jobTitle, string jobDescription, bool isCannedJob, string cannedProcedure);
        public abstract void DeleteJobType(int jobTypeId);
        #endregion

        #region Job Schedule Methods
        public abstract IDataReader GetJobSchedule();
        public abstract IDataReader GetJobScheduleItemById(int jobScheduleId);
        public abstract void SaveJobScheduleItem(int jobScheduleId, int jobTypeId, string jobScript, int frequencyValue, string jobFrequencyType, DateTime nextJobRun, DateTime LastJobRun);
        public abstract IDataReader GetJobScheduleItemsToRun();
        public abstract void DeleteJobScheduleItem(int jobScheduleId);
        #endregion

        #region Job Schedule History Methods
        public abstract IDataReader GetJobScheduleHistory(int jobScheduleId);
        public abstract void InsertJobScheduleHistory(int jobSchedleId, DateTime ExecuteTime, bool successful, string Detail);
        #endregion

        #region Job Execution
        public abstract int ExecuteJob(string storedProcedure, bool isCanned);
        #endregion

    }



}
