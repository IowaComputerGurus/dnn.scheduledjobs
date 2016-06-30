<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewScheduledSqlJobs.ascx.cs" Inherits="ICG.Modules.ScheduledSqlJobs.ViewScheduledSqlJobs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register src="ScheduleInstaller.ascx" tagname="ScheduleInstaller" tagprefix="ICG" %>

<ICG:ScheduleInstaller id="ScheduleInstaller" runat="server" />

<asp:panel ID="pnlHostOnly" runat="server" CssClass="Normal" Visible="false">
    <asp:Label ID="lblHostOnly" runat="server" resourcekey="lblHostOnly" />
</asp:panel>

<asp:Panel ID="pnlViewJobs" runat="server" CssClass="Normal" Visible="false">
    <asp:Label ID="lblViewHeader" runat="server" resourcekey="lblViewHeader" />
    <p>
        <asp:LinkButton ID="btnAddJob" runat="server" CssClass="CommandButton" 
            resourcekey="btnAddJob" onclick="btnAddJob_Click" />
    </p>
    <asp:DataGrid ID="dgrJobs" runat="server" CssClass="Normal" CellSpacing="4"
        AutoGenerateColumns="false" Width="100%" onitemcommand="dgrJobs_ItemCommand">
        <HeaderStyle CssClass="SubHead" />
        <Columns>
            <asp:BoundColumn DataField="JobScheduleId" Visible="false" />
            <asp:BoundColumn DataField="JobTitle" HeaderText="JobTitle" />
            <asp:BoundColumn DataField="JobScript" HeaderText="JobScript" />
            <asp:BoundColumn DataField="Schedule" HeaderText="Schedule" />
            <asp:BoundColumn DataField="NextJobRun" HeaderText="NextJobRun" />
            <asp:BoundColumn DataField="LastJobRun" HeaderText="LastJobRun" />
            <asp:ButtonColumn ButtonType="LinkButton" Text="Edit" CommandName="Edit">
                <ItemStyle CssClass="CommandButton" />
            </asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="Delete" CommandName="Delete">
                <ItemStyle CssClass="CommandButton" />
            </asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" Text="History" CommandName="History">
                <ItemStyle CssClass="CommandButton" />
            </asp:ButtonColumn>
            <asp:ButtonColumn ButtonType="LinkButton" text="Run Now" CommandName="Run">
                <ItemStyle CssClass="CommandButton" />
            </asp:ButtonColumn>
        </Columns>
    </asp:DataGrid>
</asp:Panel>

<asp:Panel ID="pnlAddEditJobs" runat="server" CssClass="Normal" Visible="false">
    <asp:Label ID="lblAddEditHeader" runat="server" CssClass="Normal" resourcekey="lblAddEditHeader" />
    <asp:HiddenField ID="hfJobId" runat="server" />
    <table class="Normal">
        <tr>
            <td class="SubHead" width="150">
                <dnn:label id="lblJobType" runat="server" controlname="ddlJobType" suffix=":" />
            </td>
            <td>
                <asp:DropDownList ID="ddlJobType" runat="server" AutoPostBack="true" 
                    onselectedindexchanged="ddlJobType_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="150">
                <dnn:label id="lblJobDescription" runat="server" controlname="lblJobDescriptionDisplay" suffix=":" />
            </td>
            <td>
                <asp:Label ID="lblJobDescriptionDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="150">
                <dnn:label id="lblJobScript" runat="server" controlname="lblJobScriptDisplay" suffix=":" />
            </td>
            <td>
                <asp:Label ID="lblJobScriptDisplay" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="SubHead" width="150">
                <dnn:label id="lblJobFrequency" runat="server" controlname="txtJobFrequency" suffix=":" />
            </td>
            <td>
                <asp:TextBox ID="txtJobFrequency" runat="server" MaxLength="3" Columns="3" />
                <asp:DropDownList ID="ddlJobFrequency" runat="server">
                    <asp:ListItem Text="Hours" Value="Hours" />
                    <asp:ListItem Text="Days" Value="Days" />
                    <asp:ListItem Text="Weeks" Value="Weeks" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="JobFrequencyRequired" runat="server" CssClass="NormalRed" Display="Dynamic"
                    ControlToValidate="txtJobFrequency" resourcekey="JobFrequencyRequired" />
                <asp:CompareValidator ID="JobFrequencyFormat" runat="server" CssClass="NormalRed" Display="Dynamic"
                    ControlToValidate="txtJobFrequency" Type="Integer" Operator="DataTypeCheck" resourcekey="JobFrequencyFormat" />
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinkButton ID="btnSave" runat="server" CssClass="CommandButton" 
                    resourcekey="btnSave" onclick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:LinkButton ID="btnCancel" runat="server" CssClass="CommandButton" 
                    resourcekey="btnCancel" CausesValidation="false" onclick="btnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:Panel ID="pnlJobHistory" runat="server" CssClass="Normal" Visible="false">
    <asp:Label ID="lblJobHistoryHeader" runat="server" resourcekey="lblJobHistoryHeader" />
    <asp:DataGrid ID="dgrHistory" runat="server" CssClass="Normal" CellSpacing="4"
        AutoGenerateColumns="false" >
        <HeaderStyle CssClass="SubHead" />
        <Columns>
            <asp:BoundColumn DataField="ExecuteTime" HeaderText="ExecuteTime" />
            <asp:BoundColumn DataField="Successful" HeaderText="Successful" />
            <asp:BoundColumn DataField="Detail" HeaderText="Detail" />
        </Columns>
    </asp:DataGrid>
    <p>
        <asp:LinkButton ID="btnReturn" runat="server" CssClass="CommandButton" 
            resourcekey="btnReturn" onclick="btnReturn_Click" />
    </p>
</asp:Panel>

<p class="Normal">
<a href="http://www.iowacomputergurus.com/free-products/dotnetnuke-modules/scheduled-sql-jobs.aspx" target="_blank" class="CommandButton">Scheduled SQL Jobs is a free module provided by IowaComputerGurus Inc. (Donations Appreciated)</a>
</p>
