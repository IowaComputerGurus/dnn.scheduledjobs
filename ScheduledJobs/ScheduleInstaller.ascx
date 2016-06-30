<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ScheduleInstaller.ascx.cs" Inherits="ICG.Modules.ScheduledSqlJobs.ScheduleInstaller" %>

<asp:Panel ID="pnlNotInstalled" runat="server" Visible="false" CssClass="Normal">
    <asp:Label ID="lblNotInstalled" runat="server" CssClass="Normal" resourcekey="lblNotInstalled" />
    <asp:LinkButton ID="btnInstallNow" runat="server" CssClass="CommandButton" 
        resourcekey="btnInstallNow" onclick="btnInstallNow_Click" />
</asp:Panel>

<asp:Panel ID="pnlNotEnabled" runat="server" Visible="false" CssClass="Normal">
    <asp:label ID="lblNotEnabled" runat="server" CssClass="Normal" resourcekey="lblNotEnabled" />
    <asp:LinkButton ID="btnEnableNow" runat="server" CssClass="CommandButton" 
        resourcekey="btnEnableNow" onclick="btnEnableNow_Click" />
</asp:Panel>