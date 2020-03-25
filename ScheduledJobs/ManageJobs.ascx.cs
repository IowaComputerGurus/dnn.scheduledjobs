// IowaComputerGurus, Inc. licenses this file to you under the MIT License
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Localization;

using ICG.Modules.ScheduledSqlJobs.Components;

namespace ICG.Modules.ScheduledSqlJobs
{
    public partial class ManageJobs : PortalModuleBase
    {

        private int JobId
        {
            get
            {
                if (ViewState["jobId"] != null)
                    return int.Parse(ViewState["jobId"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["jobId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                litHeader.Text = Localization.GetString("Header", this.LocalResourceFile);

                BindJobs();
            }
        }

        private void BindJobs()
        {
            var oController = new ScheduledSqlJobsController();
            var oJobs = oController.GetEditableJobTypes();
            if (oJobs == null)
                oJobs = new List<JobTypeInfo>();
            dgrExistingJobs.DataSource = oJobs;
            dgrExistingJobs.DataBind();
        }

        protected void dgrExistingJobs_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                var jobTypeId = int.Parse(e.Item.Cells[0].Text);
                var oController = new ScheduledSqlJobsController();
                var oInfo = oController.GetJobTypeById(jobTypeId);
                JobId = oInfo.JobTypeId;
                txtJobTitle.Text = oInfo.JobTitle;
                txtJobDescription.Text = oInfo.JobDescription;
                txtCannedProcedure.Text = oInfo.CannedProcedure;
                btnDelete.Visible = true;
                pnlView.Visible = false;
                pnlEdit.Visible = true;
            }
        }

        #region Add/Save/Cancel/Delete Methods

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtJobTitle.Text = string.Empty;
            txtJobDescription.Text = string.Empty;
            txtCannedProcedure.Text = string.Empty;
            JobId = -1;
            pnlEdit.Visible = false;
            pnlView.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var oCOntroller = new ScheduledSqlJobsController();
            oCOntroller.DeleteJobType(this.JobId);
            BindJobs();
            btnCancel_Click(sender, e);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var oController = new ScheduledSqlJobsController();
            var oInfo = new JobTypeInfo
            {
                CannedProcedure = txtCannedProcedure.Text,
                IsCannedJob = false,
                JobDescription = txtJobDescription.Text,
                JobTitle = txtJobTitle.Text,
                JobTypeId = this.JobId
            };
            oController.SaveJobType(oInfo);
            BindJobs();
            btnCancel_Click(sender, e);
        }
        
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlView.Visible = false;
            pnlEdit.Visible = true;
            JobId = -1;
        }
        #endregion



        


    }
}