<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageJobs.ascx.cs" Inherits="ICG.Modules.ScheduledSqlJobs.ManageJobs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<div style="text-align: left;">
    <asp:Panel ID="pnlView" runat="server">
        <asp:Literal ID="litHeader" runat="server" Mode="PassThrough" />
    
        <asp:DataGrid ID="dgrExistingJobs" runat="server" AutoGenerateColumns="false" 
            CssClass="SJDataGrid" onitemcommand="dgrExistingJobs_ItemCommand">
            <ItemStyle CssClass="SJDataGridItem" />
            <HeaderStyle CssClass="SJDataGridHeader" />
            <AlternatingItemStyle CssClass="SJDataGridAltItem" />
            <Columns>
                <asp:BoundColumn DataField="JobTypeId" Visible="false" />
                <asp:BoundColumn DataField="JobTitle" HeaderText="Job Title" />
                <asp:BoundColumn DataField="JobDescription" HeaderText="Job Description" />
                <asp:BoundColumn DataField="CannedProcedure" HeaderText="SQL Procedure" />
                <asp:ButtonColumn Text="Edit" CommandName="Edit" ButtonType="LinkButton" />
            </Columns>
        </asp:DataGrid>
        
        <p><asp:LinkButton ID="btnAddNew" runat="server" CssClass="dnnPrimaryAction" Text="Add New Job Type" onclick="btnAddNew_Click" /></p>
    </asp:Panel>

    <asp:Panel ID="pnlEdit" runat="server" Visible="false">
        <h3>Add/Edit Job Type</h3>
        
        <div class="dnnForm">
            <div class="dnnFormItem">
                <dnn:label id="lblJobTitle" runat="server" controlname="txtJobTitle" suffix=":" CssClass="dnnFormRequired" />
                <asp:TextBox ID="txtJobTitle" runat="server" width="300px" MaxLength="255" />
                <asp:RequiredFieldValidator ID="JobTitleRequired" runat="server" CssClass="dnnFormError dnnFormMessage" Display="Dynamic" ControlToValidate="txtJobTitle" resourcekey="RequiredField" />
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblJobDescription" runat="server" controlname="txtJobDescription" suffix=":" CssClass="dnnFormRequired" />
                <asp:TextBox ID="txtJobDescription" runat="server" width="300px" Height="60px" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="JobDescriptionRequired" runat="server" CssClass="dnnFormError dnnFormMessage" Display="Dynamic" ControlToValidate="txtJobDescription" resourcekey="RequiredField" />
            </div>
            <div class="dnnFormItem">
                <dnn:label id="lblCannedProcedure" runat="server" controlname="txtCannedProcedure" suffix=":" />
                <asp:TextBox ID="txtCannedProcedure" runat="server" width="300px" Height="100px" TextMode="MultiLine" />
                <asp:RequiredFieldValidator ID="CannedProcedureRequired" runat="server" CssClass="dnnFormError dnnFormMessage" Display="Dynamic" ControlToValidate="txtCannedProcedure" resourcekey="RequiredField" />
            </div>
            <ul class="ulActions">
                <li><asp:LinkButton ID="btnSave" runat="server" CssClass="dnnPrimaryAction" resourcekey="btnSave" onclick="btnSave_Click" /></li>
                <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="dnnSecondaryAction" resourcekey="btnCancel" CausesValidation="false" onclick="btnCancel_Click" /></li>
                <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="dnnSecondaryAction" resourcekey="btnDelete" CausesValidation="false" Visible="false" onclick="btnDelete_Click" /></li>
            </ul>
        </div>
    </asp:Panel>
</div>