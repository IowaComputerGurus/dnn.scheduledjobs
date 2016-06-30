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
            ScheduledSqlJobsController oController = new ScheduledSqlJobsController();
            List<JobTypeInfo> oJobs = oController.GetEditableJobTypes();
            if (oJobs == null)
                oJobs = new List<JobTypeInfo>();
            dgrExistingJobs.DataSource = oJobs;
            dgrExistingJobs.DataBind();
        }

        protected void dgrExistingJobs_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName.Equals("Edit"))
            {
                int jobTypeId = int.Parse(e.Item.Cells[0].Text);
                ScheduledSqlJobsController oController = new ScheduledSqlJobsController();
                JobTypeInfo oInfo = oController.GetJobTypeById(jobTypeId);
                this.JobId = oInfo.JobTypeId;
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
            this.JobId = -1;
            pnlEdit.Visible = false;
            pnlView.Visible = true;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ScheduledSqlJobsController oCOntroller = new ScheduledSqlJobsController();
            oCOntroller.DeleteJobType(this.JobId);
            BindJobs();
            btnCancel_Click(sender, e);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScheduledSqlJobsController oController = new ScheduledSqlJobsController();
            JobTypeInfo oInfo = new JobTypeInfo();
            oInfo.CannedProcedure = txtCannedProcedure.Text;
            oInfo.IsCannedJob = false;
            oInfo.JobDescription = txtJobDescription.Text;
            oInfo.JobTitle = txtJobTitle.Text;
            oInfo.JobTypeId = this.JobId;
            oController.SaveJobType(oInfo);
            BindJobs();
            btnCancel_Click(sender, e);
        }
        
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            pnlView.Visible = false;
            pnlEdit.Visible = true;
            this.JobId = -1;
        }
        #endregion



        


    }
}