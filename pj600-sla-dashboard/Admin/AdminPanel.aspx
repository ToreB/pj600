<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="no.nith.pj600.dashboard.Admin.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Admin Panel</h1>

   <h2>Upload export file from Tripletex</h2>
   <asp:Panel ID="FileUploadPanel" runat="server" CssClass="borderWithPadding" >
      <asp:Label ID="FileUploadLabel" runat="server" Text="Upload file: " AssociatedControlID="FileUploadPanel" /><asp:FileUpload ID="FileUpload" runat="server" />
      <asp:Button ID="FileUploadButton" runat="server" Text="Upload" OnClick="FileUploadButton_Click"/>
   </asp:Panel>
   <p>
      <asp:Label ID="FileUploadStatusLabel" runat="server" Text=""></asp:Label>
   </p>

   <br />
   
   <h2>SLA Projects</h2>
   <a href="ManageSLAProjects.aspx">Manage SLA Projects</a>

   <br />

   <h2>Account Management</h2>
   <a href="AccountsManagement.aspx">Manage Accounts</a>

</asp:Content>
