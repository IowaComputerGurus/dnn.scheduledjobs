/*
 * Copyright (c) 2008-2009 IowaComputerGurus Inc (http://www.iowacomputergurus.com)
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
using System.Data;
using DotNetNuke;
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
