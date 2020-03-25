// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;

using ICG.Modules.ScheduledSqlJobs.Components;

namespace ICG.Modules.ScheduledSqlJobs
{
    public partial class ViewScheduledSqlJobs : PortalModuleBase , IActionable 
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScheduleInstaller.ModuleConfiguration = ModuleConfiguration;

                if (!IsPostBack)
                {
                    if (UserInfo.IsSuperUser)
                    {
                        //Show proper panel
                        pnlViewJobs.Visible = true;

                        //Bind lookup items
                        var oController = new ScheduledSqlJobsController();
                        var oTypes = oController.GetJobTypes();
                        ddlJobType.DataSource = oTypes;
                        ddlJobType.DataTextField = "JobTitle";
                        ddlJobType.DataValueField = "JobTypeId";
                        ddlJobType.DataBind();

                        //Localize grid
                        Localization.LocalizeDataGrid(ref dgrJobs, this.LocalResourceFile);
                        Localization.LocalizeDataGrid(ref dgrHistory, this.LocalResourceFile);

                        //Fill the grid
                        BindJobsGrid();

                        //Ensure first type is loaded
                        ddlJobType_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        //Show host only warning
                        pnlHostOnly.Visible = true;
                        ScheduleInstaller.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        /// <summary>
        /// This method is used to bind the jobs to the grid
        /// </summary>
        private void BindJobsGrid()
        {
            var oController = new ScheduledSqlJobsController();
            var oJobs = oController.GetJobSchedule();
            if (oJobs == null)
                oJobs = new List<JobScheduleInfo>();

            dgrJobs.DataSource = oJobs;
            dgrJobs.DataBind();
        }

        /// <summary>
        /// This method will update the job type information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobType.SelectedIndex >= 0)
            {
                var oController = new ScheduledSqlJobsController();
                var oType = oController.GetJobTypeById(int.Parse(ddlJobType.SelectedValue));
                lblJobDescriptionDisplay.Text = oType.JobDescription;
                lblJobScriptDisplay.Text = oType.CannedProcedure;
            }
            else
            {
                lblJobDescriptionDisplay.Text = "";
                lblJobScriptDisplay.Text = "";
            }
        }

        #region Add/Edit Methods
        /// <summary>
        /// This method cancels an operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //Clear values and return view
            hfJobId.Value = "";
            ddlJobType.SelectedIndex = 0;
            ddlJobType_SelectedIndexChanged(sender, e);
            txtJobFrequency.Text = "";
            ddlJobFrequency.SelectedIndex = 0;

            //Panels
            pnlAddEditJobs.Visible = false;
            pnlViewJobs.Visible = true;
        }
        /// <summary>
        /// Responds to the users request to Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var oController = new ScheduledSqlJobsController();
            JobScheduleInfo oInfo;

            if (hfJobId.Value.Equals("-1"))
            {
                oInfo = new JobScheduleInfo();
                oInfo.JobScheduleId = -1;
                oInfo.NextJobRun = System.DateTime.Now.AddMinutes(2);
                oInfo.LastJobRun = System.DateTime.Now;
            }
            else
            {
                //Load the values
                oInfo = oController.GetJobScheduleItemById(int.Parse(hfJobId.Value));
            }

            //Load the rest
            oInfo.JobFrequencyType = ddlJobFrequency.SelectedValue;
            oInfo.JobFrequencyValue = int.Parse(txtJobFrequency.Text);
            oInfo.JobScript = lblJobScriptDisplay.Text;
            oInfo.JobTitle = ddlJobType.Text;
            oInfo.JobTypeId = int.Parse(ddlJobType.SelectedValue);
            
            //Save it
            oController.SaveJobScheduleItem(oInfo);

            //Update 
            BindJobsGrid();

            //Cancel
            btnCancel_Click(sender, e);
        }
        #endregion

        #region New Edit and Delete Methods
        /// <summary>
        /// Adds a job
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddJob_Click(object sender, EventArgs e)
        {
            //Setup the id and toggle
            hfJobId.Value = "-1";
            pnlViewJobs.Visible = false;
            pnlAddEditJobs.Visible = true;
        }
        /// <summary>
        /// This method handles edit and delete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void dgrJobs_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            var oController = new ScheduledSqlJobsController();
            var jobId = int.Parse(e.Item.Cells[0].Text);

            //Take action
            if (e.CommandName.Equals("Edit"))
            {
                //Set id
                hfJobId.Value = jobId.ToString();

                //Get items to load
                var oInfo = oController.GetJobScheduleItemById(jobId);
                ddlJobType.SelectedValue = oInfo.JobTypeId.ToString();
                ddlJobType_SelectedIndexChanged(this, EventArgs.Empty);
                txtJobFrequency.Text = oInfo.JobFrequencyValue.ToString();
                ddlJobFrequency.SelectedValue = oInfo.JobFrequencyType;

                //Swap views
                pnlViewJobs.Visible = false;
                pnlAddEditJobs.Visible = true;

            }
            else if (e.CommandName.Equals("Delete"))
            {
                //Delete
                oController.DeleteJobScheduleItem(jobId);

                //Rebind
                BindJobsGrid();
            }
            else if (e.CommandName.Equals("History"))
            {
                var oHistory = oController.GetJobScheduleHistory(jobId);
                if (oHistory == null)
                    oHistory = new List<JobScheduleHistoryInfo>();
                dgrHistory.DataSource = oHistory;
                dgrHistory.DataBind();

                pnlViewJobs.Visible = false;
                pnlJobHistory.Visible = true;
            }
            else if (e.CommandName.Equals("Run"))
            {
                //Manually run job
                var oJob = oController.GetJobScheduleItemById(jobId);
                var oJobInfo = oController.GetJobTypeById(oJob.JobTypeId);
                var result = "";
                var isSuccess = true;
                try
                {
                    int rows = oController.ExecuteJob(oJob.JobScript, oJobInfo.IsCannedJob);
                    result = "Success<br />" + rows.ToString() + " rows afected";
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                    result = "Error: " + ex.Message;
                }

                //Update the entry and insert history
                oController.InsertJobScheduleHistory(oJob.JobScheduleId, DateTime.Now, isSuccess, result);

                DotNetNuke.UI.Skins.Skin.AddModuleMessage(this, Localization.GetString("RunSuccess", this.LocalResourceFile), DotNetNuke.UI.Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess);
            }
        }
        #endregion

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            pnlJobHistory.Visible = false;
            pnlViewJobs.Visible = true;
        }

        #region IActionable Members
        /// <summary>
        /// IActionable implementation, adds two menu items.  1.) Add New Item.  2.) Support Forum
        /// </summary>
        public DotNetNuke.Entities.Modules.Actions.ModuleActionCollection ModuleActions
        {
            get
            {
                //Create the listing collection
                ModuleActionCollection actions = new ModuleActionCollection();

                //Add manage link
                actions.Add(GetNextActionID(), Localization.GetString("ManageJobTypes", this.LocalResourceFile), ModuleActionType.EditContent, string.Empty, string.Empty, EditUrl("ManageJobs"), 
                    false, DotNetNuke.Security.SecurityAccessLevel.Host, true, false);
                
                //Add support link, edit only, open to new window!
                actions.Add(GetNextActionID(), Localization.GetString("SupportLink", this.LocalResourceFile), ModuleActionType.OnlineHelp, "", "", "http://www.iowacomputergurus.com/forums/afv/topicsview/aff/21.aspx",
                    false, DotNetNuke.Security.SecurityAccessLevel.Edit, true, true);

                return actions;
            }
        }

        #endregion

    }
}