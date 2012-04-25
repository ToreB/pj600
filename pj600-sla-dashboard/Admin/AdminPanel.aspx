<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Admin Panel</h1>

   <h2>Tripletex</h2>
   <a href="TripletexFileUpload.aspx">Upload export file from Tripletex</a>

   <br />
   
   <h2>SLA Projects</h2>
   <a href="ManageSLAProjects.aspx">Manage SLA Projects</a>

   <br />

   <h2>Account Management</h2>
   <a href="AccountsManagement.aspx">Manage Accounts</a>

</asp:Content>
