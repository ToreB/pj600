<%@ Page Title="Admin Panel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="no.nith.pj600.dashboard.Styles.AdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <h1>Admin Panel</h1>

   <h2>Upload Excel file</h2>
   <asp:Panel ID="ExcelUploadPanel" runat="server" CssClass="borderWithPadding" >
      <asp:Label ID="ExcelUploadLabel" runat="server" Text="Upload Excel file: " AssociatedControlID="ExcelUploadPanel" /><asp:FileUpload ID="ExcelFileUpload" runat="server" />
      <asp:Button ID="ExcelUploadButton" runat="server" Text="Upload" OnClick="ExcelUploadButton_Click"/>
      <br />
      <p>
         <asp:Label ID="ExcelUploadStatusLabel" runat="server" Text=""></asp:Label>
      </p>
   </asp:Panel>
</asp:Content>
