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
        <asp:LinkButton ID="btnAddJob" runat="server" CssClass="dnnPrimaryAction" resourcekey="btnAddJob" onclick="btnAddJob_Click" />
    </p>

    <asp:DataGrid ID="dgrJobs" runat="server" CssClass="dnnGrid" CellSpacing="4"
        AutoGenerateColumns="false" Width="100%" onitemcommand="dgrJobs_ItemCommand">
        <HeaderStyle CssClass="dnnGridHeader" />
        <ItemStyle CssClass="dnnGridItem"/>
        <AlternatingItemStyle CssClass="dnnGridAltItem"/>
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
    <div class="dnnForm">
        <div class="dnnFormItem">
            <dnn:label id="lblJobType" runat="server" controlname="ddlJobType" suffix=":" />
            <asp:DropDownList ID="ddlJobType" runat="server" AutoPostBack="true" onselectedindexchanged="ddlJobType_SelectedIndexChanged" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblJobDescription" runat="server" controlname="lblJobDescriptionDisplay" suffix=":" />
            <asp:Label ID="lblJobDescriptionDisplay" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblJobScript" runat="server" controlname="lblJobScriptDisplay" suffix=":" />
            <asp:Label ID="lblJobScriptDisplay" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblJobFrequency" runat="server" controlname="txtJobFrequency" suffix=":" />
            <asp:TextBox ID="txtJobFrequency" runat="server" MaxLength="3" Columns="3" />
                <asp:DropDownList ID="ddlJobFrequency" runat="server">
                    <asp:ListItem Text="Hours" Value="Hours" />
                    <asp:ListItem Text="Days" Value="Days" />
                    <asp:ListItem Text="Weeks" Value="Weeks" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="JobFrequencyRequired" runat="server" CssClass="dnnFormMessage dnnFormError" Display="Dynamic" ControlToValidate="txtJobFrequency" resourcekey="JobFrequencyRequired" />
                <asp:CompareValidator ID="JobFrequencyFormat" runat="server" CssClass="dnnFormMessage dnNFormError" Display="Dynamic" ControlToValidate="txtJobFrequency" Type="Integer" Operator="DataTypeCheck" resourcekey="JobFrequencyFormat" />
        </div>
        <ul class="dnnActions">
            <li><asp:LinkButton ID="btnSave" runat="server" CssClass="dnnPrimaryAction" resourcekey="btnSave" onclick="btnSave_Click" /></li>
            <li><asp:LinkButton ID="btnCancel" runat="server" CssClass="dnnSecondaryAction" resourcekey="btnCancel" CausesValidation="false" onclick="btnCancel_Click" /></li>
        </ul>
    </div>


</asp:Panel>

<asp:Panel ID="pnlJobHistory" runat="server" CssClass="Normal" Visible="false">
    <asp:Label ID="lblJobHistoryHeader" runat="server" resourcekey="lblJobHistoryHeader" />
    <asp:DataGrid ID="dgrHistory" runat="server" CssClass="dnnGrid" CellSpacing="4"
        AutoGenerateColumns="false" >
        <HeaderStyle CssClass="dnnGridHeader" />
        <ItemStyle CssClass="dnnGridItem"/>
        <AlternatingItemStyle CssClass="dnnGridAltItem"/>
        <Columns>
            <asp:BoundColumn DataField="ExecuteTime" HeaderText="ExecuteTime" />
            <asp:BoundColumn DataField="Successful" HeaderText="Successful" />
            <asp:BoundColumn DataField="Detail" HeaderText="Detail" />
        </Columns>
    </asp:DataGrid>
    <p>
        <asp:LinkButton ID="btnReturn" runat="server" CssClass="dnnPrimaryAction" resourcekey="btnReturn" onclick="btnReturn_Click" />
    </p>
</asp:Panel>

<p class="Normal">
<a href="http://www.iowacomputergurus.com/Products/Open-Source" target="_blank" class="CommandButton">Scheduled SQL Jobs is a free module provided by IowaComputerGurus Inc. (Donations Appreciated)</a>
</p>
