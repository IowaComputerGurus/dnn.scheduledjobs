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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
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
                ScheduleItem oItem = SchedulingProvider.Instance().GetSchedule(Assembly_Name, "");
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
            ScheduleItem oItem = SchedulingProvider.Instance().GetSchedule(Assembly_Name, "");
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
            ScheduleItem oItem = new ScheduleItem();
            oItem.CatchUpEnabled = false;
            oItem.Enabled = true;
            oItem.NextStart = System.DateTime.Now.AddMinutes(4);
            oItem.RetainHistoryNum = 60;
            oItem.RetryTimeLapse = 30;
            oItem.RetryTimeLapseMeasurement = "m";
            oItem.ScheduleSource = ScheduleSource.NOT_SET;
            oItem.TimeLapse = 30;
            oItem.TimeLapseMeasurement = "m";
            oItem.TypeFullName = Assembly_Name;
            SchedulingProvider.Instance().AddSchedule(oItem);
            pnlNotInstalled.Visible = false;
        }
    }
}