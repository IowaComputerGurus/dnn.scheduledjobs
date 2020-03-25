// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Data;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;

namespace ICG.Modules.ScheduledSqlJobs.Components
{
    public class SqlDataProvider : DataProvider
    {


        #region vars

        private const string providerType = "data";
        private const string moduleQualifier = "ICG_SJ_";

        private ProviderConfiguration providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);
        private string connectionString;
        private string providerPath;
        private string objectQualifier;
        private string databaseOwner;

        #endregion

        #region cstor

        /// <summary>
        /// cstor used to create the sqlProvider with required parameters from the configuration
        /// section of web.config file
        /// </summary>
        public SqlDataProvider()
        {
            Provider provider = (Provider)providerConfiguration.Providers[providerConfiguration.DefaultProvider];
            connectionString = DotNetNuke.Common.Utilities.Config.GetConnectionString();

            if (connectionString == string.Empty)
                connectionString = provider.Attributes["connectionString"];

            providerPath = provider.Attributes["providerPath"];

            objectQualifier = provider.Attributes["objectQualifier"];
            if (objectQualifier != string.Empty && !objectQualifier.EndsWith("_"))
                objectQualifier += "_";

            databaseOwner = provider.Attributes["databaseOwner"];
            if (databaseOwner != string.Empty && !databaseOwner.EndsWith("."))
                databaseOwner += ".";
        }

        #endregion

        #region properties

        public string ConnectionString
        {
            get { return connectionString; }
        }


        public string ProviderPath
        {
            get { return providerPath; }
        }

        public string ObjectQualifier
        {
            get { return objectQualifier; }
        }


        public string DatabaseOwner
        {
            get { return databaseOwner; }
        }

        #endregion

        #region private methods

        private string GetFullyQualifiedName(string name)
        {
            return DatabaseOwner + ObjectQualifier + moduleQualifier + name;
        }

        private object GetNull(object field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(field, DBNull.Value);
        }

        #endregion

        #region Job Types Methods
        public override IDataReader GetJobTypes()
        {
	        return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobTypes"));
        }
        public override IDataReader GetJobTypeById(int jobTypeId)
        {
	        return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobTypeById"), jobTypeId);
        }
        public override IDataReader GetEditableJobTypes()
        {
            return SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetEditableJobTypes"));
        }
        public override void SaveJobType(int jobTypeId, string jobTitle, string jobDescription, bool isCannedJob, string cannedProcedure)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("SaveJobType"), jobTypeId, jobTitle, jobDescription, isCannedJob, cannedProcedure);
        }

        public override void DeleteJobType(int jobTypeId)
        {
            SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteJobType"), jobTypeId);
        }
        #endregion

        #region Job Schedule Methods
        public override IDataReader GetJobSchedule()
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobSchedule"));
        }
        public override IDataReader GetJobScheduleItemById(int jobScheduleId)
        {
            return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobScheduleItemById"), jobScheduleId);
        }
        public override void SaveJobScheduleItem(int jobScheduleId, int jobTypeId, string jobScript, int frequencyValue, string jobFrequencyType, DateTime nextJobRun, DateTime LastJobRun)
        {
	        SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("SaveJobScheduleItem"), jobScheduleId, jobTypeId, jobScript, frequencyValue, jobFrequencyType, nextJobRun, LastJobRun);
        }
        public override IDataReader GetJobScheduleItemsToRun()
        {
	        return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobScheduleItemsToRun"));
        }
        public override void DeleteJobScheduleItem(int jobScheduleId)
        {
	        SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("DeleteJobScheduleItem"), jobScheduleId);
        }
        #endregion

        #region Job Schedule History Methods
        public override IDataReader GetJobScheduleHistory(int jobScheduleId)
        {
	        return (IDataReader)SqlHelper.ExecuteReader(connectionString, GetFullyQualifiedName("GetJobScheduleHistory"), jobScheduleId);
        }
        public override void InsertJobScheduleHistory(int jobScheduleId, DateTime ExecuteTime, bool successful, string Detail)
        {
	        SqlHelper.ExecuteNonQuery(connectionString, GetFullyQualifiedName("InsertJobScheduleHistory"), jobScheduleId, ExecuteTime, successful, Detail);
        }
        #endregion

        #region Job Execution
        public override int ExecuteJob(string storedProcedure, bool isCanned)
        {
            if (isCanned)
                return SqlHelper.ExecuteNonQuery(connectionString, ObjectQualifier + storedProcedure);
            else
                return SqlHelper.ExecuteNonQuery(connectionString, storedProcedure);
        }
        #endregion
    }
}
