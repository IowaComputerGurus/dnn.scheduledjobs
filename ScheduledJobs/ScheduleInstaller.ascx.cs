// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Scheduling;

namespace ICG.Modules.ScheduledSqlJobs
{
    public partial class ScheduleInstaller : PortalModuleBase
    {
        private const string Assembly_Name = "ICG.Modules.ScheduledSqlJobs.Components.JobRunnerTask, ICG.Modules.ScheduledSqlJobs";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Can we get the item
                var oItem = SchedulingProvider.Instance().GetSchedule(Assembly_Name, "");
                if (oItem != null)
                {
                    //Ensure enabled
                    if (!oItem.Enabled)
                    {
                        //Not enabled
                        pnlNotEnabled.Visible = true;
                    }
                }
                else
                {
                    //Not configured
                    pnlNotInstalled.Visible = true;
                }
            }
        }

        /// <summary>
        /// This method will actually update the schedule to "Enabled"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnableNow_Click(object sender, EventArgs e)
        {
            var oItem = SchedulingProvider.Instance().GetSchedule(Assembly_Name, "");
            oItem.Enabled = true;
            SchedulingProvider.Instance().UpdateSchedule(oItem);
            pnlNotEnabled.Visible = false;
        }

        /// <summary>
        /// This method will actually install the schedule item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInstallNow_Click(object sender, EventArgs e)
        {
            var oItem = new ScheduleItem
            {
                CatchUpEnabled = false,
                Enabled = true,
                NextStart = System.DateTime.Now.AddMinutes(4),
                RetainHistoryNum = 60,
                RetryTimeLapse = 30,
                RetryTimeLapseMeasurement = "m",
                ScheduleSource = ScheduleSource.NOT_SET,
                TimeLapse = 30,
                TimeLapseMeasurement = "m",
                TypeFullName = Assembly_Name
            };
            SchedulingProvider.Instance().AddSchedule(oItem);
            pnlNotInstalled.Visible = false;
        }
    }
}